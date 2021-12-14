using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    private bool startTimer = false;
    private double timerIncrementValue;
    private double startTime;
    //[SerializeField] private double timer = 120;
    [SerializeField] private TMP_Text timerText;
    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        
    
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
             
             startTime = PhotonNetwork.Time;
             startTimer = true;
             hashtable.Add("StartTime", startTime);
             PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        }else{

        }
        StartCoroutine(StartTimerGuest());
    }

    private IEnumerator StartTimerGuest(){
        yield return new WaitForSeconds(1f);
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
         {
             
             if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
             {
                 startTime = (double) PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];                 
             }             
             startTimer = true;             
         }
    }

    // Update is called once per frame
    void Update()
    {
        if (!startTimer) return;

        timerIncrementValue = PhotonNetwork.Time - startTime;
        timerText.text = timerIncrementValue.ToString();        
        
    }
}
