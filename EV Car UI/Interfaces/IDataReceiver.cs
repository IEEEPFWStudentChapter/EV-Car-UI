namespace EV_Car_UI.Models;

/// <summary>
/// Interface specifying that it is able to receive data from outside sources
/// </summary>
public interface IDataReceiver
{
    /// <summary>
    /// The interface that will be updated when new data is received
    /// </summary>
    protected IUpdateOnReceiveData _toUpdate { get; init; }

    /// <summary>
    /// A function to be called when we data is received. Normally will just call _toUpdate.Update(data)
    /// but can do other things such as close connections or give a reply if needed
    /// </summary>
    protected void OnNewDataReceived(TransmissionData data);
    
}