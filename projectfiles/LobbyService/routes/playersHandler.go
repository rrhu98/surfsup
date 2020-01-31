package routes

import (
	"encoding/json"
	"io/ioutil"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
	"github.com/rs/xid"
)

// Represents a player in the game
type player struct {
	ID          int    `json:"ID"`
	Token       string `json:"Token"`       // The Token of the player will serve as a secret between a player and the lobby service
	LobbyID     int    `json:"LobbyID"`     // The lobby the player is currently joined. 0 if player is not in any lobby.
	PlayerName  string `json:"PlayerName"`  // The name of the player shown in-game
	PlayerTeam  string `json:"PlayerTeam"`  // The team name or team color the player is currently on.
	PlayerReady bool   `json:"PlayerReady"` // Whether or not the player is ready to play whilst in a lobby.
}
type allPlayers []player

var players = allPlayers{}

func createPlayer(w http.ResponseWriter, r *http.Request) {
	reqBody, err := ioutil.ReadAll(r.Body)
	if err == nil {
		var newPlayer player
		json.Unmarshal(reqBody, &newPlayer)

		newPlayer.ID = len(players) + 1      // automatically set the appropriate player ID
		newPlayer.Token = xid.New().String() // automatically generate and assign a secure token

		// Check if the lobby ID exists in the request, if it does attempt to add the player to that given lobby

		if newPlayer.LobbyID != 0 && (newPlayer.LobbyID > 0 && newPlayer.LobbyID <= len(lobbies)) {
			lobby := lobbies[newPlayer.LobbyID-1]
			if lobby.Joinable {
				lobby.CurrentPlayers = append(lobby.CurrentPlayers, newPlayer.ID)
				lobby.Joinable = len(lobby.CurrentPlayers) < lobby.MaximumPlayers
				lobbies[newPlayer.LobbyID-1] = lobby
			} else {
				newPlayer.LobbyID = 0 // reset the lobby id as the player wasn't able to successfully join a lobby
				newPlayer.PlayerReady = false
			}
		} else {
			newPlayer.LobbyID = 0
			newPlayer.PlayerReady = false
		}

		players = append(players, newPlayer)
		w.WriteHeader(http.StatusCreated)
		json.NewEncoder(w).Encode(newPlayer)
	} else {
		w.WriteHeader(http.StatusPreconditionFailed)
	}
}

func getPlayerByID(w http.ResponseWriter, r *http.Request) {
	playerID, _ := strconv.Atoi(mux.Vars(r)["id"])

	if playerID > 0 && playerID <= len(players) {
		player := players[playerID-1]

		// filter player secret token
		player.Token = "redacted"

		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(player)
	} else {
		w.WriteHeader(http.StatusNoContent)
	}
}

func updatePlayerByID(w http.ResponseWriter, r *http.Request) {
	reqBody, err := ioutil.ReadAll(r.Body)

	if err == nil {
		playerID, _ := strconv.Atoi(mux.Vars(r)["id"])

		if playerID > 0 && playerID <= len(players) {
			currentPlayer := players[playerID-1]
			var playerUpdate player
			json.Unmarshal(reqBody, &playerUpdate)

			// Make sure the player is authenticated
			if playerUpdate.Token == currentPlayer.Token {

				// Make sure player ID is not modifed during the patch.
				playerUpdate.ID = currentPlayer.ID

				// Lobby has been modified modify data structures accordingly
				if playerUpdate.LobbyID != currentPlayer.LobbyID {
					// The player has left a lobby
					if playerUpdate.LobbyID == 0 {
						// get the lobby the player is curently in
						lobby := lobbies[currentPlayer.LobbyID-1]

						// Remove the players ID from the lobby it's currently in
						i := 0
						for _, playerID := range lobby.CurrentPlayers {
							if playerID != currentPlayer.ID {
								lobby.CurrentPlayers[i] = playerID
								i++
							}
						}
						lobby.CurrentPlayers = lobby.CurrentPlayers[:i]
						lobby.Joinable = len(lobby.CurrentPlayers) < lobby.MaximumPlayers
						lobbies[currentPlayer.LobbyID-1] = lobby

						// The player wants to join a lobby
					} else if currentPlayer.LobbyID == 0 {
						if playerUpdate.LobbyID > 0 && playerUpdate.LobbyID <= len(lobbies) {
							lobby := lobbies[playerUpdate.LobbyID-1]
							if lobby.Joinable {
								lobby.CurrentPlayers = append(lobby.CurrentPlayers, playerUpdate.ID)
								lobby.Joinable = len(lobby.CurrentPlayers) < lobby.MaximumPlayers
								lobbies[playerUpdate.LobbyID-1] = lobby
							} else {
								playerUpdate.LobbyID = 0
								playerUpdate.PlayerReady = false
							}
						}
					} else {
						// Dont change lobby as the player is trying to join a lobby
						// without leaving the one they are currently in first.
						playerUpdate.LobbyID = currentPlayer.LobbyID
					}

				}
				players[playerID-1] = playerUpdate
				w.WriteHeader(http.StatusOK)
				json.NewEncoder(w).Encode(playerUpdate)
			} else {
				w.WriteHeader(http.StatusUnauthorized)
			}
		} else {
			w.WriteHeader(http.StatusNoContent)
		}
	} else {
		w.WriteHeader(http.StatusPreconditionFailed)
	}
}
