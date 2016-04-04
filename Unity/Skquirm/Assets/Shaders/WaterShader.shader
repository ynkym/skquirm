Shader "FX/CustomWater"
{
	Properties
	{
		_horizonColor("Horizon color", COLOR) = (.172 , .463 , .435 , 0)
		_WaveScale("Wave scale", Range(0.02,0.15)) = .07
		_WaveHeightScale("Wave height scale", Range(0.00002,0.70)) = 0.15
		_WaveHeight("Wave height", Range(0, 5.0)) = 1.0
		[NoScaleOffset] _ColorControl("Reflective color (RGB) fresnel (A) ", 2D) = "" { }
		[NoScaleOffset] _BumpMap("Waves Normalmap ", 2D) = "" { }
		[NoScaleOffset] _HeightMap("Waves Heightmap ", 2D) = "" { }
		WaveSpeed("Wave speed (map1 x,y; map2 x,y)", Vector) = (19,9,-16,-7)
		WaveSpeed2("Wave speed (map3 x,y; map4 x,y)", Vector) = (-4,10,3,-8)
		WaveHeightSpeed("Wave Height speed (map1 x,y; map2 x,y)", Vector) = (3,4,-1.5,-0.2)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		GrabPass{ "_GrabTexture" }
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			#pragma enable_d3d11_debug_symbols
			#pragma target 3.0

			#include "UnityCG.cginc"

			uniform float4 _horizonColor;

			uniform float4 WaveSpeed;
			uniform float4 WaveHeightSpeed;
			uniform float4 _WaveScale4;
			uniform float4 _WaveOffset;
			uniform float4 _WaveHeightScale4;
			uniform float4 _WaveHeightOffset;
			uniform float4 _WaveScale4_2;
			uniform float4 _WaveOffset_2;
			uniform float _WaveHeight;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal: NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 bumpuv[4] : TEXCOORD0;
				float3 viewDir : TEXCOORD4;
				float4 grabuv : TEXCOORD5;
				UNITY_FOG_COORDS(6)
			};

			sampler2D _GrabTexture;
			sampler2D _BumpMap;
			sampler2D _HeightMap;
			sampler2D _ColorControl;
			
			v2f vert (appdata v)
			{
				v2f o;
				
				
				// Going to slide 4 bump map to make it looks more complicated
				float4 temp;
				float4 wpos = mul(_Object2World, v.vertex);
				// First 2 bump map
				temp.xyzw = wpos.xzxz * _WaveScale4 + _WaveOffset;
				o.bumpuv[0] = temp.xy;
				o.bumpuv[1] = temp.wz;
				// Second 2 bump map
				temp.xyzw = wpos.xzxz * _WaveScale4_2 + _WaveOffset_2;
				o.bumpuv[2] = temp.xy;
				o.bumpuv[3] = temp.wz;

				float4 tempHeight;
				float4 heightuv = wpos.xzxz * _WaveHeightScale4 + _WaveHeightOffset;
				tempHeight.xy = heightuv.xy;
				tempHeight.zw = float2(0,0);
				float4 height1 = tex2Dlod(_HeightMap, tempHeight);
				tempHeight.xy = heightuv.wz;
				tempHeight.zw = float2(0, 0);
				float4 height2 = tex2Dlod(_HeightMap, tempHeight);
				half heightValue1 = (height1.r + height1.g + height1.b)/3.0 ;
				half heightValue2 = (height2.r + height2.g + height2.b)/3.0 ;
				half heightValue = (heightValue1 + heightValue2) * 0.5;
				
				//v.vertex = mul(_World2Object, wpos);
				wpos.y += heightValue * _WaveHeight;

				o.pos = mul(UNITY_MATRIX_VP, wpos);
				o.grabuv = ComputeGrabScreenPos(o.pos);
				o.viewDir.xzy = normalize(WorldSpaceViewDir(v.vertex));

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			half4 frag (v2f i) : SV_Target
			{
				half3 bump1 = UnpackNormal(tex2D(_BumpMap, i.bumpuv[0])).rgb;
				half3 bump2 = UnpackNormal(tex2D(_BumpMap, i.bumpuv[1])).rgb;
				half3 bump3 = UnpackNormal(tex2D(_BumpMap, i.bumpuv[2])).rgb;
				half3 bump4 = UnpackNormal(tex2D(_BumpMap, i.bumpuv[3])).rgb;
				half3 bump = (bump1 + bump2 + bump3 + bump4) * 0.25;
				//half3 bump = (bump1 + bump2) * 0.5;

				half4 grabCol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grabuv));

				half fresnel = dot(i.viewDir, bump);
				half4 water = tex2D(_ColorControl, float2(fresnel, fresnel));

				half4 col;
				col.rgb = lerp(water.rgb, grabCol.rgb, water.a);
				col.a = grabCol.a;

				//col.rgb = lerp(water.rgb, _horizonColor.rgb, water.a);
				//col.a = _horizonColor.a;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
