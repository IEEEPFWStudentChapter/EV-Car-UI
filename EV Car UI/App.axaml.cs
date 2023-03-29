using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using EV_Car_UI.Models;
using EV_Car_UI.ViewModels;
using EV_Car_UI.Views;

namespace EV_Car_UI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        // start our long running code that will receive data and update the UI with it
        CanBus.Start();
    }
    
    private static MainWindow MainWindow = new ()
    {
        DataContext = new MainWindowViewModel(),
    };
        
    public static MainWindowViewModel MainWindowViewModel => (MainWindow.DataContext as MainWindowViewModel)!;

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);
            desktop.MainWindow = MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}