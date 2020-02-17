using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight2 : MonoBehaviour
{
    public float speed = 2f;
    public float maxRotation = 90f;

    void Update()
    {
        //transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed), maxRotation * Mathf.Sin(Time.time * speed), maxRotation * Mathf.Sin(Time.time * speed));
        transform.localRotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed)+90);
        //transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed),0f,0f);

    }
}
