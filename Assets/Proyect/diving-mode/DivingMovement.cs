using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DivingMovement : MonoBehaviour
{
    private int direction = 1;
    //movement
    public float waterSpeed;
    [SerializeField]bool isInWater;
    // rotation
    private float currentRotationZ = 0f;
    public float rotationSpeed;
    public float maxRotationZ;


    [SerializeField] private float currentSpeed = 0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        currentSpeed = 0f;
        //under_water = false;
    }
    private void Start()
    {
        isInWater = false;
        rb.gravityScale = 1;
        rb.simulated = true;
    }

    private bool isGrounded; // Verifica si está en el suelo
    private bool canJump = false; // Verifica si se puede saltar
   
    void Update()
    {

        HandleRotation();

        if (canJump && isGrounded)
        {
    
        }
    }
    void FixedUpdate()
    {

        if (isInWater)
        {
            HandleMovement();
        }

    }
    void HandleRotation()
    {
        float verticalInput = Input.GetAxis("Vertical");
        currentRotationZ = Mathf.Clamp(currentRotationZ + d * verticalInput * rotationSpeed * Time.deltaTime, -maxRotationZ, maxRotationZ);
    }
    int d = 1;
    void HandleMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        
        if(horizontalInput > 0)
        {
           d = 1;
        }

        if (horizontalInput < 0)
        {
           d = -1;
        }

        Vector2 forwardDirection = transform.right;
        Vector2 velocity = new Vector2(1f, 0f) + forwardDirection * currentSpeed;
        rb.velocity = velocity * d;
        transform.localRotation = Quaternion.Euler(0, 0, currentRotationZ);
        transform.localScale = (d > 0) ? Vector3.one : new Vector3(-1, 1, 1);

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(" trigger stay " + other.gameObject.tag);

        if (isInWater)
        {
            DOTween.To(() => currentSpeed, x => currentSpeed = x, waterSpeed, 1f);
            //currentSpeed =  waterSpeed;
            rb.gravityScale = 0;
        }
        else
        {
            currentSpeed = 0;
            rb.gravityScale = 1;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(" trigger enter " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Ground")){
            isInWater = false;
        }
        if (other.gameObject.CompareTag("Water")){
            isInWater = true;
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(" trigger exit " + other.gameObject.tag);

        if (other.gameObject.CompareTag("Water"))
        {
            canJump = true;
            isInWater = false;
            currentSpeed = 0;
            rb.gravityScale = 1;
        }
    }

}
