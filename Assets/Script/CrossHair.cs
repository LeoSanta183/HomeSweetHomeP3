using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrossHair : MonoBehaviour
{
    public GameObject CrosshairNormal;
    public GameObject CrosshairInteract;
    public bool Gone;
    // Start is called before the first frame update
    void Start()
    {
        CrosshairNormal.SetActive(true);
        CrosshairInteract.SetActive(false);
        Gone = false;
    }


   void OnTriggerEnter(Collider other)
   {
       if(other.tag == "Enemy")
       {
           CrosshairInteract.SetActive(true);
           CrosshairNormal.SetActive(false);
           Gone = true;
       }
   
   }
   void OnTriggerExit(Collider other)
   {
       if(other.tag == "Reach")
       {
           CrosshairInteract.SetActive(false);
           CrosshairNormal.SetActive(true);
           Gone = false;
       }
   }


}
