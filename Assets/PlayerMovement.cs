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

    //variables for growing
    public float growTimer = 0;
    public float growTime = 6f;
    public float maxSize = 2f;
    public bool isMaxSize = false;

    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        if(isMaxSize== false)
        { 
            StartCoroutine(Grow());
        }
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

    private IEnumerator Grow()
    {
        Vector2 startScale = transform.localScale;
        Vector2 maxScale = new Vector2(maxSize, maxSize);

        do
        {
            transform.localScale = Vector3.Lerp(startScale, maxScale, growTimer / growTime);
            growTimer += Time.deltaTime;
            yield return null;  //i think this ends function
        }
        while (growTimer<growTime);

        isMaxSize = true;
    }
}
