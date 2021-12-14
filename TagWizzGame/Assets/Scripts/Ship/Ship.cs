using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IShip
{
    [SerializeField] protected string nameShip;
    [SerializeField] protected int health;
    [SerializeField] public int damage;
    [SerializeField] protected float movementSpeed;
    
    public virtual void Movement() {  }

    public virtual void TakeDamage(int damage)   {   }

    public virtual void Shoot()  {   }

    public virtual void Die()  {   }
}
