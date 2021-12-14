using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShip 
{
    public void Movement();
    public void Shoot();
    public void TakeDamage(int amount);
    public void Die();
    
}
