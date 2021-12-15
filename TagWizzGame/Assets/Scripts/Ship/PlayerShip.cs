using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputShip))]
public class PlayerShip : Ship
{    
    public static GameObject LocalPlayerInstance;  
    [HideInInspector] public PhotonView photonView;

    private InputShip inputShip;
    public UnityEvent OnShoot = new UnityEvent();
    //private float offset = 7.2f;
    

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
        if (!photonView.IsMine || ObjectsManager.instance.IsGameOver())
        {  
            return;                 
        }
        Movement();
        Shoot();
    }    

    public override void Movement()
    {
        transform.Translate(movementSpeed * inputShip.horizontal * Time.deltaTime , 0, 0);
    }

    public override void Shoot()
    {
        if(inputShip!=null)
        {
            if(Input.GetKeyDown(inputShip.shootButton) || inputShip.vertical>0)
            {
                OnShoot.Invoke();
            }
        }        
        else
        {
            inputShip = GetComponent<InputShip>();
        }
    }

    public override void TakeDamage(int amount)
    {
        health -= amount;
        if(health<=0)
        {
            if(photonView!=null)
            {
                photonView.RPC("ShowFX", RpcTarget.All);
                photonView.RPC("PlaySound", RpcTarget.All);
            }                        
            if(photonView.IsMine || PhotonNetwork.IsMasterClient){                
                Die();
            }else{      
                if(photonView!=null)          
                {
                    photonView.RPC("Die", RpcTarget.MasterClient);
                }                
            }  
        }
    }

    [PunRPC]
    private void ShowFX()
    {
        EnemyFX.instance.ShowParticle(gameObject.transform);
    }
    [PunRPC]
    private void PlaySound()
    {
        EnemySound.instance.PlaySound("death");
    }    
    [PunRPC]
    public override void Die()
    {
        FindObjectOfType<ObjectsManager>().GameOver();
        if(photonView!=null && photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }        
    }    
   
    
}
