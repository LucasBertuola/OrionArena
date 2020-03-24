using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PhotonView pv;
    [SerializeField] private Text playerName;

    public float speed;
    public float jumpForce;
    private float moveInput;


    private Rigidbody2D rb;

    //public bool facingRight = true;
    private bool isGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    private int extraJumps;
    public int extraJumpsValue;


    void Start()
    {
        pv = GetComponent<PhotonView>();
        playerName.text = pv.Owner.NickName;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (pv.IsMine)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            playerName.color = Color.green;
            playerName.fontStyle = FontStyle.Bold;
        }


    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (isGrounded)
            {
                extraJumps = extraJumpsValue;
            }

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }

        
    }

    /*public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }*/
}
