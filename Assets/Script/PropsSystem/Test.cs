using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Page1,Page2;
    // Start is called before the first frame update
    void Start()
    {
        Page1.SetActive(true);
        Page2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Page1.SetActive(false);
            Page2.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Page1.SetActive(true);
            Page2.SetActive(false);
        }
    }
}
