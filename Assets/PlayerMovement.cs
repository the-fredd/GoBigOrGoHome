using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float jumpPower = 1;
    private float horizontal;
    private bool alive = true;
    private LogicController game;

    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<LogicController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpPower);
            }

            if (Input.GetButtonUp("Jump") && playerRigid.velocity.y > 0f)
            {
                playerRigid.velocity = new Vector2(playerRigid.velocity.x, playerRigid.velocity.y * 0.5f);
            }
        }
    }
    private void FixedUpdate()
    {
        playerRigid.velocity = new Vector2(horizontal * moveSpeed, playerRigid.velocity.y);

    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            alive = false;
            playerRigid.velocity = new Vector2(0, -5);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            game.GameOver();
        }
    }
}
