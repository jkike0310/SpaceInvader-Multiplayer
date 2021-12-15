using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PlayerShip))]
public class NormalShoot : MonoBehaviour
{    
    [SerializeField] private Transform originBullet;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private float rate;
    [SerializeField] private AudioClip shootClip;
    private float initialTime = 0f;
    private bool canShoot = true;

    private PlayerShip playerShip;

    private void Start()
    {
        playerShip = GetComponent<PlayerShip>();
        playerShip.OnShoot.AddListener(this.Shoot);
    } 

    private void Update()
    {
        if(!canShoot){
            Recharge();
        }
    }

    public void Shoot()
    {
        if(canShoot){
            GameObject newBullet = PhotonNetwork.Instantiate(prefabBullet.name, originBullet.position, Quaternion.identity, 0);
            newBullet.GetComponent<BulletCustom>().Initialize(playerShip.damage);
            SoundManager.instance.PlayEffect(shootClip);
            canShoot = false;            
        }
    }

    private void Recharge(){
        if(initialTime>= rate){
            initialTime = 0f;
            canShoot = true;            
        }
        initialTime+= Time.deltaTime;        
    }
}
