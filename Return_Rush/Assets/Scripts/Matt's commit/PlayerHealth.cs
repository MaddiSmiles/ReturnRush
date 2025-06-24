using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private HashSet<GameObject> currentColliders = new HashSet<GameObject>();
    private bool isTackled = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isTackled) return;

        if (other.CompareTag("Enemy") || other.CompareTag("Lineman"))
        {
            currentColliders.Add(other.gameObject);
            CheckTackleCondition();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Lineman"))
        {
            currentColliders.Remove(other.gameObject);
        }
    }

    void CheckTackleCondition()
    {
        int enemyCount = 0;

        foreach (GameObject obj in currentColliders)
        {
            if (obj.CompareTag("Lineman"))
            {
                // Lineman = auto tackle
                TriggerTackle();
                return;
            }
            else if (obj.CompareTag("Enemy"))
            {
                enemyCount++;
            }
        }

        if (enemyCount >= 2)
        {
            TriggerTackle();
        }
    }

    void TriggerTackle()
    {
        isTackled = true;
        Debug.Log("Player has been tackled!");

        // You can trigger Game Over, disable movement, play animation, etc.
        // For now:
        Time.timeScale = 0; // Freeze game
    }
}
