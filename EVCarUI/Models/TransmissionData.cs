using System;

namespace EV_Car_UI.Models;

/// <summary>
/// A record to hold the data from the car that can be sent by LoRa
/// the names have been made smaller to reduce the size of the packet
/// </summary>
[Serializable]  
public record TransmissionData
{
    public float mainBatteryVoltage {get; init;} // 2 bytes
    public float batteryCurrent  {get; init;}     // 2 bytes
    public float carBatteryVoltage {get; init;}  // 1 byte
    public float motorTemperature  {get; init;}    // 1 byte
    public float inverterTemperature  {get; init;} // 1 byte
    public float batteryTemperature  {get; init;}  // 1 byte (From thermocouple)
    public float wheelSpeed  {get; init;}          // 1 byte (From transmission)
    public float motorSpeed  {get; init;}       // 2 byte
    public float throttlePercentage  {get; init;}  // 1 byte
    public float brakePercentage  {get; init;}     // 1 byte
    public bool  derating  {get; init;}
    public bool  batteryConnector  {get; init;}
    public bool  bridgeControl  {get; init;}

    public TransmissionData( float mainBatteryVoltage,  // 2 bytes
    float batteryCurrent,      // 2 bytes
    float carBatteryVoltage,   // 1 byte
    float motorTemperature,    // 1 byte
    float inverterTemperature, // 1 byte
    float batteryTemperature,  // 1 byte (From thermocouple)
    float wheelSpeed,          // 1 byte (From transmission)
    float motorSpeed,          // 2 byte
    float throttlePercentage,  // 1 byte
    float brakePercentage,     // 1 byte
    bool  derating,
    bool  batteryConnector,
    bool  bridgeControl)
    {
        this.batteryCurrent = batteryCurrent;
        this.carBatteryVoltage = carBatteryVoltage;
        this.motorTemperature = motorTemperature;
        this.inverterTemperature = inverterTemperature;
        this.batteryTemperature = batteryTemperature;
        this.wheelSpeed = wheelSpeed;
        this.motorSpeed = motorSpeed;
        this.throttlePercentage = throttlePercentage;
        this.brakePercentage = brakePercentage;
        this.derating = derating;
        this.batteryConnector = batteryConnector;
        this.bridgeControl = bridgeControl;
    }

    public TransmissionData(){}
}