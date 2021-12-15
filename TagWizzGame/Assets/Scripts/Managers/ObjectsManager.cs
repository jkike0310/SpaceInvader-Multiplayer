using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectsManager : MonoBehaviourPunCallbacks
{
    public static ObjectsManager instance;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject gameOverPanel;
    private bool isGameOver;

    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {

        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (PlayerShip.LocalPlayerInstance == null)
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, -3f, 0f), Quaternion.identity, 0);
        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        GameOver();
    }

    public void GameOver()
    {
        if(PhotonNetwork.InRoom){            

            if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("GameOver")){
                isGameOver = (bool)PhotonNetwork.LocalPlayer.CustomProperties["GameOver"];
            } 
            isGameOver = true;
            if(!hashtable.ContainsKey("GameOver")){
                hashtable.Add("GameOver", isGameOver);
            }else{
                hashtable["GameOver"] = isGameOver;            
            }        
            foreach (Player player in PhotonNetwork.PlayerList) 
            {            
                player.SetCustomProperties(hashtable);            
            }    
                
        }    
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void LeftRoom()
    {
        if(PhotonNetwork.IsMasterClient){
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0); 
    }

    public void GoToScore()
    {
        SceneManager.LoadScene(2);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(changedProps.ContainsKey("GameOver")){
            isGameOver = true;
            gameOverPanel.SetActive(true);
        }   
    }
}
