using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainGenerator : MonoBehaviour {

    public int width = 128;
    public int height = 128;
    public int depth = 20;
    public float scale = 20f;

    public float offsetX = 0f;
    public float offsetY = 0f;
	public float givenOffsetX = 0f;
	public float givenOffsetY = 0f;
    
	void Start () {
        if (width != 0 || height != 0)
        {
            GenerateEverything();
        }


    }

    public void setGenerationData(int w, int h, int d, int s, float offx, float offy)
    {
        width = w;
        height = h;
        depth = d;
        scale = s;
		offsetX = offx;
        offsetY = offy;
		GenerateEverything();

    }

	public void addOffsets(float offx, float offy){
		if(offx > 0)
		{
		offsetX += offx;
		}
		else
		{
		offsetX += offx;
		}

		if(offy > 0)
		{
		offsetY += offy;
		}
		else
		{
		offsetY += offy;
		}

		GenerateEverything();
	}

    public void GenerateEverything()
    {
        // takes random offset so every time the map is different
        //offsetX = Random.Range(0f, 9999f);
        //offsetY = Random.Range(0f, 9999f);
        Terrain terrain = gameObject.GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        GenerateMapTextures(terrain);
        GenerateTerrainDetail(terrain.terrainData);
    }

    private void GenerateMapTextures(Terrain terrain)
    {
        //terrain texture accoring to depth from here
        TerrainData terrainData = terrain.terrainData;
        float maxHeight = GetMaxHeight(terrainData, terrainData.heightmapWidth);
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                //Gets height at this coordinates
                float height = GetHeightAlpha(terrainData, x, y);

                //Erase existing splatmap at this point
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {
                    splatmapData[x, y, i] = 0.0f;
                }

                float[] splatWeights = new float[terrainData.alphamapLayers];
                //assign textures here--------------
                //Currently height is from 0 to depth defined at start
                if (height <= 10)
                {
                    splatWeights[0] = 1;
                    splatWeights[1] = 0;
                    splatWeights[2] = 0;
                }
                else if (height <= 25)
                {
                    splatWeights[0] = 1 - ((height - 10) / 15);
                    splatWeights[1] = (height - 10) / 15;
                    splatWeights[2] = 0;
                }
                else if (height <= 30)
                {
                    splatWeights[0] = 0;
                    splatWeights[1] = 1 - ((height - 25) / 5);
                    splatWeights[2] = (height - 25) / 5;
                }
                else if (height > 30)
                {
                    splatWeights[0] = 0;
                    splatWeights[1] = 0;
                    splatWeights[2] = 1;
                }


                float z = splatWeights.Sum();

                if (Mathf.Approximately(z, 0.0f))
                {
                    splatWeights[0] = 1.0f;
                }

                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {
                    // Normalize so sum = 1
                    splatWeights[i] /= z;

                    splatmapData[x, y, i] = splatWeights[i];
                }
            }
        }
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }

    private float GetHeightAlpha(TerrainData terrainData, int x, int y)
    {
        float y_01 = (float)y / (float)terrainData.alphamapHeight;
        float x_01 = (float)x / (float)terrainData.alphamapWidth;
        //Gets height at this coordinates
        float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapHeight), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));
        return height;
    }

    private float GetHeightDetail(TerrainData terrainData, int x, int y)
    {
        float y_01 = (float)y / (float)terrainData.detailHeight;
        float x_01 = (float)x / (float)terrainData.detailWidth;
        //Gets height at this coordinates
        float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapWidth), Mathf.RoundToInt(x_01 * terrainData.heightmapWidth));
        return height;
    }

    private float GetMaxHeight(TerrainData terrainData, int heightmapWidth)
    {

        float maxHeight = 0f;
        for (int x = 0; x < heightmapWidth; x++)
        {
            for (int y = 0; y < heightmapWidth; y++)
            {
                if (terrainData.GetHeight(x, y) > maxHeight)
                {
                    maxHeight = terrainData.GetHeight(x, y);
                }
            }
        }
        return maxHeight;
    }
    
    //Generates details for terrain (atm only grass)
    void GenerateTerrainDetail(TerrainData terrainData)
    {
        int detailWidth = terrainData.detailWidth;
        int detailHeight = terrainData.detailHeight;
        float maxHeight = GetMaxHeight(terrainData, terrainData.heightmapWidth);

        int[,] details0 = new int[detailWidth, detailHeight];
        int[,] details1 = new int[detailWidth, detailHeight];
        int[,] details2 = new int[detailWidth, detailHeight];
        int[,] details3 = new int[detailWidth, detailHeight];

        int x, y, strength;

        for (x = 0; x < detailWidth; x++)
        {
            for (y = 0; y < detailHeight; y++)
            {
                float height = GetHeightDetail(terrainData, x, y);
                
                if (height <= 10)
                {
                    strength = Random.Range(1, 5);
                    
                    float grassTypeRandom = Random.Range(0, 3);
                    if (grassTypeRandom < 1)
                    {
                        details0[x, y] = strength;
                    }
                    else if (grassTypeRandom < 2)
                    {
                        details1[x, y] = strength;
                    }
                    else
                    {
                        details2[x, y] = strength;
                    }
                }
                else if (height <= 25)
                {
                    int from = 0;
                    int fromTo = Mathf.RoundToInt((height - 10) * 5);
                    strength = Random.Range(1, 5);
                    float grassTypeRandom = Random.Range(from, fromTo);
                    if (grassTypeRandom < 1)
                    {
                        details0[x, y] = strength;
                    }
                    else if (grassTypeRandom < 2)
                    {
                        details1[x, y] = strength;
                    }
                    else if (grassTypeRandom < 3)
                    {
                        details2[x, y] = strength;
                    }
                }
                else if (height > 15)
                {
                    int to = Mathf.RoundToInt((maxHeight - (height - 15)) * 10);
                    float stoneRandom = Random.Range(0, to);
                    if(stoneRandom < 1)
                    {
                        
                        details3[x, y] = 1;
                    }
                }
                
            }
        }
        
        terrainData.SetDetailLayer(0, 0, 0, details0);
        terrainData.SetDetailLayer(0, 0, 1, details1);
        terrainData.SetDetailLayer(0, 0, 2, details2);
        terrainData.SetDetailLayer(0, 0, 3, details3);
    }


    //Generates Terrain land
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

    float AddNoise1(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX + 1000;
        float yCoord = (float)y / height * scale + offsetY + 2000;

        return Mathf.PerlinNoise(xCoord * 0.2f, yCoord * 0.2f);
    }

    float CalculateHeight (int x, int y)
    {
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord * 0.5f, yCoord * 0.5f);
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
