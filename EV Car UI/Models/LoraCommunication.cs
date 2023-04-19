using System;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;
using Path = System.IO.Path;

namespace EV_Car_UI.Models;

/// <summary>
/// The class that handles communication with the Lora module
/// </summary>
public class LoraCommunication : DataReceiver, IDataSender
{
    /// <summary>
    /// The socket we will use to send to the Lora module
    /// </summary>
    private readonly Socket _senderSocket;

    /// <summary>
    /// The socket we will use to receive from the Lora module
    /// </summary>
    private readonly Socket _receiverSocket;
    
    public LoraCommunication(IUpdateOnReceiveData toUpdate) : base(toUpdate)
    {
        _senderSocket = new Socket(AddressFamily.Unix, SocketType.Dgram, ProtocolType.Udp);
        
        RegisterSenderSocket();
    }

    private void RegisterSenderSocket()
    {
        var senderSocketLocation =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "e32.tx.data");

        if (File.Exists(senderSocketLocation))
        {
            File.Delete(senderSocketLocation);
        }

        _senderSocket.Bind(new UnixDomainSocketEndPoint(senderSocketLocation));
    }

    /// <summary>
    /// The function that sends data to the Lora module
    /// </summary>
    public void SendData(TransmissionData data)
    {
        string serializedData = JsonConvert.SerializeObject(data);
        byte[] byteData = System.Text.Encoding.ASCII.GetBytes(serializedData);
        _senderSocket.SendTo(byteData, new UnixDomainSocketEndPoint("/run/e32.data"));
    }
}