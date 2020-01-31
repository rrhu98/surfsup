package routes

import (
	"encoding/json"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"

	"../serverutils"
)

// Represents a headless unity server
type server struct {
	ID        int    `json:"ID"`
	LobbyID   int    `json:"LobbyID"`   // The lobby ID the server is started with
	IPAddress string `json:"IPAddress"` // The IPAddress of the unity game server
	Port      int    `json:"Port"`      // The Port of the unity game server
}
type allServers []server

var servers = allServers{}

func getServerByLobbyID(w http.ResponseWriter, r *http.Request) {
	lobbyID, _ := strconv.Atoi(mux.Vars(r)["id"])

	if lobbyID > 0 && lobbyID <= len(lobbies) {
		lobby := lobbies[lobbyID-1]
		// Current hard coded condition to detected when a unity lobby is ready
		// For now assume when the lobby is full we can create or get a server
		if len(lobby.CurrentPlayers) == lobby.MaximumPlayers {

			// Try getting the server based on the lobbyID
			var unityServer *server
			for _, server := range servers {
				if server.LobbyID == lobbyID {
					unityServer = &server
				}
			}

			// Create a new server as no server was found given the lobbyID
			if unityServer == nil {

				port := serverutils.GeneratePort()
				serverutils.LaunchServer(port)

				unityServer = &server{
					ID:        len(servers) + 1,
					LobbyID:   lobbyID,
					IPAddress: serverutils.ExternalIPAddr(),
					Port:      port,
				}

				servers = append(servers, *unityServer)
			}
			w.WriteHeader(http.StatusOK)
			json.NewEncoder(w).Encode(&unityServer)
		} else {
			w.WriteHeader(http.StatusPreconditionFailed)
		}
	} else {
		w.WriteHeader(http.StatusNoContent)
	}
}
