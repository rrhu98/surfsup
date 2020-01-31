using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { set; get; }

    // game objects for each screen on the menu
    //public GameObject createPlayerMenu; 
    public GameObject mainMenu;
    public GameObject lobbyListMenu;
    public GameObject createLobbyMenu;
    public GameObject lobbyJoinMenu;
    public GameObject inLobbyMenu; 

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        //createPlayerMenu.SetActive(true);
        mainMenu.SetActive(true); 
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        lobbyJoinMenu.SetActive(false);
        inLobbyMenu.SetActive(false);
        DontDestroyOnLoad(gameObject); 
    }

    // host button
    public void CreateLobbyButton()
    {
        Debug.Log("Create Lobby");
        createLobbyMenu.SetActive(true);
        mainMenu.SetActive(false);
        lobbyJoinMenu.SetActive(false);
        lobbyListMenu.SetActive(false);
        inLobbyMenu.SetActive(false); 
    }

    // button to display lobbies
    public void LobbyListButton()
    {
        Debug.Log("list of lobbies");
        lobbyListMenu.SetActive(true);
        mainMenu.SetActive(false);
        lobbyJoinMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        inLobbyMenu.SetActive(false);

    }

    // button that is each lobby instance in lobbyListMenu
    public void LobbyInstanceButton()
    {
        lobbyJoinMenu.SetActive(true);
        mainMenu.SetActive(false);
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false); 

    }
    // button to switch to in lobby canvas
    public void InLobbyCanvasButton()
    {
        mainMenu.SetActive(false);
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        lobbyJoinMenu.SetActive(false);
        inLobbyMenu.SetActive(true); 

    }

    // backbutton
    public void BackButton()
    {
        mainMenu.SetActive(true);
        lobbyListMenu.SetActive(false);
        createLobbyMenu.SetActive(false);
        lobbyJoinMenu.SetActive(false);

    }




}
