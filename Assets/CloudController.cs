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

    // Start is called before the first frame update
    void Start()
    {
        int cloudXTotal = 0;
        int cloudZTotal = 0;
        int cloudZPrevious = 0;
        for(int i = 0; i <= 100; i++)
        {
            int cloudChance = Random.Range(0, 4);
            int cloudX = Random.Range(cloudMinX, cloudMaxX);
            int cloudZ = Random.Range(cloudMinZ, cloudMaxY);
            if (cloudChance == 0)
            {
                GameObject temp = Instantiate(cloudPrefab, this.gameObject.transform);
                temp.transform.position = new Vector3(cloudXTotal, 100, cloudZTotal);
                temp.transform.localScale = new Vector3(cloudX, 4.5f, cloudZ);
            }
            cloudXTotal += cloudX;
            cloudZPrevious = cloudZ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
