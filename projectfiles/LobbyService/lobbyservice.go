package main

import (
	"fmt"
	"net/http"

	r "./routes"
)

func lobbyService(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "Surfs Up! - Lobby Service")
	fmt.Fprintf(w, "Current Version: 1.0.0")
}

func main() {
	router := r.Router()
	router.HandleFunc("/", lobbyService)
	http.ListenAndServe(":8080", router)

}
