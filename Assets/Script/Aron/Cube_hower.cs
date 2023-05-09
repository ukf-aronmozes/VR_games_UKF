using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_hower : MonoBehaviour
{
    public static Cube_hower instace;

    public float hower;

    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Cube")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * hower, ForceMode.Acceleration);
        }
    }
}
