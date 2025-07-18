using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_movement : MonoBehaviour
{

    public float amp;
    Vector3 pos;
    void Start()
    {
       pos = transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        pos.y = (Mathf.Sin(Time.time) * amp) + pos.y;
        transform.position = pos;
    }
}
