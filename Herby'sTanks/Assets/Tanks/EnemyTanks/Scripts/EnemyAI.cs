using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

enum enemyStates { patrolling, shooting }
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform parent;
    public int BounceCount;
    public Transform player;
    public GameObject bullet;
    public Transform bulletHole;
    public GameObject explosion;
    public int bulletCount;
    public int maxBullets;
    public bool canShoot;
    public bool isShot;
    private enemyStates currentState = enemyStates.patrolling;

    //AI
    public float moveSpeed = 5.0f; // The speed at which the tank moves
    public float rotationSpeed = 2.0f; // The speed at which the tank rotates
    public float sightRange; // The range at which the tank can see the player
    public Transform barrel;
    public float barrelRotationSpeed = 5.0f; // The speed at which the barrel rotates
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {

        }
        else
        {
        }
        if (!isShot)
        {
            if (Vector3.Distance(transform.position, player.position) < sightRange)
            {
                // Rotate towards the player
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);

                Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + movement);
                // Rotate the barrel towards the player
                Vector3 playerPosition = player.position;
                Vector3 relativePos = playerPosition - barrel.position;

                Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
                rotation.z = 0;
                barrel.rotation = rotation * Quaternion.Euler(0, -90, 0);
            }
            switch (currentState)
            {
                case enemyStates.patrolling:
                    Patrol();
                    Debug.Log("Patrol");
                    break;
                case enemyStates.shooting:
                    Shoot();
                    Debug.Log("Shoot");
                    break;
            }
        }
        else
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Debug.Log("isShot");
        }
    }


    void Patrol()
    {

        CastRay(bulletHole.position, bulletHole.forward);
    }

    void Shoot()
    {
        if (bulletCount < maxBullets && canShoot)
        {
            StartCoroutine(FireRate());
        }
    }

    IEnumerator FireRate()
    {
        Debug.Log("hasShot");
        Instantiate(bullet, bulletHole.position, bulletHole.rotation);
        bulletCount++;
        canShoot = false;
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
        currentState = enemyStates.patrolling;
    }

    void CastRay(Vector3 position, Vector3 direction)
    {
        for (int i = 0; i < BounceCount; i++)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, 1))
            {
                Debug.DrawLine(position, hit.point, Color.red);
                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    currentState = enemyStates.shooting;
                }
                else
                {
                    Debug.Log(hit.transform.gameObject.name);
                }
            }
            else
            {
                Debug.DrawRay(position, direction * 100, Color.blue);
            }
        }
    }


}
