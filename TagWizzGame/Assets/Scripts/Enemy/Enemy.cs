using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
[System.Serializable]
public class Enemy : Ship
{
    private PhotonView photonView;   
    [SerializeField]  private int points;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }    

    private void Update()
    {
        Movement();
    }
    [PunRPC]
    public override void Die()
    {        
        PhotonNetwork.Destroy(photonView);       
    }
    
    public override void TakeDamage(int amount)
    {        
        health-=amount;
        if(health<=0){

            FindObjectOfType<PointsManager>().GainPoints(points);

             if(photonView.IsMine || PhotonNetwork.IsMasterClient){
               Die();
            }else{
                photonView.RPC("Die", RpcTarget.MasterClient);
            }      
        }
    }

    public override void Movement()
    {
        transform.Translate(0, -movementSpeed *  Time.deltaTime, 0);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){

            other.GetComponent<PlayerShip>().TakeDamage(damage);            
        }
    }

   
}
