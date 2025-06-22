using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
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
    }
    //Listens for input from player
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue <Vector2>();
    }
}
