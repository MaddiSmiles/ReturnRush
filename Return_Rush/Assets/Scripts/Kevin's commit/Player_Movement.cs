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
    private bool isDashing;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        //Get the Rigidbody for object attached to script
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //move object from input
        rb.velocity = moveInput * moveSpeed;
        //implementing DASH
        if (Input.GetKeyDown("q") && canDash)
        {
            StartCoroutine(Dash());
        }
        if (isDashing)
        {
            return;
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
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        //this moves player forward
        rb.velocity = new Vector2(0f, transform.localScale.y * dashPower);
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
