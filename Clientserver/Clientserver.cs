using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebSocketSharp;
namespace Clientserver
{
    public static class Clientserver
    {
        public static int Main(String[] args)
        {
            //run our test - make sure server is running first!
            //SynchronousSocketClient.StartClient();
            Clientlisten.StartClient();
          
            return 0;
        }
    }
}
