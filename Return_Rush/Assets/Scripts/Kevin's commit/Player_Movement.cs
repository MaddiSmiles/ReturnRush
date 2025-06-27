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
    private bool canDash = true;
    private bool isDashing = false;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        //Get the Rigidbody for object attached to script
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            rb.velocity = moveInput * moveSpeed;

            if (moveInput != Vector2.zero)
                lastMoveDirection = moveInput.normalized;

            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Space)) && canDash)
                StartCoroutine(Dash());
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
        if (moveInput == Vector2.zero)
            yield break;
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        //this moves player forward
        rb.velocity = moveInput.normalized * dashPower;
        ///trail?
        /// tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = fasle;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
