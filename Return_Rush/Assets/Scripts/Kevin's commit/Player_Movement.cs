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
    private float currentDashEnergy;
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;

    private bool wasMovingLastFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get the Rigidbody for object attached to script
        rb = GetComponent<Rigidbody2D>();
        currentDashEnergy = maxDashEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            rb.velocity = moveInput * moveSpeed;

            if (moveInput != Vector2.zero)
                lastMoveDirection = moveInput.normalized;

            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftShift)) && canDash && currentDashEnergy >= 1f)
                StartCoroutine(Dash());

            HandleFootstepAudio(); // ✅ important!
        }

        // Recharge dash energy over time
        if (currentDashEnergy < maxDashEnergy)
        {
            currentDashEnergy += dashRechargeRate * Time.deltaTime;
            currentDashEnergy = Mathf.Min(currentDashEnergy, maxDashEnergy);
        }
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
        if (AudioManager.instance != null && AudioManager.instance.isGameOver)
            yield break;

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
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.dashClip, AudioManager.instance.dashVolume);


        ///trail?
        /// tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        //tr.emitting = fasle;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void HandleFootstepAudio()
    {
        if (AudioManager.instance == null || AudioManager.instance.footstepClip == null)
            return;

        AudioSource sfx = AudioManager.instance.sfxSource;
        bool isMoving = moveInput.sqrMagnitude > 0.1f;

        if (AudioManager.instance.isGameOver)
        {
            if (sfx.isPlaying && sfx.clip == AudioManager.instance.footstepClip)
                sfx.Stop();
            return;
        }

        if (isMoving)
        {
            if (sfx.clip != AudioManager.instance.footstepClip || !sfx.isPlaying)
            {
                AudioManager.instance.PlayLoopingSFX(AudioManager.instance.footstepClip, AudioManager.instance.footstepVolumef); // ✅ pass volume
            }
        }
        else
        {
            if (sfx.clip == AudioManager.instance.footstepClip && sfx.isPlaying)
                sfx.Stop();
        }
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
