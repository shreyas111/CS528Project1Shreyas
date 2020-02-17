using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeganFollowScript : MonoBehaviour
{
    public Transform player;
    Animator anim;
    // Start is called before the first frame update
    bool isSoundPlayed;
    void Start()
    {
        anim = GetComponent<Animator>();
        isSoundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 5)
            {

                this.transform.Translate(0, 0, 0.03f);
                anim.SetInteger("condition", 1);
            }
            else
            {
                if (!isSoundPlayed)
                {
                    isSoundPlayed = true;
                    FindObjectOfType<AudioManager>().Play("footballBox");
                }
                anim.SetInteger("condition", 0);
            }
        }
        else
        {
            anim.SetInteger("condition", 0);
            isSoundPlayed = false;

        }

    }
}
