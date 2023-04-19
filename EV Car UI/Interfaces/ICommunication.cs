﻿namespace EV_Car_UI.Models;

/// <summary>
/// Interface for being able to send data from outside sources
/// </summary>
public interface IDataSender
{
    void SendData(TransmissionData data);
}