namespace EV_Car_UI.Models;

/// <summary>
/// Interface for registering what needs to be done when data is received from outside sources
/// </summary>
public interface IUpdateOnReceiveData
{
    void Update(TransmissionData data);
}