using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;

    [SerializeField] public PlayerMovement PlayerObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

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
