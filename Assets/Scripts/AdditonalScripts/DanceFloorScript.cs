using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceFloorScript : MonoBehaviour
{

    public GameObject shay;
    public GameObject sophie;
    Animator anim;
    Animator anim2;
    // Start is called before the first frame update
    void Start()
    {
        anim = shay.GetComponent<Animator>();
        anim2 = sophie.GetComponent<Animator>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("CAVE2-PlayerController"))
        {
            anim.SetInteger("condition", 1);
            anim2.SetInteger("condition", 1);
            FindObjectOfType<AudioManager>().Pause("birdsChirping");
            FindObjectOfType<AudioManager>().Play("happyRock");
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("CAVE2-PlayerController"))
        {
            anim.SetInteger("condition", 0);
            anim2.SetInteger("condition", 0);
            FindObjectOfType<AudioManager>().Pause("happyRock");
            FindObjectOfType<AudioManager>().Play("birdsChirping");
        }
    }
}
