using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShip : Ship
{    
    public static GameObject LocalPlayerInstance;  
    [HideInInspector] public PhotonView photonView;

    private InputShip inputShip;
    public UnityEvent OnShoot = new UnityEvent();
    

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        inputShip = GetComponent<InputShip>();
        if (photonView.IsMine)
        {
            PlayerShip.LocalPlayerInstance = this.gameObject;
            
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {  
            return;                 
        }
        Movement();
        Shoot();
    }    

    public override void Movement()
    {
       transform.Translate(movementSpeed * inputShip.horizontal * Time.deltaTime, 0, 0);
    }

    public override void Shoot()
    {
        if(Input.GetKeyDown(inputShip.shootButton) || inputShip.vertical>0)
        {
            OnShoot.Invoke();
        }
    }

    public override void TakeDamage(int amount)
    {
        health -= amount;
        if(amount<=0)
        {
            Die();
        }
    }
    

    public override void Die()
    {
        PhotonNetwork.Destroy(photonView);
    }    
   
    
}
