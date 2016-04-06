using UnityEngine;
using System.Collections;

public class WaterWaves : MonoBehaviour {
    private Terrain terrain;
    private Vector3 terrainSize;

    public float heightThresh;

	// Use this for initialization
	void Start () {
        terrain = GetComponent<Terrain>();
        terrainSize = terrain.terrainData.size;
        Debug.Log(terrainSize);
        Debug.Log(terrain.terrainData.heightmapHeight);
        Debug.Log(terrain.terrainData.heightmapWidth);
        Debug.Log(terrainSize.z);

        
    }
	
	// Update is called once per frame
	void Update () {
        int rows = terrain.terrainData.heightmapHeight;
        int cols = terrain.terrainData.heightmapWidth;
        float[,] heights = new float[rows, cols];
        for (var i = 0; i < rows; i++)
            for (var j = 0; j < cols; j++)
                heights[i, j] = Random.Range(0.0f, 1.0f) * heightThresh;

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}
