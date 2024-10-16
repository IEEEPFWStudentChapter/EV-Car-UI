namespace EV_Car_UI.Models;

/// <summary>
/// Interface for registering what needs to be done when data is received from outside sources
/// </summary>
public interface IUpdateOnReceiveData
{
    /// <summary>
    /// The function that will be called when new data is received and updates the data in the
    /// class with new data
    /// </summary>
    void Update(TransmissionData data);
}