using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float followRadius = 6;

    //[SerializeField] public PlayerMovement PlayerObject;
    private GameObject player;
    private PlayerMovement plmov;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        plmov = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Mathf.Abs(Vector2.Distance(transform.position, plmov.transform.position)) < followRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(plmov.transform.position.x, transform.position.y), moveSpeed*1.4f * Time.deltaTime);
        }
        else {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime; 
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 )
        { 
            Destroy(gameObject); 
            //TODO trigger grow, do that in player script
        }
        if (collision.gameObject.layer == 6)
        { moveSpeed = moveSpeed * -1; }
    }
}
