using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Clientserver
{
    public class Clientlisten
    {
        public static void StartClient()
        {
            //buffer for incoming data from server
            byte[] bytes = new byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            //create a socket
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                //call the server
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.
                Console.Write("ask question ?_\n");
                string msg = Console.ReadLine();
                byte[] data = Encoding.ASCII.GetBytes(msg);
                //byte [] r2 = Encoding.ASCII.GetBytes("wha is the color of the hat <hat>");



                // Send the data through the socket.  
                int bytesSent = sender.Send(data);

                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("The answer :{0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
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


        }
    }
        
}

