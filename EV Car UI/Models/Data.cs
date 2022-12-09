using System;
using System.Threading;
using System.Threading.Tasks;

namespace EV_Car_UI.Models;

// a class that stores and handles the data we get from the rest of the car
public static class Data
{
    //setters for all values in view model
    public static void SetMainBatteryVoltage(float value) => App.MainWindowViewModel.MainBatteryVoltageValue = value;
    public static void SetBatteryCurrent(float value) => App.MainWindowViewModel.BatteryCurrentValue = value;
    public static void SetCarBatteryVoltage(float value) => App.MainWindowViewModel.CarBatteryVoltageValue = value;
    public static void SetMotorTemperature(float value) => App.MainWindowViewModel.MotorTemperatureValue = value;
    public static void SetInverterTemperature(float value) => App.MainWindowViewModel.InverterTemperatureValue = value;
    public static void SetBatteryTemperature(float value) => App.MainWindowViewModel.BatteryTemperatureValue = value;
    public static void SetWheelSpeed(float value) => App.MainWindowViewModel.WheelSpeedValue = value;
    public static void SetMotorSpeed(float value) => App.MainWindowViewModel.MotorSpeedValue = value;
    public static void SetThrottlePercentage(double value) => App.MainWindowViewModel.ThrottlePercentageValue = value;
    public static void SetBrakePercentage(double value) => App.MainWindowViewModel.BrakePercentageValue = value;
    public static void SetStatusDerating(bool value) => App.MainWindowViewModel.DeratingValue = value;
    public static void SetStatusBatteryConnector(bool value) => App.MainWindowViewModel.BatteryConnectorValue = value;
    public static void SetStatusBridgeControl(bool value) => App.MainWindowViewModel.BridgeControlValue = value;
}