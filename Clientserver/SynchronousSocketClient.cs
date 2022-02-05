using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
public class SynchronousSocketClient
{
    /*
    * StartClient()
    * 
    * purpose: The actual client code
     * 
    * written by Microsoft and updated by Mike Audet
    * 
    * Purpose: creates a socket and sends data to the server.
    * Sent data is echoed back from the server.
    * 
    * params: none:
    * 
    * returns: void
*/

    public static void StartClient()
    {
        // Data buffer for incoming data (from the server).  
        byte[] bytes = new byte[1024];

        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.
            // For our purposes, Dns.GetHostName() can be replaced by "localhost"
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            //get the first IP address from the list
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            //combine  our ip address and port into an object
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                //call the server
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array for seding across the network.  
                byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);

                //print out out echoed text
                Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }//end inner try for sending data
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }//end outer try for connection
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}


