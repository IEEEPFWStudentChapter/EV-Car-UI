using System;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;
using Path = System.IO.Path;

namespace EV_Car_UI.Models;

/// <summary>
/// The class that handles communication with the Lora module
/// </summary>
public class LoraCommunication : IDataReceiver, IDataSender
{
    /// <summary>
    /// The interface that will be updated when new data is received
    /// </summary>
    public IUpdateOnReceiveData _toUpdate { get; init; }
    
    /// <summary>
    /// The socket we will use to send to the Lora module
    /// </summary>
    private readonly Socket _senderSocket;

    /// <summary>
    /// The socket we will use to receive from the Lora module
    /// </summary>
    private readonly Socket _receiverSocket;
    
    public LoraCommunication(IUpdateOnReceiveData toUpdate)
    {
        _toUpdate = toUpdate;
        
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
    
    /// <summary>
    /// A function to be called when we data is received.
    /// For now just updates the interface that wants to be updated (i.e the MainWindowViewModel)
    /// </summary>
    public void OnNewDataReceived(TransmissionData data) => _toUpdate.Update(data);
}