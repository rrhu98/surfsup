using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI; 

public class CreateLobbyPOST : MonoBehaviour
{
    InputField MaximumPlayers;

    public void CreateLobby(string gameType, int maxPlayers)
    {
        POSTLobby(gameType, maxPlayers); 
    }
 
 
    private void POSTLobby(string gameType, int maxPlayers)
    {
        try
        {
            string webAddr = "http://lobbyservice.mooo.com:8080/lobbies/create";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                if (gameType == "Solo")
                {
                    maxPlayers = 1; 
                }
                LobbyInfo newLobby = new LobbyInfo
                {
                    GameType = gameType,
                    MaximumPlayers = maxPlayers,

                };
                string json = LobbyInfo.CreateJSON(newLobby);
                //Debug.Log(json);
                //string json = "{\"GameType\":\"" + gameType + "\",\"" + "Joinable\":" + isJoinable + "," + "\"MaximumPlayers\":" + maxPlayers + "}";

                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                Console.WriteLine(responseText);
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}


