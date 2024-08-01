using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace ConsoleClient.Model;
public class Client
{
    IPEndPoint endp;
    Socket socket;
    public byte[] Buffer = new byte[1024];

    public Client(string ip, int port)
    {
        this.endp = new IPEndPoint(IPAddress.Parse(ip), port);
        this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
    }

    public bool IsConnected() => this.socket.Connected;
    public void Connect()
    {
        try
        {
            socket.Connect(endp);
        }
        catch (SocketException ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public void Disconnect()
    {
        try
        {
            this.socket.Shutdown(SocketShutdown.Both);
            this.socket.Close();
        }
        catch (SocketException ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public int Receive() => this.socket.Receive(this.Buffer);
    public void Send(string strSend)
    {
        if (this.socket != null)
            this.socket.Send(System.Text.Encoding.UTF8.GetBytes(strSend));
    }
}