using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    public Terrain terrain;
    public Text scoreText;
    public float respawnRange = 40f;
    public float respawnMaxHeight = 5f;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Relocate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Relocate() {

        // Randomly choose new loaction
        float newX = transform.position.x + Random.Range(-respawnRange, respawnRange);
        float newZ = transform.position.z + Random.Range(-respawnRange, respawnRange);
        Vector3 newPosition = new Vector3(newX, 0, newZ);

        // New height relative to terrain
        float radius = 5f;
        float newY = radius + terrain.SampleHeight(newPosition) + Random.Range(1f, respawnMaxHeight);
        newPosition.y = newY;

        transform.position = newPosition;

        // Also add a random orientation
        transform.eulerAngles = new Vector3(90, 0, Random.Range(-180f, 180f));

    }

    void OnTriggerEnter(Collider other) {

        // When bike hits the target
        score += 1;
        scoreText.text = "Targets Hit: " + score;
        Relocate();

    }
}
