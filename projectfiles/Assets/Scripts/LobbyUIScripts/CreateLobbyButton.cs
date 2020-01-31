using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CreateLobbyButton : MonoBehaviour
{
    List<string> gameTypeOptions = new List<string>() { "Please select", "Solo", "Multiplayer" };

    public InputField MaxPlayers;
    public Dropdown gameTypeDropdown;

    // vars for the selected options
    public string gameType;


    public void Dropdown_IndexChanged_GameType(int index)
    {
        Debug.Log(gameTypeOptions[index]);
        gameType = gameTypeOptions[index];
        if (gameType == "Solo")
        {
            MaxPlayers.text = "1"; 
        }
    }

    private void Start()
    {
        PopulateLists();
    }

    void PopulateLists()
    {
        gameTypeDropdown.AddOptions(gameTypeOptions);
    }

    public void OnClick()
    {
        //Debug.Log(MaxPlayers.text);
        CreateLobbyPOST newLobby = new CreateLobbyPOST();
        //Debug.Log(gameType + " " + isJoinable + " " + MaxPlayers.text); 
        newLobby.CreateLobby(gameType, Int32.Parse(MaxPlayers.text));
        Debug.Log("Lobby created"); 
    }


}