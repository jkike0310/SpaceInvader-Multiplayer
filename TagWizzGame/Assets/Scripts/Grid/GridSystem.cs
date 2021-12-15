using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class GridSystem : MonoBehaviour
{
    
    private List<Cell> cellList = new List<Cell>();

    private void Awake()
    {
        cellList = GetComponentsInChildren<Cell>().ToList();        
    }

    private void Start()
    {
        LevelManager.instance.totalEnemies = 0;
        InitObjects();        
    }
    public void InitObjects()
    {
        for (int i = 0; i < cellList.Count; i++)
        {
            if(cellList[i].isActiveCell())
            {
                if(cellList[i].GetTypeSpawn() == TypeSpawn.Random)
                {
                    int randomType = Random.Range(0, 3);
                    cellList[i].SetTypeSpawn(randomType);
                }

                switch (cellList[i].GetTypeSpawn())
                {                   
                    case TypeSpawn.RedShip:
                        ShowEnemies(EnemySpawner.instance.redShipPrefab, EnemySpawner.instance.content, cellList[i].transform);
                        break;
                    case TypeSpawn.BlueShip:
                        ShowEnemies(EnemySpawner.instance.blueShipPrefab, EnemySpawner.instance.content, cellList[i].transform);
                        break;
                    case TypeSpawn.GreenShip:
                        ShowEnemies(EnemySpawner.instance.greenShipPrefab, EnemySpawner.instance.content, cellList[i].transform);
                        break;
                }                
            }
        }
    }

    private void ShowEnemies(GameObject prefab, Transform content, Transform cell)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            GameObject obj = PhotonNetwork.InstantiateRoomObject(prefab.name, cell.position, Quaternion.identity);
            obj.transform.SetParent(content);            
        }   
        LevelManager.instance.totalEnemies ++;
        LevelManager.instance.actualEnemies = LevelManager.instance.totalEnemies; 
    }


}
