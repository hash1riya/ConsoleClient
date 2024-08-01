using System.Diagnostics;
using System.Net.Sockets;

namespace ConsoleClient;
internal class Program
{
    public static async Task Main(string[] args)
    {
        string ip = "127.0.0.1";
        int port = 1024;
        Model.Client client = new Model.Client(ip, port);

        string sendStr = string.Empty;
        int bytesRead;
        try
        {
            client.socket.Connect(ip, port);

            if (client.IsConnected())
            {
                Console.WriteLine(
                    $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                    $"SYSTEM: " +
                    $"Connected!");

                while (true)
                {
                    Console.Write("> ");
                    sendStr = Console.ReadLine();

                    if (!string.IsNullOrEmpty(sendStr) && sendStr != "exit")
                    {
                        await client.socket.SendAsync(System.Text.Encoding.UTF8.GetBytes(sendStr));

                        bytesRead = await client.Receive();
                        Console.WriteLine(
                            $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                            $"server: " +
                            $"{System.Text.Encoding.UTF8.GetString(client.Buffer, 0, bytesRead)}");
                    }
                    else if (sendStr == "exit")
                        break;
                }
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Console.WriteLine(
                $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                $"SYSTEM: " +
                $"Remote host is unreacheble");
        }
        finally
        {
            Console.WriteLine(
                $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                $"SYSTEM: " +
                $"Disconnecting...");
            client.Disconnect();
        }
    }
}