using System;

namespace EV_Car_UI.Models;

/// <summary>
/// A record to hold the data from the car that can be sent by LoRa
/// the names have been made smaller to reduce the size of the packet
/// </summary>
[Serializable]
public record TransmissionData(
    float mainBatteryVoltage,
    float batteryCurrent,
    float carBatteryVoltage,
    float motorTemperature,
    float inverterTemperature,
    float batteryTemperature,  // 
    float wheelSpeed,          // calculated
    float motorSpeed,          // 14 bits
    float throttlePercentage,  // 7 bits
    float brakePercentage,     // 7 bits
    bool  derating,
    bool  batteryConnector,
    bool  bridgeControl
    );