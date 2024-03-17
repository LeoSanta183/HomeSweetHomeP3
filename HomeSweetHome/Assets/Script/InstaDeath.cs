using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaDeath : MonoBehaviour
{

private void OnTriggerEnter(Collider other)
{
    if(other.tag == "Spear")
    {
        Destroy(gameObject);
    }
}

}
