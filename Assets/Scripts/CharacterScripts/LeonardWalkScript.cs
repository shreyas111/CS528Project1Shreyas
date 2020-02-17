using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonardWalkScript : MonoBehaviour
{
    public Transform[] walkSpots;
    public float speed;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = walkSpots[index].position - this.transform.position;
        direction.y = 0;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
        if (Vector3.Distance(this.transform.position, walkSpots[index].position) > 3)
        {
            this.transform.Translate(0, 0, 0.08f);
        }
        else
        {
            index = (index + 1) % walkSpots.Length;
        }
    }
}
