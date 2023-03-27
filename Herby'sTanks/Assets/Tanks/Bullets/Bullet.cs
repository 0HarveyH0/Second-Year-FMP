using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem bulletParticleSystem;
    public Rigidbody rb;
    public int BounceCount;
    int bounces;
    public int speed = 15;

    private void Start()
    {
        rb.velocity = transform.forward * speed;
        bounces = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bounceable"))
        {
            if(bounces < BounceCount)
            {
                Vector3 newDir = Vector3.Reflect(transform.forward, collision.contacts[0].normal);

                transform.rotation = Quaternion.LookRotation(newDir);

                rb.velocity = transform.forward * speed;
                bounces++;
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<TankScript>().isShot = true;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
           Destroy(this.gameObject);

        }
    }

}
