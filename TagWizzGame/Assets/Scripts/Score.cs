using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text points;
    [SerializeField] private TMP_Text levelReached;
    [SerializeField] private TMP_Text enemiesKilled;

    [SerializeField] private Button buttonDone;

    private void Start()
    {
        levelReached.text = "Level Reached : "+ LevelManager.instance.GetLevel().ToString();
        enemiesKilled.text = "Enemies Killed: " + LevelManager.instance.GetSummatoryEnemiesKilled().ToString();
        // if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Points")){
        //     points.text = "Points: " + PhotonNetwork.LocalPlayer.CustomProperties["Points"].ToString();            
        // }  
        points.text = "Points: "+ LevelManager.instance.totalPoints.ToString();
        buttonDone.onClick.AddListener(()=>LeftRoom());
    }

    public void LeftRoom()
    {
        if(PhotonNetwork.IsMasterClient){
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        PhotonNetwork.LeaveRoom();
        Destroy(LevelManager.instance.gameObject);
        SceneManager.LoadScene(0); 
    }
}
