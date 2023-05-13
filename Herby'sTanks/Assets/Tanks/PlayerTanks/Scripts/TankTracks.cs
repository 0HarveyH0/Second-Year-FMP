using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTracks : MonoBehaviour
{
    public static TankTracks instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 500;

    [SerializeField] private GameObject trackPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(trackPrefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

}
