using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    public GameObject moon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(transform.position, Vector3.right, 1.66f * Time.deltaTime);
        moon.transform.RotateAround(transform.position, Vector3.right, -1.66f * Time.deltaTime);
    }
}
