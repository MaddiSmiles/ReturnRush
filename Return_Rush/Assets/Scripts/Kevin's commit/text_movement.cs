using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class text_movement : MonoBehaviour
{

    public float amp;
    Vector3 pos;
    public PauseMenu pause;
    void Start()
    {
        pos = transform.position;
        
    }
    // Update is called once per frame
    void Update()
    {

        if (pause.isPaused)
        {
            pausedGame();
        }
        else
        {
            moveText();
        }
    }
    void moveText()
    {
        pos.y = (Mathf.Sin(Time.time) * amp) + pos.y;
        transform.position = pos;
    }
    void pausedGame()
    {
        return;
    }
}
