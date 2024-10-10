// See https://aka.ms/new-console-template for more information
using SocketCANSharp;
using SocketCANSharp.Network;


Console.WriteLine("All Can interfaces.");
CanNetworkInterface.GetAllInterfaces(true).ToList().ForEach(x=>Console.WriteLine("Hey: "+x.ToString()));

CanNetworkInterface can0 = CanNetworkInterface.GetAllInterfaces(true).First();
Console.WriteLine("Interface being used: "+can0.ToString());

using(RawCanSocket socket = new())
{
    
    socket.Bind(can0);
    while(true)
    {
        CanFrame frame = new CanFrame();
        socket.Read(out frame);
        Console.WriteLine(frame);
    }
}


