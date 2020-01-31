package serverutils

import (
	"net"
	"strconv"
)

// GeneratePort() generates a unused port number
// which can be binded to by a server
func GeneratePort() int {
	// specifying port 0 is asking the Kernel to automatically bind to a unused port
	listen, _ := net.Listen("tcp", ":0")
	listen.Close()                           // Close listener
	return listen.Addr().(*net.TCPAddr).Port // Return the port given by the Kernel
}

// PortInUse() returns whether or not a given port is already binded
// to a service on the current system.
func PortInUse(port int) bool {
	_, err := net.Listen("tcp", ":"+strconv.Itoa(port))
	return err != nil
}
