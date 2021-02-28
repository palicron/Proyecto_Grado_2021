using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatform : MonoBehaviour
{
    public float platformSpeed;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public Rigidbody tubeRB1;



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationX, rotationY, rotationZ)*platformSpeed* Time.deltaTime);
    }
}
