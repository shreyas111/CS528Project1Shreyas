using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClairWalkScript : MonoBehaviour
{
    public Transform[] walkSpots;
    public float speed;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        //updateDirection();

    }
    void updateDirection()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(walkSpots[index].position, this.transform.position) > 3)
        //{
            //Debug.Log("Index is" + index);
            //Debug.Log("Distance is " + Vector3.Distance(walkSpots[index].position, this.transform.position));
            Vector3 direction = walkSpots[index].position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            //if (this.transform.position != walkSpots[index].position)
            if (Vector3.Distance(this.transform.position, walkSpots[index].position)>3)
            {
                //Vector3 pos = Vector3.MoveTowards(this.transform.position, walkSpots[index].position, speed*Time.deltaTime);
                //this.GetComponent<Rigidbody>().MovePosition(pos);
                
                this.transform.Translate(0, 0, 0.05f);
            }
            else
            {
                index= (index + 1) % walkSpots.Length ;
            }
        }

    //}
}
