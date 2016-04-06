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
		_VortexSpeed("Vortex Speed",Range(0.02, 5)) = 0.05
			_VortexAcceleration("Vortex Acceleration", Range(0.8, 20)) = 2
			_VortexScale("Vortex scale", Range(0.02,5.6)) = 0.15
			_VortexMaxSpeed("Vortex max speed" ,Range(20, 100)) = 50
			_VortexCenterHeight("Vortex center depth", Range(0,90)) = 45

	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		GrabPass{  }
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
#pragma multi_compile START_VORTEX STOP_VORTEX
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
			uniform float _VortexOffset;
			uniform float _VortexSpeed;
			uniform float _VortexMaxSpeed;
			uniform float _VortexCenterHeight;
			uniform float tempSpeed;
#if defined(START_VORTEX)
			uniform float decreaseHeight;
#endif
			struct appdata
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float3 normal: NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 bumpuv[4] : TEXCOORD0;
				float3 viewDir : TEXCOORD4;
				float4 grabuv : TEXCOORD5;
				float4 uv : TEXCOORD6;
				UNITY_FOG_COORDS(7)
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
#if defined (STOP_VORTEX)
				o.bumpuv[0] = temp.xy;
				o.bumpuv[1] = temp.wz;
#endif
#if defined (START_VORTEX)
				o.bumpuv[0] = float2(temp.x * cos(_VortexOffset) - temp.y*sin(_VortexOffset), temp.x * sin(_VortexOffset) + temp.y*cos(_VortexOffset));
				o.bumpuv[1] = float2(temp.z * cos(_VortexOffset) - temp.w*sin(_VortexOffset), temp.z * sin(_VortexOffset) + temp.w*cos(_VortexOffset));
#endif
				// Second 2 bump map
				temp.xyzw = wpos.xzxz * _WaveScale4_2 + _WaveOffset_2;
#if defined (STOP_VORTEX)
				o.bumpuv[2] = temp.xy;
				o.bumpuv[3] = temp.wz;
#endif
#if defined (START_VORTEX)
				o.bumpuv[2] = float2(temp.x * cos(_VortexOffset) - temp.y*sin(_VortexOffset), temp.x * sin(_VortexOffset) + temp.y*cos(_VortexOffset));
				o.bumpuv[3] = float2(temp.z * cos(_VortexOffset) - temp.w*sin(_VortexOffset), temp.z * sin(_VortexOffset) + temp.w*cos(_VortexOffset));
#endif

				float4 tempHeight;
				float4 heightuv = wpos.xzxz * _WaveHeightScale4 + _WaveHeightOffset;
				tempHeight.xy = heightuv.xy;
				tempHeight.zw = float2(0,0);
				float4 height1 = tex2Dlod(_HeightMap, tempHeight);
				tempHeight.xy = heightuv.wz;
				tempHeight.zw = float2(0, 0);
				float4 height2 = tex2Dlod(_HeightMap, tempHeight);
				float heightValue1 = (height1.r + height1.g + height1.b)/3.0 ;
				float heightValue2 = (height2.r + height2.g + height2.b)/3.0 ;
				float heightValue = (heightValue1 + heightValue2) * 0.5;
				
				//v.vertex = mul(_World2Object, wpos);
				wpos.y += heightValue * _WaveHeight;

#if defined(START_VORTEX)
				decreaseHeight = (tempSpeed / _VortexMaxSpeed)*_VortexCenterHeight;
				if (v.vertex.x == 0 && v.vertex.z == 0) {
					wpos.y -= decreaseHeight;
				}
#endif

//#if defined(START_VORTEX)
//				o.bumpuv[0] = float2(o.bumpuv[0].x * cos(_VortexOffset) - o.bumpuv[0].y*sin(_VortexOffset), o.bumpuv[0].x * sin(_VortexOffset) + o.bumpuv[0].y*cos(_VortexOffset));
//				o.bumpuv[1] = float2(o.bumpuv[1].x * cos(_VortexOffset) - o.bumpuv[1].y*sin(_VortexOffset), o.bumpuv[1].x * sin(_VortexOffset) + o.bumpuv[1].y*cos(_VortexOffset));
//				o.bumpuv[2] = float2(o.bumpuv[2].x * cos(_VortexOffset) - o.bumpuv[2].y*sin(_VortexOffset), o.bumpuv[2].x * sin(_VortexOffset) + o.bumpuv[2].y*cos(_VortexOffset));
//				o.bumpuv[3] = float2(o.bumpuv[3].x * cos(_VortexOffset) - o.bumpuv[3].y*sin(_VortexOffset), o.bumpuv[3].x * sin(_VortexOffset) + o.bumpuv[3].y*cos(_VortexOffset));
//#endif
				o.pos = mul(UNITY_MATRIX_VP, wpos);
				o.grabuv = ComputeGrabScreenPos(o.pos);
				o.viewDir.xzy = normalize(WorldSpaceViewDir(v.vertex));

				o.uv = float4(v.texcoord.xy, 0, 0);
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
