using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte maxPlayers = 2;
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }

    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void CancelMatch()
    {
        if(PhotonNetwork.InRoom){
            if(PhotonNetwork.IsMasterClient){
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
            PhotonNetwork.LeaveRoom();            
        }        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }
    
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("ROOM CREATED");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("JOINED TO ROOM");
         if (PhotonNetwork.CurrentRoom.PlayerCount == 2) {
          PhotonNetwork.LoadLevel ("Game");
        } 
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 1) {
            Debug.Log ("Not Enough PLayers");          
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i=0; i<roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;                
            }
            Debug.Log(info);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2) {
          PhotonNetwork.LoadLevel ("Game");
        } 
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 1) {
            Debug.Log ("Not Enough PLayers");          
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }
    
}
