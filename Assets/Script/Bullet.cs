using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerHealth pHealthScript;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
       pHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        damage = 5;
        pHealthScript.TakeDamage(damage);
        
    }
}
