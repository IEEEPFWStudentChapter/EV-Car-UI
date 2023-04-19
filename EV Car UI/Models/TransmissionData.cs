using System;

namespace EV_Car_UI.Models;

/// <summary>
/// A record to hold the data from the car that can be sent by LoRa
/// the names have been made smaller to reduce the size of the packet
/// </summary>
[Serializable]
public record TransmissionData(
    float mbV,           //main battery voltage
    float bC,            //battery current
    float cbV,           //car battery voltage
    float motTem,        //motor temperature
    float invTem,        //inverter temperature
    float batTem,        //battery temperature
    float whelSped,      //wheel speed
    float motSpd,        //motor speed 
    double throt,        //throttle
    double brak,         //brake
    bool derat,          //derating
    bool batCon,         //battery connector
    bool bridCon         //bridge control
    );