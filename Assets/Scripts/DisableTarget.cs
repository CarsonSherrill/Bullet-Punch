using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Collider>().enabled = false;
    }
}
