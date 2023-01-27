﻿using System;
using System.Threading;
using System.Threading.Tasks;
using EV_Car_UI.Models;
using PropertyChanged.SourceGenerator;
using ReactiveUI;

namespace EV_Car_UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
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
    
    //properties that are binded to the ui display
    public string MainBatteryVoltage => $"{MainBatteryVoltageValue:0.00} V";
    public string BatteryCurrent => $"{BatteryCurrentValue:0.00} A";
    public string CarBatteryVoltage => $"{CarBatteryVoltageValue:0.00} V";
    public string MotorTemperature => $"{MotorTemperatureValue:0.00} °C";
    public string InverterTemperature => $"{InverterTemperatureValue:0.00} °C";
    public string BatteryTemperature => $"{BatteryTemperatureValue:0.00} °C";
    public string WheelSpeed => $"{WheelSpeedValue:0.00}";
    public string MotorSpeed => $"{MotorSpeedValue:0.00}";
    public float BatteryConnector => BatteryConnectorValue ? 100 : 0;
    public float BridgeControl => BridgeControlValue ? 100 : 0;
    public float Derating => DeratingValue ? 100 : 0;

    public double ThrottleBarHeight => ThrottlePercentageValue / 100f * 480f;
    public double BrakeBarHeight => BrakePercentageValue / 100f * 480f;

    //for the source generator to find it
    private void RaisePropertyChanged(string name) => IReactiveObjectExtensions.RaisePropertyChanged(this, name);

    public MainWindowViewModel()
    {
        CanBus.Start();
    }
    
}
