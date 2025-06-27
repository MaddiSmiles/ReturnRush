using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    public Slider dashSlider;           // Reference to the UI Slider
    public Player_Movement player;      // Reference to your player script

    void Update()
    {
        if (player != null && dashSlider != null)
        {
            dashSlider.value = player.GetDashCharge();  // Call the public getter
        }
    }
}
