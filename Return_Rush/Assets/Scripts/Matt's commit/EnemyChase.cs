using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 3f;                 // Movement speed
    public Transform target;                // Reference to the player

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        Vector2 direction = (target.position - transform.position).normalized;

        // Move toward the player
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
