using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Threading;
using EV_Car_UI.ViewModels;

namespace EV_Car_UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        LayoutUpdated += (_, _) => Dispatcher.UIThread.InvokeAsync(Scale);
    }

    private static double MainGridWidth = 0;

    private static Dictionary<string, double> IdealWidths = new Dictionary<string, double>()
    {
        { nameof(RawDataGrid), 300 },
        { nameof(SpeedStack), 500 },
        { nameof(ThrottleAndBrake), 600 },
        { nameof(Flags), 400 },
    };
    private static Dictionary<string, double> IdealHeights = new Dictionary<string, double>()
    {
        { nameof(SpeedStack), 1080 },
        { nameof(ThrottleAndBrake), 730 },
        { nameof(Flags), 1080 },
    };

    private static Dictionary<string, double> DefaultSizes = new Dictionary<string, double>()
    {
        { nameof(RawDataGrid) + nameof(FontSize), 40 },
        { nameof(SpeedStack) + nameof(WheelSpeed), 180 },
        { nameof(SpeedStack) + nameof(WheelSpeedUnits), 80 },
        { nameof(SpeedStack) + nameof(MotorSpeed), 100 },
        { nameof(SpeedStack) + nameof(MotorSpeedUnits), 50 },
        { nameof(SpeedStack) + nameof(Padding), 200 },
        { nameof(ThrottleAndBrake) + nameof(Height), 480 },
        { nameof(ThrottleAndBrake) + nameof(FontSize), 60 },
        { nameof(ThrottleAndBrake) + nameof(BorderThickness), 5 },
        { nameof(Flags) + nameof(FontSize), 40 },
        { nameof(Flags) + nameof(BorderThickness), 5 },
        { nameof(Flags) + nameof(Height), 120 },
        { nameof(Flags) + nameof(Width), 250 },
        { nameof(Flags) + nameof(Padding), 150 },
        
    };

    private void Scale()
    {
        if (MainGrid.Bounds.Width is 0 or double.NaN) return;
        
        // don't needlessly run this
        if (Math.Abs(MainGridWidth - MainGrid.Bounds.Width) < 0.01) return;

        MainGridWidth = MainGrid.Bounds.Width;

        foreach (var child in RawDataGrid.Children)
        {
            var widthScaleFactor = RawDataGrid.Bounds.Width / IdealWidths[nameof(RawDataGrid)];
            if (child is Label label)
            {
                label.FontSize = widthScaleFactor * DefaultSizes[nameof(RawDataGrid) + nameof(FontSize)];
            }
        }

        foreach (var child in SpeedStack.Children)
        {
            var widthScaleFactor = SpeedStack.Bounds.Width / IdealWidths[nameof(SpeedStack)];
            var heightScaleFactor = SpeedStack.Bounds.Height / IdealHeights[nameof(SpeedStack)];
            if (child is Label label)
            {
                label.FontSize =  widthScaleFactor * DefaultSizes[nameof(SpeedStack) + label.Name];
                if (!label.Name.Contains("Units"))
                {
                    label.Padding = new Thickness(0,
                        heightScaleFactor * DefaultSizes[nameof(SpeedStack) + nameof(Padding)],
                        0,
                        0);
                }
            }
        }

        foreach (var child in ThrottleAndBrake.Children)
        {
            var widthScaleFactor = ThrottleAndBrake.Bounds.Width / IdealWidths[nameof(ThrottleAndBrake)];
            if (child is Label label)
            {
                label.FontSize =  widthScaleFactor * DefaultSizes[nameof(ThrottleAndBrake) + nameof(FontSize)];
            }
            if (child is Border border)
            {
                border.BorderThickness = new Thickness(widthScaleFactor * DefaultSizes[nameof(ThrottleAndBrake) + nameof(BorderThickness)]);
            }
        }
        
        (DataContext as MainWindowViewModel)!.BarHeight = ThrottleAndBrake.Bounds.Height / IdealHeights[nameof(ThrottleAndBrake)] *
                                                          DefaultSizes[nameof(ThrottleAndBrake) + nameof(Height)];
        
        foreach (var child in Flags.Children)
        {
            var widthScaleFactor = Flags.Bounds.Width / IdealWidths[nameof(Flags)];
            var heightScaleFactor = Flags.Bounds.Height / IdealHeights[nameof(Flags)];
            if (child is Label label)
            {
                label.FontSize =  widthScaleFactor * DefaultSizes[nameof(Flags) + nameof(FontSize)];
                
                foreach (var labelClass in label.Classes)
                {
                    Debug.WriteLine(labelClass);
                    if (!labelClass.Contains("Top"))
                    {
                        label.Padding = new Thickness(0, 
                            heightScaleFactor * DefaultSizes[nameof(Flags) + nameof(Padding)], 
                            0,
                            0);
                    }
                }
            }
            
            if (child is Border border)
            {
                border.BorderThickness = new Thickness(widthScaleFactor * DefaultSizes[nameof(Flags) + nameof(BorderThickness)]);
                border.Height = heightScaleFactor * DefaultSizes[nameof(Flags) + nameof(Height)];
                border.Width = widthScaleFactor * DefaultSizes[nameof(Flags) + nameof(Width)];
            }
        }
    }
}