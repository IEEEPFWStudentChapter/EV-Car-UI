﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace EV_Car_UI.Models;

/// <summary>
/// The class that will get the data from can bus and update the UI with new data
/// </summary>
public class CanBus : DataReceiver, IUpdateOnReceiveData
{
    /// <summary>
    /// Called by us when the program is started do all canbus receiving stuff here
    /// The IUpdateOnReceiveData will be used to update the app when new data comes
    /// by calling toUpdate.Update(data); 
    /// </summary>
    public CanBus(IUpdateOnReceiveData toUpdate) : base(toUpdate)
    {
        // passes random data this every 2 seconds
        // to simulate receiving data from canbus
        Task.Run(()=> new RandomData(this).RepeatSendRandomData());
    }

    /// <summary>
    /// This function exists because of the RandomData class. When we get an update
    /// from there, we act as if we got it from the canbus so we call receive data
    /// hence we call that
    /// </summary>
    public void Update(TransmissionData data) => OnNewDataReceived(data);
}

/// <summary>
/// A class that passes in random data
/// Serve as an example on how to update the app when new data comes
/// In this case the received data is data from the random calls
/// </summary>
public class RandomData : DataReceiver
{
    /// <summary>
    /// The IUpdateOnReceiveData will be used to update the data when new data comes
    /// </summary>
    public RandomData(IUpdateOnReceiveData toUpdate) : base(toUpdate) { }
    
    private readonly double Increment = 5;
    private readonly int loopsToWaitBeforeStarting = 10;
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
            if (loopsCompleted < 1)
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
                    Increment * (loopsCompleted - loopsToWaitBeforeStarting) % 100,
                    Increment * (loopsCompleted - loopsToWaitBeforeStarting) % 100,
                    GetRandomBool(),
                    GetRandomBool(),
                    GetRandomBool()));
        }
    }
}