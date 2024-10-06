using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [Space(5)]
    [SerializeField] private float runMaxSpeed;
    [SerializeField] private float runAcceleration;
    [SerializeField] private float runDecceleration;
    private float acceleration;
    private float decceleration;
    [Space(5)]
    [Range(0f, 1)] [SerializeField] private float accelerationInAir;
    [Range(0f, 1)] [SerializeField] private float deccelerationInAir;
    [Space(5)]
    [SerializeField] private bool conserveMomentum;

    [Space(20)]
    [Header("Jump")]
    [Space(5)]

    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpTimeToApex;
    private float jumpForce;
    [Space(5)]
    [SerializeField] private float jumpCutGravityMultiplier;
    [Range(0f, 1)] [SerializeField] private float jumpHangGravityMultiplier;
    [SerializeField] private float jumpHangTimeTreshold;
    [Space(0.5f)]
    [SerializeField] private float jumpHangAccelerationMultiplier;
    [SerializeField] private float jumpHangMaxSpeedMultiplier;

    [Space(5)]
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isJumpFalling;
    [SerializeField] private bool isJumpCut;
    [SerializeField] private bool jumpInputReleased;
    [Space(5)]
    [SerializeField] private float lastGroundedTime;
    [SerializeField] private float lastJumpTime;

    [Space(20)]
    [Header("Gravity")]
    [Space(5)]
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float maxFallSpeed;
    [Space(5)]
    [SerializeField] private float fastFallGravityMultiplier;
    [SerializeField] private float maxFastFallSpeed;
    private float gravityScale;

    [Space(20)]
    [Header("Checks")]
    [SerializeField] private bool grounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
    //[SerializeField] private PlayerLadderMovement plm;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Header("Reset")]
    [SerializeField] private float maxTime;
    [SerializeField] private float timeToReset;

    [Header("Timers")]
    [Range(0.01f, 0.5f)] [SerializeField] private float jumpCoyoteTime;
    [Range(0.01f, 0.5f)] [SerializeField] private float jumpBufferTime;

    [Header("Surface Aligment")]
    [SerializeField] private float rayLength;// Longitud del raycast
    [SerializeField] private float rotationSpeed; // Velocidad de suavizado de la rotación
    [SerializeField] private AnimationCurve animCurve;

    [Header("Shape")]
    [SerializeField] private int playerShape = 0;
    public string PlayerShape = "Earth";
    public GameObject earthShape;
    public GameObject airShape;

    private int playerMaxShapes = 2;


    //Input handler
    private Vector2 moveInput;


    public bool Grounded { get => grounded; set => grounded = value; }

    void Start()
    {
        timeToReset = 0;
        maxTime = 3;
        rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();
        // Mueve el centro de masa hacia abajo para dar la sensación de que la parte inferior es más pesada
        Vector2 newCenterOfMass = new Vector2(0, -0.5f); // Ajusta según el tamaño de tu sprite
        rb.centerOfMass = newCenterOfMass;
        //plm = GetComponent<PlayerLadderMovement>();
        if(playerShape == 0)
        {
            SetValues();
        }
        else
        {
            SetAirValues();
        }
        
    }


    bool getFlipX()
    {
        if (transform.localScale.x > 0) return true;
        else return false;
    }



    private void Update()
    {
        //AlignToGround();
        #region Timer
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
        #endregion

        #region Input Handler
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.x < 0 && getFlipX()) transform.localScale = new Vector3(Math.Abs(transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);
        if (moveInput.x > 0 && !getFlipX()) transform.localScale = new Vector3(Math.Abs(transform.localScale.x) , transform.localScale.y, transform.localScale.z);

        if (Input.GetButtonDown("Jump"))
        {
            print("saltito");
            OnJump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            OnJumpUp();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeShape();
            //KillPlayer();
        }
        #endregion

        #region Grounded
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer) && !isJumping)
        {
            Grounded = true;
            //if (rb.velocity.y < 0) plm.IsClimbing = false;
            lastGroundedTime = jumpCoyoteTime;
        }
        else { Grounded = false; }
        #endregion

        switch (playerShape)
        {
            case 0:
                EarthController();
                break;
            case 1:
                AirController();
                break;



        }
        
        

        //LimitCarRotation();
    }

    private void ChangeShape()
    {
        playerShape++;
        if (playerShape >= playerMaxShapes)
        {
            playerShape = 0;
        }
        switch (playerShape)
        {
            case 0:
                SetValues();
                break;
            case 1:
                SetAirValues();
                break;



        }
    }
    private void EarthController()
    {


        #region Jump
        if (isJumping && rb.velocity.y < 0)
        {
            isJumping = false;
            isJumpFalling = true;
        }

        if (lastGroundedTime > 0 && !isJumping)
        {
            isJumpCut = false;
            if (!isJumping)
                isJumpFalling = true;
        }

        //if (lastGroundedTime > 0 && lastJumpTime > 0 && !isJumping && !plm.IsLadder)
        if (lastGroundedTime > 0 && lastJumpTime > 0 && !isJumping)
        {
            print("ejecuta salto");
            isJumping = true;
            isJumpCut = false;
            isJumpFalling = false;
            Jump();
        }
        #endregion


        #region Jump Gravity
        //if (!plm.IsLadder)
        if (true)
        {
            if (rb.velocity.y < 0 && moveInput.y < 0)
            {
                rb.gravityScale = gravityScale * fastFallGravityMultiplier;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFastFallSpeed));
            }
            else if (isJumpCut)
            {
                rb.gravityScale = gravityScale * jumpCutGravityMultiplier;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
            }
            else if ((isJumping || isJumpFalling) && Mathf.Abs(rb.velocity.y) > jumpHangTimeTreshold)
            {
                rb.gravityScale = gravityScale * jumpHangGravityMultiplier;
            }
            else if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravityScale * fallGravityMultiplier;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
            }
            else
            {
                rb.gravityScale = 1.1f;
            }
        }

        #endregion

        #region Falling
        if (transform.position.y <= -500)
        {
            print("reset level");
            //LevelManager.instance.ResetLevel();
        }
        #endregion

        #region Reset
        if (timeToReset >= maxTime)
        {
            timeToReset = 0;
            //LevelManager.instance.ResetLevel();
            print("reset level");
        }
        #endregion



    }


    private void AirController()
    {
        
    }
    private void KillPlayer()
    {
        //LevelManager.instance.ResetLevel();
        print("reset level");
    }

    private void FixedUpdate()
    {
        #region Movement

        //if (!plm.IsClimbing)
        if (!(playerShape==1&&grounded))
        {
            float targetSpeed = moveInput.x * runMaxSpeed;
            //print("target speed: " + targetSpeed);
            targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, 1);
            //print("target speed2: " + targetSpeed);

            float accelRate;
            if (lastGroundedTime > 0)
            {
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
            }
            else
            {
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration * accelerationInAir : decceleration * deccelerationInAir;
            }
            //print("accelrate: " + accelRate);

            if ((isJumping || isJumpFalling) && Mathf.Abs(rb.velocity.y) > jumpHangTimeTreshold)
            {
                accelRate *= jumpHangAccelerationMultiplier;
                targetSpeed *= jumpHangMaxSpeedMultiplier;
            }

            if (conserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && lastGroundedTime < 0)
            {
                accelRate = 0;
            }

            float speedDif = targetSpeed - rb.velocity.x;

            float movement = speedDif * accelRate;
            //print(movement);
            if(playerShape == 1)
            {
                if (grounded)
                {
                    movement = 0;
                    print("no move");
                }
            }


            rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }
        #endregion
    }

    private void Jump()
    {
        lastJumpTime = 0;
        lastGroundedTime = 0;


        //if (AudioManager.Instance.gameObject != null)
        //{
            //AudioManager.Instance.PlaySFX(2);
        //}
        float force = jumpForce;
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    private void OnJump()
    {
        lastJumpTime = jumpBufferTime;
    }

    private void OnJumpUp()
    {
        //if (rb.velocity.y > 0 && isJumping)
        //{
        //    rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        //}
        //jumpInputReleased = true;
        //lastJumpTime = 0;
        if (isJumping && rb.velocity.y > 0)
        {
            isJumpCut = true;
        }
    }

    private void SetValues()
    {
        //sr.sprite = earth;
        float gravityStrength = (-(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex))*3;
        gravityScale = gravityStrength / Physics2D.gravity.y;
        acceleration = (50 * runAcceleration) / runMaxSpeed;
        decceleration = (50 * runDecceleration) / runMaxSpeed;
        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;
        rb.gravityScale = gravityScale;
        PlayerShape = "Earth";
        airShape.SetActive(false);
        earthShape.SetActive(true);
    }
    private void SetAirValues()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = .02f;
        PlayerShape = "Air";
        earthShape.SetActive(false);
        airShape.SetActive(true);
        //sr.sprite = air;
    }

    private void OnDrawGizmos()
    {
        // Dibuja el Raycast para visualización en la ventana de Scene
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }
    void LimitCarRotation()
    {
        // Limita el ángulo de rotación en el eje Z para evitar que se voltee
        float zRotation = rb.rotation;  // Obtén la rotación actual en el eje Z

        // Convierte el ángulo a un rango de -180 a 180 grados para un control más preciso
        if (zRotation > 180)
        {
            zRotation -= 360;
        }

        // Restringe la rotación al rango definido (-maxTiltAngle a maxTiltAngle)
        zRotation = Mathf.Clamp(zRotation, -45, 45);

        // Aplica la rotación limitada directamente al Rigidbody2D
        rb.rotation = zRotation;
    }

}
