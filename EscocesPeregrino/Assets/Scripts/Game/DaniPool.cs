using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaniPool : MonoBehaviour
{
    [System.Serializable]
    struct ObjectsToPool
    {
        public GameObject prefab;
        public List<GameObject> objects;
        public int firstAmount;
    }
    [SerializeField] List<ObjectsToPool> posibleEnemies = new List<ObjectsToPool>();

    public static DaniPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        foreach(ObjectsToPool group in posibleEnemies)
        {
            for (int i = 0; i < group.firstAmount; i++)
            {
                GameObject newEnemy = Instantiate(group.prefab);
                newEnemy.SetActive(false);
                group.objects.Add(newEnemy);
            }
        }
    }

    public void Spawn(GameObject enemyType, Transform where)
    {
        foreach(ObjectsToPool obj in posibleEnemies)
        {
            if(obj.prefab == enemyType)
            {
                foreach(GameObject enemy in obj.objects)
                {
                    if(!enemy.activeInHierarchy)
                    {
                        enemy.transform.position = where.position;
                        enemy.transform.rotation = where.rotation;
                        enemy.SetActive(true);
                        return;
                    }
                }
                    GameObject newEnemy = Instantiate(obj.prefab, where.position, where.rotation);
                    obj.objects.Add(newEnemy);
                    return;
            }
        }
        print("No hay ese tipo de cosa en la pool");
    }
}
