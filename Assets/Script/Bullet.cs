using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Target targetScript;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        targetScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        damage = 5;
        targetScript.TakeDamage(damage);
        
    }
}
