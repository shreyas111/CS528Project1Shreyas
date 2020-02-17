using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDayNightScript : MonoBehaviour
{
    public PhysicsButton button;
    bool resetPressed;
    DayNightCycle dayNightCycle;

    // Use this for initialization
    void Start()
    {
        dayNightCycle = GameObject.Find("Day Night Cycle").GetComponent<DayNightCycle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (button.pressed && !resetPressed)
        {
            Debug.Log("Button Pressed");
            //Debug.Log("Time of Day is: " + dayNightCycle.timeOfDay);
            dayNightCycle.pause = !dayNightCycle.pause;
            resetPressed = true;
        }
        else if (!button.pressed)
        {
            //Debug.Log("Button unpressed");
            resetPressed = false;
        }
    }
}
