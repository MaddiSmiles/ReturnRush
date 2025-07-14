using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_triggers : MonoBehaviour
{
    public GameObject movement;
    public GameObject dash;
    public GameObject enemy_intruct;
    public GameObject final_words;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EndZone"))
        {
            SceneManager.LoadScene("Game");
        }
        if (other.CompareTag("10"))
        {
            movement.SetActive(true);
        }
        if (other.CompareTag("30"))
        {
            movement.SetActive(true);
        }
        if (other.CompareTag("50"))
        {
            movement.SetActive(true);
        }
        if (other.CompareTag("40"))
        {
            movement.SetActive(true);
        }
    }
}
