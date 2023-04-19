using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using EV_Car_UI.Models;
using PropertyChanged.SourceGenerator;
using ReactiveUI;

namespace EV_Car_UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IUpdateOnReceiveData
{
    // fields that store the data that is shown on UI

    // the [Notify] attribute auto generates code that will call RaisePropertyChanged whenever the field is set. 
    // This is important to do because this function is what upates the UI with new values.

    // Note that the source generator will generate the field as a public property that has the same name
    // but without the _ and with the first letter capital
     [Notify] private float _mainBatteryVoltageValue;
     [Notify] private float _batteryCurrentValue;
     [Notify] private float _carBatteryVoltageValue;
     [Notify] private float _motorTemperatureValue;
     [Notify] private float _inverterTemperatureValue;
     [Notify] private float _batteryTemperatureValue;
     [Notify] private float _wheelSpeedValue;
     [Notify] private float _motorSpeedValue;
     [Notify] private double _throttlePercentageValue;
     [Notify] private double _brakePercentageValue;
     [Notify] private bool _deratingValue;
     [Notify] private bool _batteryConnectorValue;
     [Notify] private bool _bridgeControlValue;
     [Notify] public double _barHeight = 480f;
    
    // The properties that are binded to the values in the UI.

    // We don't use the above declared fields in the UI directly becayse we want to format them.
    // such as add units and capping the decimal places
    public string MainBatteryVoltage => $"{MainBatteryVoltageValue:0.00} V";
    public string BatteryCurrent => $"{BatteryCurrentValue:0.00} A";
    public string CarBatteryVoltage => $"{CarBatteryVoltageValue:0.00} V";
    public string MotorTemperature => $"{MotorTemperatureValue:0.00} °C";
    public string InverterTemperature => $"{InverterTemperatureValue:0.00} °C";
    public string BatteryTemperature => $"{BatteryTemperatureValue:0.00} °C";
    public string WheelSpeed => $"{WheelSpeedValue:0.0}";
    public string MotorSpeed => $"{MotorSpeedValue:0.0}";
    public IBrush BatteryConnector => BatteryConnectorValue ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.WhiteSmoke);
    public IBrush BridgeControl => BridgeControlValue ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.WhiteSmoke);
    public IBrush Derating => DeratingValue ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.WhiteSmoke);
    public double ThrottleBarHeight => ThrottlePercentageValue / 100f * BarHeight;
    public double BrakeBarHeight => BrakePercentageValue / 100f * BarHeight;

    // a function for the source generator to find
    // this will cause the UI to update with new data
    private void RaisePropertyChanged(string name) => IReactiveObjectExtensions.RaisePropertyChanged(this, name);
    private void RaisePropertyChanging(string name) => IReactiveObjectExtensions.RaisePropertyChanging(this, name);


    private readonly CanBus _canBus;
    private readonly LoraCommunication? _loraCommunication;
    
    public MainWindowViewModel()
    {
        _canBus = new CanBus(this);

        // so our windows dev environment doesn't die
        if (AvaloniaLocator.Current.GetService<IRuntimePlatform>()!.GetRuntimeInfo().OperatingSystem ==
            OperatingSystemType.Linux)
        {
            _loraCommunication = new LoraCommunication(this);
        }
    }

    public void Update(TransmissionData data)
    {
        // update the data
        MainBatteryVoltageValue = data.mbV;
        BatteryCurrentValue = data.bC;
        CarBatteryVoltageValue = data.cbV;
        MotorTemperatureValue = data.motTem;
        InverterTemperatureValue = data.invTem;
        BatteryTemperatureValue = data.batTem;
        WheelSpeedValue = data.whelSped;
        MotorSpeedValue = data.motSpd;
        ThrottlePercentageValue = data.throt;
        BrakePercentageValue = data.brak;
        DeratingValue = data.derat;
        BatteryConnectorValue = data.batCon;
        BridgeControlValue = data.bridCon;
    }
}

public class ViewModelBase : ReactiveObject {}