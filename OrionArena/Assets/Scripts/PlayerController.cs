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
    public bool disableInputs = false;

    private Rigidbody2D rb;
    public GameObject playerCam;
    private Animator anim;

    //public bool facingRight = true;
    private bool isGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    public int extraJumps;
    public int extraJumpsValue;


    private void Awake()
    {
    }

    void Start()
    {
        pv = GetComponent<PhotonView>();
        //playerName.text = pv.Owner.NickName;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (pv.IsMine)
        {
            GameManager.instance.localPlayer = this.gameObject;
            playerCam.SetActive(true);
            playerName.text = PhotonNetwork.NickName;
            playerName.color = Color.green;
            playerName.fontStyle = FontStyle.Bold;
        }
        else
        {
            playerName.text = pv.Owner.NickName;
        }
    }

    void Update()
    {
        if (pv.IsMine && !disableInputs)
        {
            Physics2D.IgnoreLayerCollision(9, 9);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            if (isGrounded)
            {
                extraJumps = extraJumpsValue;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetTrigger("Jumping");
                
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 && !isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetTrigger("Jumping");
                extraJumps--;
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
