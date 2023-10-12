using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    private Rigidbody rb;
       public float moveSpeed;
    private bool hasDamaged;
    void Start()
    {


        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * moveSpeed;


       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && !hasDamaged)
        {
            hasDamaged = true;
        }

        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
