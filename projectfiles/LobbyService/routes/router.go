package routes

import "github.com/gorilla/mux"

func Router() *mux.Router {
	router := mux.NewRouter().StrictSlash(true)
	router.HandleFunc("/lobbies", getAllLobbies).Methods("GET")
	router.HandleFunc("/lobbies/{id}", getLobbyByID).Methods("GET")
	router.HandleFunc("/lobbies/create", createLobby).Methods("POST")
	router.HandleFunc("/players/create", createPlayer).Methods("POST")
	router.HandleFunc("/players/{id}", getPlayerByID).Methods("GET")
	router.HandleFunc("/players/{id}", updatePlayerByID).Methods("PATCH")
	router.HandleFunc("/players/create", createPlayer).Methods("POST")
	router.HandleFunc("/servers/{id}", getServerByLobbyID).Methods("GET")
	return router
}
