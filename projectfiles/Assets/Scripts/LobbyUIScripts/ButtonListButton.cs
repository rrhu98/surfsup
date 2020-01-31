using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    [SerializeField]
    private ButtonListControl lobbyNum;

    // string to hold text of button 
    private int LobbyIDCopy; 

    // function to set button text
    public void SetText(int LobbyID, string ButtonTextString)
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        LobbyIDCopy = LobbyID;
        myText.text = ButtonTextString; 
    }

    // function to provide logic on a click 
    public void OnClick()
    {

        lobbyNum.ButtonClicked(LobbyIDCopy); 
    }
}
