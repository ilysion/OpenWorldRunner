using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private int speed = 5;

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.x += Time.deltaTime;
        if (temp.x >= CloudController.max)
        {
            gameObject.GetComponentInParent<CloudController>().cloudCount--;
            Destroy(this.gameObject);
        }
        transform.position = temp;
    }
}
