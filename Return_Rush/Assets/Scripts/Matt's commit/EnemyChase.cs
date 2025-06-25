using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 3f;                 // Movement speed
    public Transform target;                // Reference to the player

    private Rigidbody2D rb;
    //adding in some rotation mechanic so the player has a bettter chance to dodge enemy
    public float rotationSpeed = 50f;
    public GameObject pointA;
    private enemyAware _enemyAware;
    private UnityEngine.Vector2 _targetDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //retrieve varaibles from the enemyAware script
        _enemyAware = GetComponent<enemyAware>();
        // Auto-find player if not assigned in Inspector
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }

    
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Direction to the player
        UnityEngine.Vector2 direction = (target.position - transform.position).normalized;

        //check if player is nearby
        if (_enemyAware.AwareOfPlayer)
        {
            // Move toward the player
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            _targetDirection = direction;
        }
        else
        {
            _targetDirection = UnityEngine.Vector2.zero;
        }
        RotateTowardsTarget();  //call the rotate function
        SetVelocity();

    }
    //rotating the object
    private void RotateTowardsTarget()
    {
        
        if (_targetDirection == UnityEngine.Vector2.zero)
        {
            return;
        }
        UnityEngine.Quaternion targetRotation = UnityEngine.Quaternion.LookRotation(transform.forward, _targetDirection);
        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);

    }
    private void SetVelocity()
    {
        if (_targetDirection == UnityEngine.Vector2.zero)
        {
            rb.velocity = UnityEngine.Vector2.zero;
        }
        else
        {
            rb.velocity = transform.up * speed;
        }
    }
}
