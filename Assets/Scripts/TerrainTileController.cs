using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainTileController : MonoBehaviour
{


    //    NW   N   NE   +
    //
    //     W   C   E    Z
    //                  
    //    SW   S   SE   -
    //      -  X  +

    //CenterTerrain - this gameobject that the script is attached to
    public GameObject _player;

    private PlayerController playerController;
    private Terrain Center, N, S, E, W, NE, SE, SW, NW;

    /// <summary>
    /// 0=Center, 1=N, 2=S, 3=E, 4=W, 5=NE, 6=NW, 7=SE, 8=SW
    /// </summary>
    private int _ret; //the tile occupied by the player
    private int tw, cDepth, cScale; //terrain width
	private float centerOffsetX, centerOffsetY;

    private void Awake()
    {
        playerController = _player.GetComponent<PlayerController>();
    }

    void Start()
    {
        Center = gameObject.GetComponent<Terrain>();
        N = gameObject.transform.GetChild(0).GetComponent<Terrain>();
        S = gameObject.transform.GetChild(1).GetComponent<Terrain>();
        E = gameObject.transform.GetChild(2).GetComponent<Terrain>();
        W = gameObject.transform.GetChild(3).GetComponent<Terrain>();
        NE = gameObject.transform.GetChild(4).GetComponent<Terrain>();
        SE = gameObject.transform.GetChild(5).GetComponent<Terrain>();
        SW = gameObject.transform.GetChild(6).GetComponent<Terrain>();
        NW = gameObject.transform.GetChild(7).GetComponent<Terrain>();
        
        tw = (int)Center.terrainData.size.x;
        cDepth = (int)Center.GetComponent<TerrainGenerator>().depth;
        cScale = (int)Center.GetComponent<TerrainGenerator>().scale;

		centerOffsetX = Center.GetComponent<TerrainGenerator>().offsetX;
		centerOffsetY = Center.GetComponent<TerrainGenerator>().offsetY;

        SetTilesAtStart();
    }

    void Update()
    {
        UpdateChunks();
    }

    private void UpdateChunks()
    {
        _ret = -1;
        //this first check is not really necessary:
        if (_ret == -1 && _player.transform.position.x >= Center.transform.position.x &&
            _player.transform.position.x <= Center.transform.position.x + tw &&
            _player.transform.position.z >= Center.transform.position.z &&
            _player.transform.position.z <= Center.transform.position.z + tw)
        {
            //we're inside the center
            _ret = 0;
        }
        else if (_ret == -1 && _player.transform.position.x >= N.transform.position.x &&
             _player.transform.position.x <= N.transform.position.x + tw &&
             _player.transform.position.z >= N.transform.position.z &&
             _player.transform.position.z <= N.transform.position.z + tw)
        {
            //we're inside the N
            _ret = 1;
        }
        else if (_ret == -1 && _player.transform.position.x >= S.transform.position.x &&
             _player.transform.position.x <= S.transform.position.x + tw &&
             _player.transform.position.z >= S.transform.position.z &&
             _player.transform.position.z <= S.transform.position.z + tw)
        {
            //we're inside the S
            _ret = 2;
        }
        else if (_ret == -1 && _player.transform.position.x >= E.transform.position.x &&
            _player.transform.position.x <= E.transform.position.x + tw &&
            _player.transform.position.z >= E.transform.position.z &&
            _player.transform.position.z <= E.transform.position.z + tw)
        {
            //we're inside the E
            _ret = 3;
        }
        else if (_ret == -1 && _player.transform.position.x >= W.transform.position.x &&
            _player.transform.position.x <= W.transform.position.x + tw &&
            _player.transform.position.z >= W.transform.position.z &&
            _player.transform.position.z <= W.transform.position.z + tw)
        {
            //we're inside the W
            _ret = 4;
        }
        else if (_ret == -1 && _player.transform.position.x >= NE.transform.position.x &&
            _player.transform.position.x <= NE.transform.position.x + tw &&
            _player.transform.position.z >= NE.transform.position.z &&
            _player.transform.position.z <= NE.transform.position.z + tw)
        {
            //we're inside the NE
            _ret = 5;
        }
        else if (_ret == -1 && _player.transform.position.x >= NW.transform.position.x &&
            _player.transform.position.x <= NW.transform.position.x + tw &&
            _player.transform.position.z >= NW.transform.position.z &&
            _player.transform.position.z <= NW.transform.position.z + tw)
        {
            //we're inside the NW
            _ret = 6;
        }
        else if (_ret == -1 && _player.transform.position.x >= SE.transform.position.x &&
            _player.transform.position.x <= SE.transform.position.x + tw &&
            _player.transform.position.z >= SE.transform.position.z &&
            _player.transform.position.z <= SE.transform.position.z + tw)
        {
            //we're inside the SE
            _ret = 7;
        }
        else if (_ret == -1 && _player.transform.position.x >= SW.transform.position.x &&
            _player.transform.position.x <= SW.transform.position.x + tw &&
            _player.transform.position.z >= SW.transform.position.z &&
            _player.transform.position.z <= SW.transform.position.z + tw)
        {
            //we're inside the SW
            _ret = 8;
        }

        
        if (_ret != 0)
        {
            //move the tiles
            switch (_ret)
            {
                case 1: //N
                    Center.transform.position = N.transform.position;
                    //movedNorth();
                    editTileTerrains(20f, 0f);
                    break;
                case 2: //S
                    Center.transform.position = S.transform.position;
					editTileTerrains(-20f, 0f);
                    break;
                case 3: //E
                    Center.transform.position = E.transform.position;
					editTileTerrains(0f, 20f);
                    CloudController.max = 1536;
                    break;
                case 4: //W
                    Center.transform.position = W.transform.position;
					editTileTerrains(0f, -20f);
                    CloudController.max = 512;
                    break;
                case 5: //NE
                    Center.transform.position = NE.transform.position;
					editTileTerrains(20f, 20f);
                    CloudController.max = 1536;
                    break;
                case 6: //NW
                    Center.transform.position = NW.transform.position;
					editTileTerrains(-20f, 20f);
                    CloudController.max = 512;
                    break;
                case 7: //SE
                    Center.transform.position = SE.transform.position;
					editTileTerrains(20f, -20f);
                    CloudController.max = 1536;
                    break;
                case 8: //SW
                    Center.transform.position = SW.transform.position;
					editTileTerrains(-20f, -20f);
                    CloudController.max = 512;
                    break;
            }
            //generate new tile data


        }
    }

    private void SetTilesAtStart()
    {
        //offset goes +- 20 because of the scale


        N.transform.position = Center.transform.position + new Vector3(0,   0,  tw);
        N.transform.GetComponentsInChildren<TerrainGenerator>()[0].setGenerationData
		(tw, tw, cDepth, cScale, centerOffsetX + 20, centerOffsetY);

        S.transform.position = Center.transform.position + new Vector3(0,   0, -tw);
        S.transform.GetComponentsInChildren<TerrainGenerator>()[0].setGenerationData
		(tw, tw, cDepth, cScale,centerOffsetX - 20, centerOffsetY);

        E.transform.position = Center.transform.position + new Vector3( tw, 0,  0);
        E.transform.GetComponentsInChildren<TerrainGenerator>()[0].setGenerationData
		(tw, tw, cDepth, cScale, centerOffsetX, centerOffsetY + 20);

        W.transform.position = Center.transform.position + new Vector3(-tw, 0,  0);
        W.transform.GetComponentsInChildren<TerrainGenerator>()[0].setGenerationData
		(tw, tw, cDepth, cScale, centerOffsetX, centerOffsetY - 20);

        NE.transform.position = Center.transform.position + new Vector3( tw, 0,  tw);
        NE.transform.GetComponentsInChildren<TerrainGenerator>()[0].setGenerationData
		(tw, tw, cDepth, cScale,centerOffsetX + 20, centerOffsetY + 20);

        SE.transform.position = Center.transform.position + new Vector3( tw, 0, -tw);
        SE.transform.GetComponentsInChildren<TerrainGenerator>()[0].setGenerationData
		(tw, tw, cDepth, cScale,centerOffsetX - 20, centerOffsetY + 20);

        SW.transform.position = Center.transform.position + new Vector3(-tw, 0,  tw);
        SW.transform.GetComponentsInChildren<TerrainGenerator>()[0].setGenerationData
		(tw, tw, cDepth, cScale,centerOffsetX + 20, centerOffsetY - 20);

        NW.transform.position = Center.transform.position + new Vector3(-tw, 0, -tw);
        NW.transform.GetComponentsInChildren<TerrainGenerator>()[0].setGenerationData
		(tw, tw, cDepth, cScale,centerOffsetX - 20, centerOffsetY - 20);

        if (GameController.cloudsEnabled)
        {
            N.GetComponents<CloudController>()[0].generateCloud();
            S.GetComponents<CloudController>()[0].generateCloud();
            E.GetComponents<CloudController>()[0].generateCloud();
            NE.GetComponents<CloudController>()[0].generateCloud();
            SE.GetComponents<CloudController>()[0].generateCloud();
            SW.GetComponents<CloudController>()[0].generateCloud();
            Center.GetComponents<CloudController>()[0].generateCloud();
            NW.GetComponents<CloudController>()[0].generateCloud();
        }
    }

	private void editTileTerrains(float a, float b)
	{
		Center.GetComponent<TerrainGenerator>().addOffsets(a, b);

        N.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(a, b);
        S.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(a, b);
        E.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(a, b);
        W.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(a, b);

        NE.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(a, b);
        SE.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(a, b);
        SW.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(a, b);
        NW.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(a, b);
	}

	//    NW   N   NE   +
    //
    //     W   C   E    Z
    //                  
    //    SW   S   SE   -
    //      -  X  +

	private void movedNorth()
	{
		TerrainData WData = W.terrainData;
		TerrainData CData = Center.terrainData;
		TerrainData EData = E.terrainData;
		TerrainData NWData = NW.terrainData;
		TerrainData NData = N.terrainData;
		TerrainData NEData = NE.terrainData;

		W.terrainData = NWData;
		Center.terrainData = NData;
		E.terrainData = NEData;
		SW.terrainData = WData;
		S.terrainData = CData;
		SE.terrainData = EData;

		
		//new data
		NW.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(20f, 0f);
		N.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(20f, 0f);
		NE.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsets(20f, 0f);


		Center.GetComponent<TerrainGenerator>().addOffsetsWOGeneration(20f, 0f);
        S.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsetsWOGeneration(20f, 0f);
        E.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsetsWOGeneration(20f, 0f);
        W.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsetsWOGeneration(20f, 0f);
        SE.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsetsWOGeneration(20f, 0f);
        SW.transform.GetComponentsInChildren<TerrainGenerator>()[0].addOffsetsWOGeneration(20f, 0f);
        
	}

}
