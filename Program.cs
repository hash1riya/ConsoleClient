using System.Diagnostics;

namespace ConsoleClient;
internal class Program
{
    public static async Task Main(string[] args)
    {
        string ip = "127.0.0.1";
        int port = 1024;
        Model.Client client = new Model.Client(ip, port);

        string sendStr = "Hello server";
        int bytesRead;
        try
        {
            client.Connect();

            if(client.IsConnected())
                Console.WriteLine(
                    $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                    $"SYSTEM: " +
                    $"Connected!");

            client.Send(sendStr);
            Console.WriteLine(
                $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                $"me: " +
                $"{sendStr}");

            bytesRead = await client.Receive();
            Console.WriteLine(
                $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                $"server: " +
                $"{System.Text.Encoding.UTF8.GetString(client.Buffer, 0, bytesRead)}");
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