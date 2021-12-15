using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool activeCell;
    [SerializeField] private TypeSpawn typeSpawn;
    
    public bool isActiveCell()
    {
        return activeCell;
    }

    public TypeSpawn GetTypeSpawn()
    {
        return typeSpawn;
    }

    public void SetTypeSpawn(int type)
    {
        typeSpawn = (TypeSpawn) type;
    }
}
