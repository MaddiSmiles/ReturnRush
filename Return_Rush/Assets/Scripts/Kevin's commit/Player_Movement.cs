using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    //fields for checking dash
    [SerializeField] private float dashPower = 10f;
    //If we want to implement a trail thing
    //[SerializeField] private TrailRenderer tr;
    [SerializeField] private float maxDashEnergy = 1f;
    [SerializeField] private float dashRechargeRate = 0.25f; // per second
    //The animator
    public Animator animator;
    private float currentDashEnergy;
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    private bool wasMoving = false;

    // Add these fields at the top of class (with the other private fields)
    private float stepTimer = 0f;
    private float stepInterval = 0.5f; // adjust this for faster or slower footsteps


    //private AudioSource footstepSource;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get the Rigidbody for object attached to script
        rb = GetComponent<Rigidbody2D>();
        currentDashEnergy = maxDashEnergy;
        //Footstep audio
        /*
        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.clip = audioManager.steps;
        footstepSource.loop = true;
        footstepSource.volume = 0.5f; // Or whatever you want
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftShift)) && canDash && currentDashEnergy >= 1f)
            StartCoroutine(Dash());

        if (!isDashing)
        {
            rb.velocity = moveInput * moveSpeed;

            if (moveInput != Vector2.zero)
            {
                stepTimer -= Time.deltaTime;

                if (stepTimer <= 0f)
                {
                    audioManager.PlaySFX(audioManager.steps);
                    stepTimer = stepInterval; // Reset the step timer
                }

                lastMoveDirection = moveInput.normalized;
                wasMoving = true;
            }
            else
            {
                wasMoving = false;
                stepTimer = 0f; // Reset step timer when not moving
            }
        }


        // Recharge dash energy over time
        if (currentDashEnergy < maxDashEnergy)
        {
            currentDashEnergy += dashRechargeRate * Time.deltaTime;
            currentDashEnergy = Mathf.Min(currentDashEnergy, maxDashEnergy);
        }

        //Animating the player
        animator.SetFloat("Speed", Mathf.Abs(moveInput.y + moveInput.x * moveSpeed));

    }


    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    //Listens for input from player
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //setting dash to limit player from spamming
    private IEnumerator Dash()
    {
        if (Time.timeScale == 0f) yield break; // Prevent dash if paused

        if (moveInput == Vector2.zero)
            yield break;

        canDash = false;
        isDashing = true;
        currentDashEnergy -= 1f;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        //this moves player forward
        rb.velocity = moveInput.normalized * dashPower;

        // Play dash sound through AudioManager
        audioManager.PlaySFX(audioManager.dash);

        ///trail?
        /// tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        //tr.emitting = fasle;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }



    public float GetDashCharge()
    {
        return currentDashEnergy;
    }
     
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDashing && other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDir = (other.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockbackDir * 300f); // You can tweak the force value
            }
        }
    }
}
