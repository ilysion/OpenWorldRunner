using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public int width = 1024;
    public int height = 1024;
    public int depth = 20;
    public float scale = 20f;

    public float offsetX = 1000f;
    public float offsetY = 1000f;
    
	void Start () {
        // takes random offset so every time the map is different
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        GenerateTerrainDetail(terrain.terrainData) ;

    }

    void GenerateTerrainDetail(TerrainData terrainData)
    {
        int detailWidth = terrainData.detailWidth;
        int detailHeight = terrainData.detailHeight;

        int[,] details0 = new int[detailWidth, detailHeight];
        int[,] details1 = new int[detailWidth, detailHeight];
        int[,] details2 = new int[detailWidth, detailHeight];

        int x, y, strength;

        for (x = 0; x < detailWidth; x++)
        {
            for (y = 0; y < detailHeight; y++)
            {
                strength = Random.Range(1, 4);

                float grassTypeRandom = Random.Range(0, 3);
                if (grassTypeRandom < 1)
                {
                    details0[y, x] = strength;
                }
                else if (grassTypeRandom < 2)
                {
                    details1[y, x] = strength;
                }
                else
                {
                    details2[y, x] = strength;
                }
            }
        }

        terrainData.SetDetailLayer(0, 0, 0, details0);
        terrainData.SetDetailLayer(0, 0, 1, details1);
        terrainData.SetDetailLayer(0, 0, 2, details2);
    }



TerrainData GenerateTerrain(TerrainData terraindata)
    {
        terraindata.heightmapResolution = width + 1;
        terraindata.size = new Vector3(width, depth, height);

        terraindata.SetHeights(0, 0, GenerateHeights());


    return terraindata;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }

        }
        return heights;
    }

    float CalculateHeight (int x, int y)
    {
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    //at the moment is used at 0 - 256 x
    float CalculateTestHeight(int x, int y)
    {
        float depthMultiplier = 0.005f;

        float xCoord = (depthMultiplier * x) * (float)x / width * scale + offsetX;
        float yCoord = (depthMultiplier * y) * (float)y / height * scale + offsetY;
             
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

}
