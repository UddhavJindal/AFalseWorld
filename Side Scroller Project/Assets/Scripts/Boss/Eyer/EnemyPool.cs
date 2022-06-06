using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    private List<GameObject> poolObject = new List<GameObject>();
    private int amountToPool;

    [SerializeField] GameObject objectToPool;
    [SerializeField] int amount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        amountToPool = amount;
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            obj.transform.parent = transform;
            poolObject.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolObject.Count; i++)
        {
            if (!poolObject[i].activeInHierarchy)
            {
                return poolObject[i];
            }
        }

        return null;
    }
}