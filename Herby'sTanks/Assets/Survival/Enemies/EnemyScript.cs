using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum enemyStates
    {
        patrolling,
        shooting,
        persueing,
        fleeing
    }

    [SerializeField]
    private enemyStates currentState;
    [SerializeField]
    private int BounceCount;
    [SerializeField]
    private bool isShot;


    //AI
    [SerializeField] private float moveSpeed = 5.0f; // The speed at which the tank moves
    [SerializeField] private float rotationSpeed = 2.0f; // The speed at which the tank rotates
    [SerializeField] private float sightRange; // The range at which the tank can see the player
    [SerializeField] private Transform barrel;
    [SerializeField] private float barrelRotationSpeed = 5.0f; // The speed at which the barrel rotates
    [SerializeField] private Rigidbody rb;


    void Update()
    {
        if (!isShot)
        {
            switch (currentState)
            {
                case enemyStates.patrolling:
                    break;
                case enemyStates.shooting:
                    break;
                case enemyStates.fleeing:
                    break;
            }
        }
    }
    void Patrol()
    {

    }
    void Persue()
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
