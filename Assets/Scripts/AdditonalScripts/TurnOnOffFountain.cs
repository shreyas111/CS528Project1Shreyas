using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOffFountain : MonoBehaviour
{
    public PhysicsButton button;
    bool resetPressed;
    public ParticleSystem fountain;
    public bool fountainStarted;

    // Use this for initialization
    void Start()
    {
        fountainStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (button.pressed && !resetPressed)
        {
            Debug.Log("Button Fountain Pressed");
            if(fountainStarted)
            {
                fountainStarted = false;
                fountain.Stop();
                FindObjectOfType<AudioManager>().Pause("fountainSound");


            }
            else
            {
                fountainStarted = true;
                fountain.Play();
                FindObjectOfType<AudioManager>().Play("fountainSound");
            }
            resetPressed = true;
        }
        else if (!button.pressed)
        {
            //Debug.Log("Button unpressed");
            resetPressed = false;
        }
    }
}
