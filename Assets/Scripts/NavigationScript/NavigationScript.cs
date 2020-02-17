using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationScript : MonoBehaviour
{
    public Transform[] positions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void travelPosition1()
    {
       // Debug.Log("Position 1");
        transform.position = positions[0].position;
        transform.rotation = positions[0].rotation;
        
    }
    public void travelPosition2()
    {
        Debug.Log("Position 2");
        transform.position = positions[1].position;
        transform.rotation = positions[1].rotation;
    }
    public void travelPosition3()
    {
        Debug.Log("Position 3");
        transform.position = positions[2].position;
        transform.rotation = positions[2].rotation;
    }
    public void travelPosition4()
    {
        Debug.Log("Position 4");
        transform.position = positions[3].position;
        transform.rotation = positions[3].rotation;
    }
    public void travelPosition5()
    {
        Debug.Log("Position 5");
        transform.position = positions[4].position;
        transform.rotation = positions[4].rotation;
    }
}
