package routes

import (
	"encoding/json"
	"io/ioutil"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
)

// Represents a lobby room in the game
type lobby struct {
	ID             int    `json:"ID"`
	GameType       string `json:"GameType"`       // The type of game the lobby is hosting, ex. Teams/Solo.
	Joinable       bool   `json:"Joinable"`       // Whether or not the lobby is joinable
	IsStarted      bool   `json:"IsStarted"`      // Whether or not the lobby is already started and the players in-game
	CurrentPlayers []int  `json:"CurrentPlayers"` // The IDs of the current players in the lobby / game.
	MaximumPlayers int    `json:"MaximumPlayers"` // The maximum number of players allowed in the lobby / game.
}

type allLobbies []lobby

var lobbies = allLobbies{}

func createLobby(w http.ResponseWriter, r *http.Request) {
	reqBody, err := ioutil.ReadAll(r.Body)
	if err == nil {
		var newLobby lobby
		json.Unmarshal(reqBody, &newLobby)

		// Make sure the Maximum Players in the lobby are atleast one.
		if newLobby.MaximumPlayers > 0 {
			newLobby.ID = len(lobbies) + 1    // automatically set the appropriate lobby ID
			newLobby.Joinable = true          // a new lobby should be joinable
			newLobby.CurrentPlayers = []int{} // a new lobby should have no players in it by default

			lobbies = append(lobbies, newLobby)
			w.WriteHeader(http.StatusCreated)
			json.NewEncoder(w).Encode(newLobby)
		} else {
			w.WriteHeader(http.StatusForbidden)
		}
	} else {
		w.WriteHeader(http.StatusPreconditionFailed)
	}
}

func getAllLobbies(w http.ResponseWriter, r *http.Request) {
	json.NewEncoder(w).Encode(lobbies)
}

func getLobbyByID(w http.ResponseWriter, r *http.Request) {
	lobbyID, _ := strconv.Atoi(mux.Vars(r)["id"])
	if lobbyID > 0 && lobbyID <= len(lobbies) {
		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(lobbies[lobbyID-1])
	} else {
		w.WriteHeader(http.StatusNoContent)
	}
}
