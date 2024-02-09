using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Coin_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private float spawnRate = 3;
    [SerializeField] private float spawnDelay = 0;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer -= spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate)
        { timer += Time.deltaTime; }
        else { 
            timer = 0;
            Instantiate(coin, new Vector3(Random.Range(-7, 7), transform.position.y, transform.position.z), transform.rotation);
        }
    }
}
