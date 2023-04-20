namespace EV_Car_UI.Models;

/// <summary>
/// Interface for being able to send data from outside sources
/// </summary>
public interface IDataSender
{
    /// <summary>
    /// The function that will be called to send data
    /// </summary>
    void SendData(TransmissionData data);
}