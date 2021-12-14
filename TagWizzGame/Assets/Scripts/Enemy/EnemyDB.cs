using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="EnemiesDB", menuName ="Enemy/new EnemyDB")]
public class EnemyDB : ScriptableObject
{
    public List<Enemy> enemies;
}
