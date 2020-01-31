using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Newtonsoft.Json;
using Mirror;

// class responsible for setting the port and Ip of the networked object 
public class PortAndIP : MonoBehaviour
{
    public NetworkManager NetworkManagerScript;
    public TelepathyTransport TelepathyTransportScript; 
    // getting json for port and Ip
    public void Set_Port_and_IP(int LobbyID)
    {

        JSONParser PortIPParser = new JSONParser();
        ServerInfo Port_and_IP = PortIPParser.GetIPAndPORT(LobbyID);

        // getting the IP and setting it from the NetworkManagerScript 
        var NetworkManagerScript = GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<NetworkManager>();
        NetworkManagerScript.networkAddress = Port_and_IP.IPAddress;


        // getting the port and setting it from the TelepathyTransport script
        var TelepathyTransportScript = GameObject.FindGameObjectWithTag("NetworkObject").GetComponent<TelepathyTransport>();
        TelepathyTransportScript.port = (ushort)Port_and_IP.Port;


    }

}
