using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinScript : MonoBehaviour
{
    //private LogicController game;
    private GameObject player;
    private PlayerMovement plmov;
    // Start is called before the first frame update
    void Start()
    {
        //game = GameObject.FindGameObjectWithTag("GameController").GetComponent<LogicController>();
        player = GameObject.FindGameObjectWithTag("Player");
        plmov = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        { 
            Destroy(gameObject);
            //game.AddScore(); //TODO reactivate
            plmov.StartCoroutine(plmov.Grow());
        }
    }
}
