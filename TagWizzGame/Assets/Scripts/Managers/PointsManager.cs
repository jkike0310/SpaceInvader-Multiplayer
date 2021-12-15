using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PointsManager : MonoBehaviourPunCallbacks
{    
    private int points;
    private TMP_Text pointsText;
    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {        
        pointsText = GetComponent<TMP_Text>();
        if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Points")){
            points = (int)PhotonNetwork.LocalPlayer.CustomProperties["Points"];
        } else
        {
            points = LevelManager.instance.totalPoints;
        }
        pointsText.text = points.ToString();
    }
    public void GainPoints(int amount){
        if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Points")){
            points = (int)PhotonNetwork.LocalPlayer.CustomProperties["Points"];
        } 
        points += amount;
        if(!hashtable.ContainsKey("Points")){
            hashtable.Add("Points", points);
        }else{
            hashtable["Points"] = points;            
        }        
        foreach (Player player in PhotonNetwork.PlayerList) 
        {            
            player.SetCustomProperties(hashtable);            
        }    
        StartCoroutine(ShowPoints(amount));
    }

    private IEnumerator ShowPoints(int amount){
            
        yield return new WaitForSeconds(0.5f);
        if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Points")){
            pointsText.text = PhotonNetwork.LocalPlayer.CustomProperties["Points"].ToString();            
        }        
    }    

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(changedProps.ContainsKey("Points")){
            pointsText.text = changedProps["Points"].ToString(); 
            LevelManager.instance.totalPoints = (int) changedProps["Points"]; 
        }
    }
}
