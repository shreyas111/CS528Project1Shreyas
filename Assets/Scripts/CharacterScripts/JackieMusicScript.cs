using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackieMusicScript : MonoBehaviour
{
    public float a1;
    public float a2;
    public float b1;
    public float b2;
    public Transform player;
    bool isElectronicPlaying;
    bool isElectronicPaused;
    bool electronicStartedPlaying;

    //public GameObject TriggerObject;
    public BallBoxTriggerScript whichSongScript;
    // Start is called before the first frame update
    void Start()
    {
        //a1 = 30;
        //a2 = 0;
        //b1 = 0;
        //b2 = 1;
        isElectronicPlaying = false;
        isElectronicPaused = false;
        electronicStartedPlaying = false;
        whichSongScript = GameObject.Find("BallBoxTrigger").GetComponent <BallBoxTriggerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(player.position, this.transform.position));
        if (Vector3.Distance(player.position, this.transform.position) < 20)
        {
            if (!isElectronicPlaying)
            {
                isElectronicPlaying = true;
                FindObjectOfType<AudioManager>().Pause("birdsChirping");
                electronicStartedPlaying = true;
                //FindObjectOfType<AudioManager>().Play("bensoundSummer");
                if (whichSongScript.playSummer)
                    FindObjectOfType<AudioManager>().Play("bensoundSummer");
                else
                    FindObjectOfType<AudioManager>().Play("allThat");
            }
            else
            {
                if (isElectronicPaused)
                {
                    isElectronicPaused = false;
                    FindObjectOfType<AudioManager>().Pause("birdsChirping");
                    if (whichSongScript.playSummer)
                        FindObjectOfType<AudioManager>().Play("bensoundSummer");
                    else
                        FindObjectOfType<AudioManager>().Play("allThat");
                    //FindObjectOfType<AudioManager>().Play("bensoundSummer");
                }
            }

            float volume = map(Vector3.Distance(player.position, this.transform.position), a1, a2, b1, b2);
            //FindObjectOfType<AudioManager>().changeVolume("bensoundSummer", volume);
            if (whichSongScript.playSummer)
                FindObjectOfType<AudioManager>().changeVolume("bensoundSummer", volume);
            else
                FindObjectOfType<AudioManager>().changeVolume("allThat", volume);

            //Debug.Log("Volume is:" + volume);
        }
        else
        {
            if (!isElectronicPaused && electronicStartedPlaying)
            {
                isElectronicPaused = true;
                //FindObjectOfType<AudioManager>().Pause("bensoundSummer");
                if (whichSongScript.playSummer)
                    FindObjectOfType<AudioManager>().Pause("bensoundSummer");
                else
                    FindObjectOfType<AudioManager>().Pause("allThat");

                FindObjectOfType<AudioManager>().Play("birdsChirping");
            }
        }
    }
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
