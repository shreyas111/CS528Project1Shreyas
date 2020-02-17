using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JodyTalkingScript : MonoBehaviour
{
    public Transform player;
    Animator anim;
    bool isSoundPlayed;
    // Start is called before the first frame update
    void Start()
    {
        isSoundPlayed = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude < 5)
            {
                anim.SetInteger("condition", 1);
                if (!isSoundPlayed)
                {
                    isSoundPlayed = true;
                    //FindObjectOfType<AudioManager>().Play("hello");
                    FindObjectOfType<AudioManager>().Play("cafeFoodSound");
                }
            }
            else
            {
                isSoundPlayed = false;
                anim.SetInteger("condition", 0);
            }

        }
        else
        {
            isSoundPlayed = false;
            anim.SetInteger("condition", 0);

        }

    }
}
