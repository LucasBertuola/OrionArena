using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView pv;
    [SerializeField] private Text playerName;

    public float speed;
    public float jumpForce;
    public float jetForce;
    private float moveInput;
    public bool disableInputs = false;

    private Rigidbody2D rb;
    public GameObject playerCam;
    private Animator anim;
    public Slider fuelSlider;
    public ParticleSystem jetSmoke;

    //public bool facingRight = true;
    private bool isGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    public float fuelAmount;
    public float maxFuel;
    public bool refueling = false;


    private void Awake()
    {
    }

    void Start()
    {
        pv = GetComponent<PhotonView>();
        //playerName.text = pv.Owner.NickName;
        fuelSlider.maxValue = maxFuel;
        fuelAmount = maxFuel;
        fuelSlider.value = fuelAmount;
        jetSmoke.Stop();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (pv.IsMine)
        {
            GameManager.instance.localPlayer = this.gameObject;
            playerCam.SetActive(true);
            playerCam.transform.SetParent(null, false);
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

            if (isGrounded && fuelAmount < maxFuel)
            {
                Refuel();
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetTrigger("Jumping");               
            }

            if (Input.GetKey(KeyCode.Space) && fuelAmount > 0 && !isGrounded)
            {
                rb.velocity = new Vector2(moveInput * speed, jetForce);
                fuelAmount -= 1 * Time.deltaTime;
                fuelSlider.value = fuelAmount;
                if (!jetSmoke.isPlaying)
                    jetSmoke.Play();
            }
            else
            {
                jetSmoke.Stop();
            }
        }

        
    }

    public void Refuel()
    {
        if (!pv.IsMine)
            return;

        if (Input.GetKey(KeyCode.Space) || refueling)
        {
            return;
        }

        if (fuelAmount >= maxFuel)
        {
            fuelAmount = maxFuel;
            return;
        }
        fuelAmount += 2f * Time.deltaTime;
        fuelSlider.value = fuelAmount;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(fuelAmount);
        }
        else
        {
            this.fuelAmount = (float)stream.ReceiveNext();
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
