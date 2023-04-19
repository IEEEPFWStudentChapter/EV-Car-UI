namespace EV_Car_UI.Models;

/// <summary>
/// Interface specifying that it is able to receive data from outside sources
/// </summary>
public class DataReceiver
{
    /// <summary>
    /// The class that will be updated when new data is received
    /// </summary>
    protected readonly IUpdateOnReceiveData _toUpdate;
    
    /// <summary>
    /// The IUpdateOnReceiveData will be used to update the app when new data comes
    /// by calling toUpdate.Update(data); 
    /// </summary>
    public DataReceiver(IUpdateOnReceiveData toUpdate)
    {
        _toUpdate = toUpdate;
    }
    
    /// <summary>
    /// A function to be called when we data is received
    /// </summary>
    protected void OnNewDataReceived(TransmissionData data)
    {
        _toUpdate.Update(data);
    }
}