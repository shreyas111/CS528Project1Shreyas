using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BryceMusicScript : MonoBehaviour
{
    public float a1;
    public float a2;
    public float b1;
    public float b2;
    public Transform player;
    bool isPianoPlaying;
    bool isPianoPaused;
    bool pianoStartedPlaying;
    // Start is called before the first frame update
    void Start()
    {
        //a1 = 30;
        //a2 = 0;
        //b1 = 0;
        //b2 = 1;
        isPianoPlaying = false;
        isPianoPaused = false;
        pianoStartedPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(player.position, this.transform.position));
        if (Vector3.Distance(player.position, this.transform.position) < a1)
        {
            if (!isPianoPlaying)
            {
                isPianoPlaying = true;
                FindObjectOfType<AudioManager>().Pause("birdsChirping");
                pianoStartedPlaying = true;
                FindObjectOfType<AudioManager>().Play("Piano");
            }
            else
            {
                if(isPianoPaused)
                {
                    isPianoPaused = false;
                    FindObjectOfType<AudioManager>().Pause("birdsChirping");
                    FindObjectOfType<AudioManager>().Play("Piano");
                }
            }

            float volume = map(Vector3.Distance(player.position, this.transform.position), a1, a2, b1, b2);
            FindObjectOfType<AudioManager>().changeVolume("Piano", volume);
            //Debug.Log("Volume is:" + volume);
        }
        else
        {
            if (!isPianoPaused && pianoStartedPlaying)
            {
                isPianoPaused = true;
                FindObjectOfType<AudioManager>().Pause("Piano");
                FindObjectOfType<AudioManager>().Play("birdsChirping");
            }
        }
    }
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
