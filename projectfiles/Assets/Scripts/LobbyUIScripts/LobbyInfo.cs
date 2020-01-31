using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; 

// class to hold labby info used to deserialize json 
public class LobbyInfo
{


    public int ID;
    public string GameType;
    public bool Joinable; 
    public bool IsStarted;
    public int[] CurrentPlayers;
    public int MaximumPlayers;

    public static LobbyInfo CreateFromJSON(string jsonString)
    {
        return JsonConvert.DeserializeObject<LobbyInfo>(jsonString); 
    }
    public static string CreateJSON(LobbyInfo lobby)
    {
        return JsonConvert.SerializeObject(lobby);
    }



}
