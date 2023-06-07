using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public int BounceCount;
    int bounces;
    public int speed = 15;
    [SerializeField] private AudioSource bulletSFX;

    private void Start()
    {
        rb.velocity = transform.forward * speed;
        bounces = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bounceable"))
        {
            Debug.Log("audioplay");
            bulletSFX.Play();
            if (bounces < BounceCount)
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
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<TankScript>().isShot = true;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
           Destroy(this.gameObject);

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.gameObject.GetComponent<EnemyScript>().isShot = true;
            collision.gameObject.gameObject.GetComponent<EnemyAI>().isShot = true;
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
