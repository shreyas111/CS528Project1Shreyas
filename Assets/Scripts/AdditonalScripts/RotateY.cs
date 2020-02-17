using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    public int speed;
    // Use this for initialization
    void Start()
    {
        //name1=this.name;
        //Debug.Log("The name of the object is" + name1);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(Time.deltaTime * 50,0 , 0));
    }
}
