using System;
using System.Threading;
using System.Threading.Tasks;

namespace EV_Car_UI.Models;

//The class that will get the data from can bus
public static class CanBus
{
    //called when app is created do all canbus stuff here probably in a async awaited while loop (see randomdata class)
    public static void Start()
    {
        //passing the UI random data every 1/2 seconds to see some movement
        new RandomData().RepeatSendRandomData();
    }
    
    
}

//passes in random data, remove it when actual data comes
public class RandomData
{
    private double Inc = 5;
    private int count = 1;
    private int counter = 0;
    private float GetRandomFloat(float num) => num * 3 / 4f + counter/100f * num;
    private bool GetRandomBool() => new Random().NextDouble() > 0.5;
    
    public async Task RepeatSendRandomData()
    {
        var periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(500));
        while (await periodicTimer.WaitForNextTickAsync())
        {
            if (counter < 20)
            {
                counter++;
                continue;
            }

            counter++;
            Data.SetMainBatteryVoltage(GetRandomFloat(300));
            Data.SetBatteryCurrent(GetRandomFloat(300));
            Data.SetCarBatteryVoltage(GetRandomFloat(12));
            Data.SetMotorTemperature(GetRandomFloat(25));
            Data.SetInverterTemperature(GetRandomFloat(25));
            Data.SetBatteryTemperature(GetRandomFloat(25));
            Data.SetWheelSpeed(GetRandomFloat(60));
            Data.SetMotorSpeed(GetRandomFloat(5000));
            Data.SetThrottlePercentage(Inc*count++ % 100);
            Data.SetBrakePercentage((Inc/2)*count++ % 100);
            Data.SetStatusDerating(GetRandomBool());
            Data.SetStatusBatteryConnector(GetRandomBool());
            Data.SetStatusBridgeControl(GetRandomBool());
        }
    }
}