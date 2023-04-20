using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    private bool _isConnected = false;
    
    public LoraCommunication(IUpdateOnReceiveData toUpdate)
    {
        _toUpdate = toUpdate;
        
        Trace.WriteLine("Setting up socket");
        _senderSocket = new Socket(AddressFamily.Unix, SocketType.Dgram, ProtocolType.Unspecified);
        
        RegisterSenderSocket();
    }

    private void RegisterSenderSocket()
    {
        var senderSocketLocation =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "e32.tx.data");

        Trace.WriteLine("socket location: " + senderSocketLocation);
        
        if (File.Exists(senderSocketLocation))
        {
            File.Delete(senderSocketLocation);
        }

        Trace.WriteLine("binding to socket");
        try
        {
            _senderSocket.Bind(new UnixDomainSocketEndPoint(senderSocketLocation));
        }
        catch (Exception e)
        {
            Trace.TraceError("Failed to bind to socket: " + e);
        }

        Trace.WriteLine("successfully binded up socket");
        _isConnected = true;
    }

    /// <summary>
    /// The function that sends data to the Lora module
    /// </summary>
    public void SendData(TransmissionData data)
    {
        if (!_isConnected)
        {
            Trace.WriteLine($"Unable to send data because of no connection {DateTime.Now}");
            return;
        }
        List<byte> byteData = new List<byte>();
        
        byteData.AddRange(BitConverter.GetBytes(data.mainBatteryVoltage));
        byteData.AddRange(BitConverter.GetBytes(data.batteryCurrent));
        byteData.AddRange(BitConverter.GetBytes(data.carBatteryVoltage));
        byteData.AddRange(BitConverter.GetBytes(data.motorTemperature));
        byteData.AddRange(BitConverter.GetBytes(data.inverterTemperature));
        byteData.AddRange(BitConverter.GetBytes(data.batteryTemperature));
        byteData.AddRange(BitConverter.GetBytes(data.wheelSpeed));
        byteData.AddRange(BitConverter.GetBytes(data.motorSpeed));
        byteData.AddRange(BitConverter.GetBytes(data.throttlePercentage));
        byteData.AddRange(BitConverter.GetBytes(data.brakePercentage));
        byteData.AddRange(BitConverter.GetBytes(data.derating));
        byteData.AddRange(BitConverter.GetBytes(data.batteryConnector));
        byteData.AddRange(BitConverter.GetBytes(data.bridgeControl));
        
        //send 13 pieces of data with 10 floats. rest will be considered bools
        byteData.Insert(0, 13);
        byteData.Insert(1, 10);
        _senderSocket.SendTo(byteData.ToArray(), new UnixDomainSocketEndPoint("/run/e32.data"));
        Trace.WriteLine($"Sent data {DateTime.Now}");
    }
    
    /// <summary>
    /// A function to be called when we data is received.
    /// For now just updates the interface that wants to be updated (i.e the MainWindowViewModel)
    /// </summary>
    public void OnNewDataReceived(TransmissionData data) => _toUpdate.Update(data);
}