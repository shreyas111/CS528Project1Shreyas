using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBoxTriggerScript : MonoBehaviour
{
    public bool playSummer;

    private void Awake()
    {
        playSummer = true;
    }
    public void OnTriggerEnter(Collider other)
    {
       if(other.name.Equals("Football"))
       {
            playSummer = false;
            FindObjectOfType<AudioManager>().Pause("bensoundSummer");
            FindObjectOfType<AudioManager>().Play("allThat");
        }


    }
    public void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Football"))
        {
            playSummer = true;
            FindObjectOfType<AudioManager>().Pause("allThat");
            FindObjectOfType<AudioManager>().Play("bensoundSummer");
        }
    }
}
