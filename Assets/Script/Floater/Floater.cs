using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    float degreesPerSecond = 300.0f;
    float amplitude = 0.05f;
    float frequency = 1f;
    public bool ItemFloaterState;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }
    public void startFloater()
    {
        posOffset = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(ItemFloaterState ==true)
        {
            // Spin object around Y-Axis
            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
       
    }
}
