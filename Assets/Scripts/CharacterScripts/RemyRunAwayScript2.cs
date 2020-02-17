using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyRunAwayScript2 : MonoBehaviour
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
        if (Vector3.Distance(player.position, this.transform.position) < 10)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(-direction), 0.1f);
            

            if (direction.magnitude < 8)
            {
                this.transform.Translate(0, 0, 0.05f);
                anim.SetInteger("condition", 1);
            }
            else
            {
                anim.SetInteger("condition", 0);
            }
        }
        else
        {
            anim.SetInteger("condition", 0);

        }

    }
}
