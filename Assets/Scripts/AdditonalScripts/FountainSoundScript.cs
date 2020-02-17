using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainSoundScript : MonoBehaviour
{
    public float a1;
    public float a2;
    public float b1;
    public float b2;
    public Transform player;
    bool isFountainPlaying;
    bool isFountainPaused;
    bool fountainStartedPlaying;
    TurnOnOffFountain turn;
    // Start is called before the first frame update
    private void Awake()
    {
        turn= GameObject.Find("FountainRedButton").GetComponent<TurnOnOffFountain>();
    }
    void Start()
    {
        //a1 = 30;
        //a2 = 0;
        //b1 = 0;
        //b2 = 1;
        isFountainPlaying = false;
        isFountainPaused = false;
        fountainStartedPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(player.position, this.transform.position));
        if (Vector3.Distance(player.position, this.transform.position) < a1)
        {
            if (!isFountainPlaying)
            {
                if (turn.fountainStarted)
                {
                    isFountainPlaying = true;
                    FindObjectOfType<AudioManager>().Pause("birdsChirping");
                    fountainStartedPlaying = true;
                    FindObjectOfType<AudioManager>().Play("fountainSound");
                }
            }
            else 
            {
                if (isFountainPaused)
                {
                    if (turn.fountainStarted)
                    {
                        isFountainPaused = false;
                        FindObjectOfType<AudioManager>().Pause("birdsChirping");
                        FindObjectOfType<AudioManager>().Play("fountainSound");
                    }
                }
            }

            float volume = map(Vector3.Distance(player.position, this.transform.position), a1, a2, b1, b2);
            FindObjectOfType<AudioManager>().changeVolume("fountainSound", volume);
            //Debug.Log("Volume is:" + volume);
        }
        else
        {
            if (!isFountainPaused && fountainStartedPlaying)
            {
                isFountainPaused = true;
                FindObjectOfType<AudioManager>().Pause("fountainSound");
                FindObjectOfType<AudioManager>().Play("birdsChirping");
            }
        }
    }
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
