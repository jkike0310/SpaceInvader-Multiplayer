using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeSpawn
{    
    RedShip = 0,
    BlueShip = 1,
    GreenShip = 2,
    Random = 3
}

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    [SerializeField] public Transform content;

    [SerializeField] public GameObject redShipPrefab;
    [SerializeField] public GameObject greenShipPrefab;
    [SerializeField] public GameObject blueShipPrefab;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
