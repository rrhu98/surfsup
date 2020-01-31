using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerInfo 
{
    public int ID { get; set; }
    public string Token { get; set; }
    public int LobbyID { get; set; }
    public string PlayerName { get; set; }
    public string PlayerTeam { get; set; }
    public bool PlayerReady { get; set; }

    public static PlayerInfo CreateFromJSON(string jsonString)
    {
        return JsonConvert.DeserializeObject<PlayerInfo>(jsonString);
    }
    public static string CreateJSON(PlayerInfo cur_player)
    {
        return JsonConvert.SerializeObject(cur_player); 
    }

}
