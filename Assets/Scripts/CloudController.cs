using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public int cloudMinX = 30;
    public int cloudMaxX = 100;
    public int cloudMinZ = 35;
    public int cloudMaxY = 80;

    public TerrainGenerator generator;

    public GameObject cloudPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int xStart = 0;
        int zStart = 0;
        int xMax = xStart + generator.height;
        int zMax = zStart + generator.width;

        int cloudZMaxTill = 0;
        int cloudZTotal = 0;
        while (true)
        {
            if (cloudZTotal >= zMax) break;
            int cloudXTotal = 0;
            for (int i = 0; i <= 100; i++)
            {
                int cloudChance = Random.Range(0, 4);
                int cloudX = Random.Range(cloudMinX, cloudMaxX);
                int cloudZ = Random.Range(cloudMinZ, cloudMaxY);
                if (cloudXTotal + cloudX >= xMax || cloudZTotal + cloudZ >= zMax) break;
                cloudXTotal += cloudX;
                if (cloudChance == 0)
                {
                    GameObject temp = Instantiate(cloudPrefab, this.gameObject.transform);
                    temp.transform.position = new Vector3(cloudXTotal, 100, cloudZTotal + cloudZ);
                    temp.transform.localScale = new Vector3(cloudX, 4.5f, cloudZ);
                    if (cloudZ > cloudZMaxTill) cloudZMaxTill = cloudZ;
                }
            }
            cloudZTotal += cloudZMaxTill;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
