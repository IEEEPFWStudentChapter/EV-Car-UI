﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using SocketCANSharp;
using SocketCANSharp.Network;

namespace EV_Car_UI.Models;

/// <summary>
/// The class that will get the data from can bus and update the UI with new data
/// </summary>
public class CanBus : IDataReceiver, IUpdateOnReceiveData
{
    /// <summary>
    /// The interface that will be updated when new data is received
    /// </summary>
    public IUpdateOnReceiveData _toUpdate { get; init; }
    
    /// <summary>
    /// Called by us when the program is started do all canbus receiving stuff here
    /// The IUpdateOnReceiveData will be used to update the app when new data comes
    /// by calling toUpdate.Update(data); 
    /// </summary>
    public CanBus(IUpdateOnReceiveData toUpdate)
    {
        _toUpdate = toUpdate;
        // passes random data this every 2 seconds
        // to simulate receiving data from canbus
        Task.Run(()=> new CanData(this).ReceiveData());
    }

    /// <summary>
    /// This function exists because of the RandomData class. When we get an update
    /// from there, we act as if we got it from the canbus so we call receive data
    /// hence we call that
    /// </summary>
    public void Update(TransmissionData data) => OnNewDataReceived(data);
    
    /// <summary>
    /// A function to be called when we data is received.
    /// For now just updates the interface that wants to be updated (i.e the MainWindowViewModel)
    /// </summary>
    public void OnNewDataReceived(TransmissionData data) => _toUpdate.Update(data);
}


public class CanData : IDataReceiver
{
    public IUpdateOnReceiveData _toUpdate { get; init; }

    public CanData(IUpdateOnReceiveData toUpdate)
    {
        _toUpdate = toUpdate;
    }

    public void OnNewDataReceived(TransmissionData data) => _toUpdate.Update(data);

    public async Task ReceiveData()
    {
        // I'm not incredibly familiar with asyncronous code so if this is dumb, then...
        // Uh, I guess I wouldn't be surprised.
        
        CanNetworkInterface can0 = CanNetworkInterface.GetAllInterfaces(true).First();
        using(RawCanSocket socket = new())
        {
    
            socket.Bind(can0);
            while(true) 
            {
                TransmissionData data = ReceivePacket(socket);
            
                OnNewDataReceived(data);
            }


        }
    }

    private TransmissionData ReceivePacket(RawCanSocket socket)
    {
        CanFrame frame1 = new();
        CanFrame frame2 = new();
        while(frame1.CanId != 1)
        {
            socket.Read(out frame1);
            Console.WriteLine("!!!"+frame1.ToString());    
        }
        Console.WriteLine("Frame1"+frame1.ToString());

        /*while(frame2.CanId != 2)
        {
            socket.Read(out frame2);
        }*/
        //Console.WriteLine("Frame2"+frame1.ToString());

        // I could set up nice code that you just configure stuff   
        // but I will not.
        

        // what wonderfully awful code. Indeed.
        TransmissionData toReturn = new TransmissionData(){
            mainBatteryVoltage =(BitConverter.ToInt16(frame1.Data, 0))/10.0f,
            batteryCurrent = (BitConverter.ToInt16(frame1.Data, 2))/10.0f,
            carBatteryVoltage = frame1.Data[4]/10.0f
        };

        return toReturn;


    }   
}


/// <summary>
/// A class that passes in random data
/// Serve as an example on how to update the app when new data comes
/// In this case the received data is data from the random calls
/// </summary>
public class RandomData : IDataReceiver
{
    /// <summary>
    /// The IUpdateOnReceiveData will be used to update the data when new data comes
    /// </summary>
    public RandomData(IUpdateOnReceiveData toUpdate)
    {
        _toUpdate = toUpdate;
    }

    public IUpdateOnReceiveData _toUpdate { get; init; }
    
    private readonly double Increment = 5;
    private readonly int loopsToWaitBeforeStarting = 1;
    private int loopsCompleted = 0;
    private float GetRandomFloat(float num) => num * 3 / 4f + loopsCompleted/100f * num;
    private bool GetRandomBool() => new Random().NextDouble() > 0.5;
    
    // This can be used as a sample to see how to set data
    // instead of GetRandom, use actual data
    public async Task RepeatSendRandomData()
    {
        var periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(2000));
        while (await periodicTimer.WaitForNextTickAsync())
        {
            // waits for 2 seconds before starting putting in random data
            if (loopsCompleted < loopsToWaitBeforeStarting)
            {
                loopsCompleted++;
                continue;
            }

            loopsCompleted++;
            OnNewDataReceived(new TransmissionData(
                    GetRandomFloat(300), 
                    GetRandomFloat(300),
                    GetRandomFloat(12),
                    GetRandomFloat(25),
                    GetRandomFloat(25),
                    GetRandomFloat(25),
                    GetRandomFloat(60),
                    GetRandomFloat(5000),
                    (float) Increment * (loopsCompleted - loopsToWaitBeforeStarting) % 100,
                    (float) Increment * (loopsCompleted - loopsToWaitBeforeStarting) % 100,
                    GetRandomBool(),
                    GetRandomBool(),
                    GetRandomBool()));
        }
    }
    public void OnNewDataReceived(TransmissionData data) => _toUpdate.Update(data);
    
}