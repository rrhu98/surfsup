package serverutils

import (
	"encoding/json"
	"io/ioutil"
	"net/http"
	"os"
	"os/exec"
	"strconv"
	"strings"
)

const ConfigFile = "config.json"

type Config struct {
	Timeout        int    `json:"timeout"`        // Timeout in seconds to automatically terminate a server after
	ExecutablePath string `json:"executablePath"` // The path to the headless server executable
}

// ReadConfig() will read the configuration file and return the appropriate
// Config struct with the values unmarshalled.
func ReadConfig() Config {
	var config Config
	configFile, _ := os.Open(ConfigFile)
	readByte, _ := ioutil.ReadAll(configFile)
	json.Unmarshal(readByte, &config)
	return config
}

// LaunchServer() will launch a unity game server given a port to run it on
func LaunchServer(port int) {
	if PortInUse(port) {
		panic("Attempting to Launch a Server on a port already in-use!")
	}
	config := ReadConfig()

	// Run the unity game server on a tmux session
	// tmux new-session -d -s <port_number> timeout <timeout> <executable> --port <port_number>
	cmd := exec.Command("tmux", "new-session", "-d", "-s", strconv.Itoa(port),
		"timeout", strconv.Itoa(config.Timeout), config.ExecutablePath, "--port", strconv.Itoa(port))

	err := cmd.Run()
	if err != nil {
		panic(err)
	}

}

// ExternalIPAddr() will get the external IP address of the current machine.
func ExternalIPAddr() string {
	// IP address are maximum 32 bytes
	buffer := make([]byte, 32)
	resp, err := http.Get("http://ipv4.icanhazip.com/")
	if err != nil {
		panic(err)
	}
	val, _ := resp.Body.Read(buffer)
	return strings.TrimRight(string(buffer[:val]), "\r\n")

}
