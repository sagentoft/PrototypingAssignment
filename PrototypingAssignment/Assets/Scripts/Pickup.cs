using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Touching the object destroys it.
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
