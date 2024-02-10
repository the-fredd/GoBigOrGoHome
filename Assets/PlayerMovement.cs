using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
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
    public float growTime = 1f; //speed of growth
    public float maxSize = 2f; //maximum size after growth
    public float minSize = 0.5f; //maximum size after growth
    public float medSize = 1f;
    public float sizePenalty = 0.75f;
    public bool isMaxSize = false;
    public bool isMinSize = false;
    public bool isMedSize = true;
    private float moveSpeedForSize;
    private float jumpPowerForSize;

    [SerializeField] private Rigidbody2D playerRigid;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform topCheck;
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
                jumpPowerForSize = jumpPower * (sizePenalty*(0.6f+0.4f*Mathf.Max(transform.localScale.x, transform.localScale.y)));
                playerRigid.velocity = new Vector2(playerRigid.velocity.x, jumpPowerForSize);
            }

            if (Input.GetButtonUp("Jump") && playerRigid.velocity.y > 0f)
            {
                playerRigid.velocity = new Vector2(playerRigid.velocity.x, playerRigid.velocity.y * 0.5f);
            }

            if (Input.GetButtonUp("Fire1") && (isMinSize || isMedSize))
            {
                StartCoroutine(Grow());
            }

            if (Input.GetButtonUp("Fire2") && (isMaxSize || isMedSize))
            {
                StartCoroutine(Shrink());
            }
        }
    }
    private void FixedUpdate()
    {
        moveSpeedForSize = moveSpeed /(1.5f*sizePenalty*Mathf.Max(transform.localScale.x, transform.localScale.y));
        playerRigid.velocity = new Vector2(horizontal * moveSpeedForSize, playerRigid.velocity.y);

    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsSquashed()
    {
        return Physics2D.OverlapCircle(topCheck.position, 0.2f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7 || (collision.gameObject.layer == 6 && IsGrounded() && (IsSquashed())))
        {
            alive = false;
            playerRigid.velocity = new Vector2(0, -5);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            game.GameOver();
        }
    }

    public IEnumerator Grow()
    {
        Vector2 startScale = transform.localScale;

        float size = 2;
        Vector2 maxScale = new Vector2(maxSize, maxSize);
        if (isMinSize)
        {
            maxScale = new Vector2(medSize, medSize);
            size = 1;
        }

        do
        {
            if (isMedSize) { isMedSize = false; }
            if (isMinSize) { isMinSize = false; }
            transform.localScale = Vector3.Lerp(startScale, maxScale, growTimer / growTime);
            growTimer += Time.deltaTime;
            yield return null;  //i think this ends function
        }
        while (growTimer<growTime);
        growTimer = 0;
        isMaxSize = true;
        if (size == 1) { isMedSize = true; }
        if (size == 2) { isMaxSize = true; }
    }

    public IEnumerator Shrink()
    {
        Vector2 startScale = transform.localScale;

        float size = 2;
        Vector2 minScale = new Vector2(minSize, minSize);
        if (isMaxSize){
            minScale = new Vector2(medSize, medSize);
            size = 3;
        }
        
        do
        {
            if (isMaxSize) { isMaxSize = false; }
            if (isMedSize) { isMedSize = false; }
            transform.localScale = Vector3.Lerp(startScale, minScale, growTimer / growTime);
            growTimer += Time.deltaTime;
            yield return null;  //i think this ends function
        }
        while (growTimer < growTime);
        growTimer = 0;
        if (size == 2) { isMinSize = true; }
        if (size == 3) { isMedSize = true; }
    }


}

