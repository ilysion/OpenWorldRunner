using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public int cloudMinX = 30;
    public int cloudMaxX = 100;
    public int cloudMinZ = 35;
    public int cloudMaxY = 80;

    public GameObject cloudPrefab;

    public int cloudCount = 0;

    public static int max = 1024;

    public void Update()
    {
        if (!GameController.cloudsEnabled)
            return;
        if (cloudCount == 0)
            generateCloud();
    }

    public void generateCloud()
    {
        if (!GameController.cloudsEnabled)
            return;
        float xStart = gameObject.transform.position.x;
        float zStart = gameObject.transform.position.z;
        float xMax = xStart + gameObject.GetComponentsInChildren<TerrainGenerator>()[0].height;
        float zMax = zStart + gameObject.GetComponentsInChildren<TerrainGenerator>()[0].width;
        float cloudZMaxTill = 0;
        float cloudZTotal = zStart;
        while (true)
        {
            if (cloudZTotal >= zMax) break;
            float cloudXTotal = xStart;
            for (int i = 0; i <= 5; i++)
            {
                int cloudChance = Random.Range(0, 6);
                int cloudX = Random.Range(cloudMinX, cloudMaxX);
                int cloudZ = Random.Range(cloudMinZ, cloudMaxY);
                if (cloudXTotal + cloudX >= xMax || cloudZTotal + cloudZ >= zMax) break;
                cloudXTotal += cloudX;
                Collider[] intersecting = Physics.OverlapBox(new Vector3(cloudXTotal, 100, cloudZTotal + cloudZ), new Vector3(cloudX, 4.5f, cloudZ) / 2);
                if (cloudChance == 0 && intersecting.Length == 0)
                {
                    cloudCount++;
                    GameObject temp = Instantiate(cloudPrefab, this.gameObject.transform);
                    temp.transform.position = new Vector3(cloudXTotal, 100, cloudZTotal + cloudZ);
                    temp.transform.localScale = new Vector3(cloudX, 4.5f, cloudZ);
                    if (cloudZ > cloudZMaxTill) cloudZMaxTill = cloudZ;
                }
            }
            cloudZTotal += cloudZMaxTill;
        }
    }
}
