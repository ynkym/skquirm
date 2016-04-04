using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class WaterControll : MonoBehaviour {

    [Range(0.7f, 0.9f)]
    public float map1StartScale = 0.8f;
    [Range(0.02f, 0.15f)]
    public float map1Range = 0.1f;
    [Range(0.3f, 0.5f)]
    public float map2StartScale = 0.45f;
    [Range(0.1f, 0.3f)]
    public float map2Range = 0.2f;
    [Range(0.1f, 0.2f)]
    public float map3StartScale = 0.15f;
    [Range(0.02f, 0.1f)]
    public float map3Range = 0.07f;
    [Range(0.3f, 0.5f)]
    public float map4StartScale = 0.4f;
    [Range(0.2f, 0.3f)]
    public float map4Range = 0.3f;

    private Vector4 waveHeightScale4;
    private Vector4 heightOffsetClamped;

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Renderer>())
        {
            return;
        }
        Material mat = GetComponent<Renderer>().sharedMaterial;
        if (!mat)
        {
            return;
        }

        // Time since level load, and do intermediate calculations with doubles
        double t = Time.timeSinceLevelLoad / 20.0;

        float map1Scale = map1StartScale + Mathf.Sin((float)t) * map1Range;
        float map2Scale = map2StartScale + Mathf.Sin((float)t) * map2Range;
        float map3Scale = map3StartScale + Mathf.Sin((float)t) * map3Range;
        float map4Scale = map4StartScale + Mathf.Sin((float)t) * map4Range;

        Vector4 waveSpeed = mat.GetVector("WaveSpeed");
        float waveScale = mat.GetFloat("_WaveScale");
        Vector4 waveScale4 = new Vector4(waveScale * map1Scale, waveScale * (map1Scale + 0.02f), waveScale * map2Scale, waveScale * (map2Scale+0.05f));
        //Debug.Log("WaveScale: " + waveScale4);

        Vector4 offsetClamped = new Vector4(
            (float)Math.IEEERemainder(waveSpeed.x * waveScale4.x * t, 1.0),
            (float)Math.IEEERemainder(waveSpeed.y * waveScale4.y * t, 1.0),
            (float)Math.IEEERemainder(waveSpeed.z * waveScale4.z * t, 1.0),
            (float)Math.IEEERemainder(waveSpeed.w * waveScale4.w * t, 1.0)
            );

        Vector4 waveSpeed_2 = mat.GetVector("WaveSpeed2");
        Vector4 waveScale4_2 = new Vector4(waveScale * (map3Scale + 0.05f), waveScale * map3Scale, waveScale * map4Scale, waveScale * map4Scale);
       // Debug.Log("WaveScale2: " + waveScale4_2);
        // Time since level load, and do intermediate calculations with doubles
        Vector4 offsetClamped_2 = new Vector4(
            (float)Math.IEEERemainder(waveSpeed_2.x * waveScale4_2.x * t, 1.0),
            (float)Math.IEEERemainder(waveSpeed_2.y * waveScale4_2.y * t, 1.0),
            (float)Math.IEEERemainder(waveSpeed_2.z * waveScale4_2.z * t, 1.0),
            (float)Math.IEEERemainder(waveSpeed_2.w * waveScale4_2.w * t, 1.0)
            );
       // Debug.Log(offsetClamped_2);

        mat.SetVector("_WaveOffset", offsetClamped);
        mat.SetVector("_WaveScale4", waveScale4);
        mat.SetVector("_WaveOffset_2", offsetClamped_2);
        mat.SetVector("_WaveScale4_2", waveScale4_2);


        Vector4 waveHeightSpeed = mat.GetVector("WaveHeightSpeed");
        float waveHeightScale = mat.GetFloat("_WaveHeightScale");
        waveHeightScale4 = new Vector4(waveHeightScale, waveHeightScale, waveHeightScale*0.4f, waveHeightScale*0.45f);

        heightOffsetClamped = new Vector4(
            (float)Math.IEEERemainder(waveHeightSpeed.x * waveHeightScale4.x * t, 1.0),
            (float)Math.IEEERemainder(waveHeightSpeed.y * waveHeightScale4.y * t, 1.0),
            (float)Math.IEEERemainder(waveHeightSpeed.z * waveHeightScale4.z * t, 1.0),
            (float)Math.IEEERemainder(waveHeightSpeed.w * waveHeightScale4.w * t, 1.0)
            );
       // Debug.Log(heightOffsetClamped);

        mat.SetVector("_WaveHeightScale4", waveHeightScale4);
        mat.SetVector("_WaveHeightOffset", heightOffsetClamped);
    }

    public Vector4 getWaveHeightScale4() { return waveHeightScale4;  }

    public Vector4 getHeightOffsetClamped() { return heightOffsetClamped; }

        
}
