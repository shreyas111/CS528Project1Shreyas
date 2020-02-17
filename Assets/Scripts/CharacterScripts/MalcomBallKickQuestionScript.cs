using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalcomBallKickQuestionScript : MonoBehaviour
{
    public Transform player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) > 10 && Vector3.Distance(player.position, this.transform.position) <= 15)
        {
            anim.SetBool("isStandingUp", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
        }
        else if (Vector3.Distance(player.position, this.transform.position) > 15)
        {
            anim.SetBool("isStandingUp", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
        }
        else if (Vector3.Distance(player.position, this.transform.position) <= 10)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            if (direction.magnitude > 5)
            {

                this.transform.Translate(0, 0, 0.05f);
                anim.SetBool("isStandingUp", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isStandingUp", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalking", false);
            }
        }

    }
}
