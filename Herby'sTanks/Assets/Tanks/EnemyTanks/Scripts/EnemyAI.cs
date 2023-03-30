using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum enemyStates { patrolling, shooting }
public class EnemyAI : MonoBehaviour
{
    public int BounceCount;
    public NavMeshAgent player;
    public Transform barrelHole;
    private enemyStates currentState = enemyStates.patrolling;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case enemyStates.patrolling:
                Patrol();
                break;
            case enemyStates.shooting:
                break;
        }
    }


    void Patrol()
    {
        CastRay(barrelHole.position, barrelHole.forward);
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
            }
            else
            {
                Debug.DrawRay(position, direction * 100, Color.blue);
            }
        }
    }


}
