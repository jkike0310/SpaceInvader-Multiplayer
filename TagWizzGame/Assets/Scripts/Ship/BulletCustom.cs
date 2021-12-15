using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletCustom : MonoBehaviour
{
    [SerializeField] 
    private float speed = 200f;
    private int damage;
    private PhotonView photonView;


    [PunRPC]
    private void DestroySelf()
    {
        gameObject.SetActive(false);
        if(photonView!=null)
        {
            if(photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            } 
        }        
        //Destroy(gameObject);
    }

    public void Initialize(int damageAmount){
        damage = damageAmount;
    }

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        //Invoke("DestroySelf", lifeTime);
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Limit")){
            other.GetComponent<Enemy>().TakeDamage(damage); 
            if(photonView.IsMine || PhotonNetwork.IsMasterClient){                
                DestroySelf();
            }else{      
                if(photonView!=null)          
                {
                    photonView.RPC("DestroySelf", RpcTarget.MasterClient);
                }                
            }            
        }   
    }

}
