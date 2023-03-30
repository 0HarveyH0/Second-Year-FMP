using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum enemyStates { patrolling, shooting }
public class EnemyAI : MonoBehaviour
{
    public int BounceCount;
    public NavMeshAgent player;
    public GameObject bullet;
    public Transform bulletHole;
    public int bulletCount;
    public int maxBullets;
    public bool canShoot;
    public bool isShot;
    private enemyStates currentState = enemyStates.patrolling;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!isShot)
        {
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
        var bulletObj = Instantiate(bullet, bulletHole.position, bulletHole.rotation);
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
