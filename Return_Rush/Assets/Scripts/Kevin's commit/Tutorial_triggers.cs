using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_triggers : MonoBehaviour
{
    public GameObject movement;
    public GameObject dash;
    public GameObject enemy_instr;
    public GameObject enemy_instr2;
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
            dash.SetActive(true);
        }
        if (other.CompareTag("50"))
        {
            enemy_instr.SetActive(true);
        }
        if (other.CompareTag("40"))
        {
            enemy_instr2.SetActive(true);
        }
        if (other.CompareTag("20"))
        {
            final_words.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("10"))
        {
            movement.SetActive(false);
        }
        if (other.CompareTag("30"))
        {
            dash.SetActive(false);
        }
        if (other.CompareTag("50"))
        {
            enemy_instr.SetActive(false);
        }
        if (other.CompareTag("40"))
        {
            enemy_instr2.SetActive(false);
        }
        if (other.CompareTag("20"))
        {
            final_words.SetActive(false);
        }
    }
}
