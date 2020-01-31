using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobby : MonoBehaviour
{

    public InputField PlayerName;
    //public InputField PlayerTeamColour;
    [SerializeField]
    private Text LobbyIDText;
    private int lobbyID;
    // ref to in lobby canvas
    public InLobby inLobby;


    public void SetLobbyID(int LobbyID)
    {
        LobbyIDText.text = "LobbyID: " + LobbyID.ToString();
        lobbyID = LobbyID;

    }

    // function to create a player by sending a post request to API
    // function then returns a new player of type PlayerInfo
    public PlayerInfo CreatePlayer(int LobbyID, string PlayerName)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://lobbyservice.mooo.com:8080/players/create");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            PlayerInfo newPlayer = new PlayerInfo
            {
                LobbyID = LobbyID,
                PlayerName = PlayerName,
                //PlayerTeam = PlayerTeam,
                //PlayerReady = true
            };

            string json = PlayerInfo.CreateJSON(newPlayer);

            Debug.Log(json);

            //'{"PlayerName": "Player 1", "PlayerTeam": "Green"}'
            //Debug.Log("{\"LobbyID\":" + LobbyID + ",\"" + "PlayerName\":" + "\"" + PlayerName + "\"" + "," + "\"PlayerTeam\":" + "\"" + PlayerTeam + "\"" + "}");
            //string json = "{\"LobbyID\":" + LobbyID + ",\"" + "PlayerName\":" + "\"" + PlayerName + "\"" + "," + "\"PlayerTeam\":" + "\"" + PlayerTeam + "\"" + "}";

            streamWriter.Write(json); 
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            // creating a player with the result
            PlayerInfo NewPlayer = PlayerInfo.CreateFromJSON(result);
            return NewPlayer; 

        }
    }

    ////function to join lobby with created player by sending patch request to API
    //public void Join(PlayerInfo NewPlayer, int LobbyID)
    //{
    //    Debug.Log("Player ID: " + NewPlayer.ID);
    //    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://lobbyservice.mooo.com:8080/players/" + NewPlayer.ID);
    //    httpWebRequest.ContentType = "application/json";
    //    httpWebRequest.Method = "PATCH";

    //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    //    {
    //        //{"LobbyID":1,"PlayerName":"Faiz","PlayerTeam":"Blue", "Token":"ur token"}
    //        Debug.Log("{\"LobbyID\":" + LobbyID + "," + "\"PlayerName\":" + "\"" + NewPlayer.PlayerName + "\"," + "\"PlayerTeam\":" + "\"" + NewPlayer.PlayerTeam + "\"," + "\"Token\":" + "\"" + NewPlayer.Token + "\"" + "}");
    //        string json = "{\"LobbyID\":" + LobbyID + "," + "\"PlayerName\":" + "\"" + NewPlayer.PlayerName + "\"," + "\"PlayerTeam\":" + "\"" + NewPlayer.PlayerTeam + "\"," + "\"Token\":" + "\"" + NewPlayer.Token + "\"" + "}";
    //        streamWriter.Write(json);


    //    }
    //    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //    {
    //        var result = streamReader.ReadToEnd();
    //        // creating a player with the result
    //        PlayerInfo NewPlayer = PlayerInfo.CreateFromJSON(result);
    //        return NewPlayer;

    //    }

    //}

    public void OnClick()
    {

        //// creates a new player      
        PlayerInfo newPlayer = CreatePlayer(this.lobbyID, PlayerName.text);

        // passing the lobby id and token to in lobby script
        inLobby = GameObject.FindObjectOfType<InLobby>();
        inLobby.SetLobby(lobbyID);
        inLobby.SetPlayer(newPlayer); 

    }


}
