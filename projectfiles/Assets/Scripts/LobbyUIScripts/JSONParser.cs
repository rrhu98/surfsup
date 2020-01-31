using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Newtonsoft.Json;

// class to parse json and return relevent information
public class JSONParser 
{
    // url for the API 
    private string url = "http://lobbyservice.mooo.com:8080/";

    // function to convert JSON string into list of LobbyInfo objects
    public List<LobbyInfo> GetListLobbies()
    {
        var json = new WebClient().DownloadString(url + "/lobbies");
        var LobbyList = JsonConvert.DeserializeObject<List<LobbyInfo>>(json);
        return LobbyList; 
    }

    public ServerInfo GetIPAndPORT(int LobbyID)
    {
        var json = new WebClient().DownloadString(url + "/servers/" + LobbyID);
        var ServerJson = JsonConvert.DeserializeObject<ServerInfo>(json);
        return ServerJson;

    }
    public PlayerInfo GetPlayer(int playerID)
    {
        var json = new WebClient().DownloadString(url + "/players/" + playerID);
        var playerJson = JsonConvert.DeserializeObject<PlayerInfo>(json);
        return playerJson; 
    }

    public LobbyInfo GetLobby(int LobbyID)
    {
        var json = new WebClient().DownloadString(url + "/lobbies/" + LobbyID);
        var lobbyJson = JsonConvert.DeserializeObject<LobbyInfo>(json);
        return lobbyJson;

    }

}
