using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using Photon.Pun.UtilityScripts;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView pv;
    [SerializeField] private Text playerName;

    public float speed;
    public float jumpForce;
    public float jetForce;
    private float moveInput;
    public bool disableInputs = false;

    public string myName;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    public GameObject playerCam;
    private Animator anim;
    public Slider fuelSlider;
    public ParticleSystem jetSmoke;

    public int killspread = 0;
    //public bool facingRight = true;
    //private bool isGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    public float fuelAmount;
    public float maxFuel;
    public bool refueling = false;

    public AudioClip jetSound;
    public AudioClip noFueljetSound;

    public Light2D flashlight;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        //playerName.text = pv.Owner.NickName;
        fuelSlider.maxValue = maxFuel;
        fuelAmount = maxFuel;
        fuelSlider.value = fuelAmount;
        jetSmoke.Stop();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        if (pv.IsMine)
        {
            GameManager.instance.localPlayer = this.gameObject;
            playerCam.SetActive(true);
            playerCam.transform.SetParent(null, false);
            playerName.text = PhotonNetwork.NickName;
            playerName.color = Color.green;
            playerName.fontStyle = FontStyle.Bold;
            myName = PhotonNetwork.NickName;
            pv.RPC("SetFlashlight", RpcTarget.AllBuffered, false);

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

            //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

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

            if (IsGrounded() && fuelAmount < maxFuel)
            {
                Refuel();
            }

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetTrigger("Jumping");               
            }

            if (Input.GetKey(KeyCode.Space) && fuelAmount > 0 && !IsGrounded())
            {
                rb.velocity = new Vector2(moveInput * speed, jetForce);
                fuelAmount -= 1 * Time.deltaTime;
                fuelSlider.value = fuelAmount;
                if (!jetSmoke.isPlaying)
                {
                    jetSmoke.Play();
                    pv.RPC("PlayJetpackSound",RpcTarget.AllBuffered);
                }

              
            }
            else
            {
                jetSmoke.Stop();
                StopJetpackSound();
            }

            if (Input.GetKeyDown(KeyCode.F) && flashlight.enabled == true)
            {
                pv.RPC("SetFlashlight", RpcTarget.AllBuffered, false);
            }
            else if (Input.GetKeyDown(KeyCode.F) && flashlight.enabled == false)
            {
                pv.RPC("SetFlashlight", RpcTarget.AllBuffered, true);
            }

            if(fuelAmount <= 0 && !IsGrounded())
            {
                    pv.RPC("StopJetpackSound", RpcTarget.AllBuffered);
                    pv.RPC("PlayNoFuelSound", RpcTarget.AllBuffered);

            }
            if (fuelAmount > 0)
            {
                notSoundfuel = true;
            }

           
        }
    }
    bool notSoundfuel;
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

    private bool IsGrounded()
    {
        float extraHeight = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);
        return raycastHit.collider != null;
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

    [PunRPC]
    public void SetFlashlight(bool value)
    {
        flashlight.enabled = value;
    }

  AudioSource audioRPC;
    [PunRPC]
    public void PlayJetpackSound()
    {
        if(!audioRPC)
            audioRPC = gameObject.AddComponent<AudioSource>();

        audioRPC.clip = jetSound;
        audioRPC.volume = 0.2f;

        if(!audioRPC.isPlaying)
            audioRPC.Play();

        

    }


    [PunRPC]
    public void StopJetpackSound()
    {
        if(!audioRPC)
            audioRPC = gameObject.AddComponent<AudioSource>();

        audioRPC.Stop();
    }

    [PunRPC]
    public void PlayNoFuelSound()
    {
        if (notSoundfuel)
        {
            AudioSource audioRPC = gameObject.AddComponent<AudioSource>();
            audioRPC.clip = noFueljetSound;
            audioRPC.volume = 0.1f;
            if (!audioRPC.isPlaying)
                audioRPC.Play();
            notSoundfuel = false;
        }
    }

    [PunRPC]
    public void Disable(bool dis)
    {
        disableInputs = dis;
    }
  
}
