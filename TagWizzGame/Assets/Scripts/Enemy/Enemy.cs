using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class Enemy : Ship
{
    private PhotonView photonView;   
    [SerializeField]  private int points;


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }    

    private void Start()
    {
        this.movementSpeed = this.movementSpeed*LevelManager.instance.GetLevel();
    }    

    private void Update()
    {
        if(ObjectsManager.instance.IsGameOver())
        {
            return;
        }
        Movement();
    }
    [PunRPC]
    public override void Die()
    {          
        LevelManager.instance.KillEnemy();
        if(photonView!=null && photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }        
    }
    [PunRPC]
    private void ShowFX()
    {
        EnemyFX.instance.ShowParticle(gameObject.transform);
    }
    [PunRPC]
    private void PlaySoundDead()
    {
        EnemySound.instance.PlaySound("death");
    }
    [PunRPC]
    private void PlaySoundDamage()
    {
        EnemySound.instance.PlaySound("hurt");
    }
    
    public override void TakeDamage(int amount)
    {        
        health-=amount;
        photonView.RPC("ShowFX", RpcTarget.All);
        photonView.RPC("PlaySoundDamage", RpcTarget.All);
        if(health<=0){

            FindObjectOfType<PointsManager>().GainPoints(points);
            if(photonView!=null)
            {
                photonView.RPC("ShowFX", RpcTarget.All);
                photonView.RPC("PlaySoundDead", RpcTarget.All);
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

    public override void Movement()
    {
        transform.Translate(0, -movementSpeed *  Time.deltaTime, 0);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShip>().TakeDamage(damage);            
        }
        else if( other.CompareTag("Limit"))
        {
            FindObjectOfType<ObjectsManager>().GameOver();
        }
    }

   
}
