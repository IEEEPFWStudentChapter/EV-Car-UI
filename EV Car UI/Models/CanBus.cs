using System;
using System.Threading;
using System.Threading.Tasks;

namespace EV_Car_UI.Models;

// The class that will get the data from can bus and update the UI with new data
public static class CanBus
{
    // Called by us when the program is started
    // do all canbus receiving stuff here
    // and when data is received call Data.Set...() to set values in UI
    public static async void Start()
    {
        // passes random data every 1/2 seconds
        await new RandomData().RepeatSendRandomData();
    }
}

// passes in random data
// Remove it when actual data comes.
// Can serve as an example on how to update the UI when new data comes
public class RandomData
{
    private readonly double Increment = 5;
    private readonly int loopsToWaitBeforeStarting = 10;
    private int loopsCompleted = 0;
    private float GetRandomFloat(float num) => num * 3 / 4f + loopsCompleted/100f * num;
    private bool GetRandomBool() => new Random().NextDouble() > 0.5;
    
    // This can be used as a sample to see how to set data
    // instead of GetRandom, use actual data
    public async Task RepeatSendRandomData()
    {
        var periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(500));
        while (await periodicTimer.WaitForNextTickAsync())
        {
            // waits for 5 seconds before starting putting in random data
            if (loopsCompleted < 10)
            {
                loopsCompleted++;
                continue;
            }

            loopsCompleted++;
            Data.SetMainBatteryVoltage(GetRandomFloat(300));
            Data.SetBatteryCurrent(GetRandomFloat(300));
            Data.SetCarBatteryVoltage(GetRandomFloat(12));
            Data.SetMotorTemperature(GetRandomFloat(25));
            Data.SetInverterTemperature(GetRandomFloat(25));
            Data.SetBatteryTemperature(GetRandomFloat(25));
            Data.SetWheelSpeed(GetRandomFloat(60));
            Data.SetMotorSpeed(GetRandomFloat(5000));
            Data.SetThrottlePercentage(Increment*(loopsCompleted - loopsToWaitBeforeStarting) % 100);
            Data.SetBrakePercentage(Increment*(loopsCompleted - loopsToWaitBeforeStarting) % 100);
            Data.SetStatusDerating(GetRandomBool());
            Data.SetStatusBatteryConnector(GetRandomBool());
            Data.SetStatusBridgeControl(GetRandomBool());
        }
    }
}