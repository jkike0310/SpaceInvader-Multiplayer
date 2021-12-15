using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(PhotonView))]
public class LevelManager : MonoBehaviourPunCallbacks
{
    public static LevelManager instance;
    [SerializeField] public int totalEnemies;
    public int actualEnemies;

    [SerializeField] private int enemyKilled = 0;

    [SerializeField] private int enemiesKilledSum = 0;
    
    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] private int level = 1;

    public int totalPoints;
    
    private PhotonView photonViewLM;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            if(instance!=this)
            {
                Destroy(LevelManager.instance.gameObject);
                instance = this;
            }            
        }
        
        DontDestroyOnLoad(gameObject);
        photonViewLM = GetComponent<PhotonView>();           
    }
    

    public void KillEnemy()
    {
        if(PhotonNetwork.InRoom && photonViewLM!=null){            
            photonViewLM.RPC("RestEnemy", RpcTarget.All);                                
        }   
    }
    [PunRPC]
    private void RestEnemy()
    {
        enemyKilled++;
        enemiesKilledSum ++;
        actualEnemies = totalEnemies - enemyKilled;        
        if(actualEnemies<=0)
        {
            if(photonViewLM!=null)
            {
                photonViewLM.RPC("NextLevel", RpcTarget.All);
            }            
        }
        Debug.Log("RESTING ENEMY");
    }

    public int GetSummatoryEnemiesKilled()
    {
        return enemiesKilledSum;
    }

    public int GetLevel()
    {
        return level;
    }

    [PunRPC]
    public void NextLevel()
    {
        Debug.Log("ADDING LEVEL");
       if(PhotonNetwork.InRoom && actualEnemies <= 0){            
           actualEnemies = 1;
            level++;
            enemyKilled = 0;
            LoadLevel();
        } 

        Debug.Log("LEVELLL "+ level)   ;
    }

    private void LoadLevel()
    {        
        PhotonNetwork.LoadLevel(1);
    }

    public void Reset()
    {
        enemyKilled = 0;
        enemiesKilledSum = 0;
        totalEnemies = 0;
        level = 1;
    }    

}
