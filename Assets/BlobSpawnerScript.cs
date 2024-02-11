using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject blob;
    private float spawnRate = 20;
    private float EnemyNumber = 1;
    //[SerializeField] private float OffsetX = 0;
    //[SerializeField] private float OffsetY;
    private float spawnedNumber = 0;
    private float spawnDelay = 0;
    private float timer;
    //private Vector3 spawnPosition;
    //private Vector3 newspawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnedNumber = 0;
        timer = spawnRate - spawnDelay;
        //spawnPosition = transform.position;
        //spawnPosition.y += OffsetY;
        //newspawnPosition = spawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;


            if (spawnedNumber < EnemyNumber)
            {
                //newspawnPosition.x = spawnPosition.x + 5 + Random.Range(-7, 9);
                Instantiate(blob, transform.position, transform.rotation);
                spawnedNumber++;
            }
        }
    }
}
