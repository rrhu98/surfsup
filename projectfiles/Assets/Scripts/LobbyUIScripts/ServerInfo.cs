using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class ServerInfo : MonoBehaviour
{
    public int ID;
    public int LobbyID;
    public string IPAddress;
    public int Port;

    public static ServerInfo CreateFromJSON(string jsonString)
    {
        return JsonConvert.DeserializeObject<ServerInfo>(jsonString);

    }

}
