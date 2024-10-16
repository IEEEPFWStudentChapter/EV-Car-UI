using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using Avalonia.Controls.ApplicationLifetimes;
using JetBrains.Annotations;

namespace EV_Car_UI;

[UsedImplicitly]
internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            SetupErrorLogging();
        }
        catch (Exception e)
        {
            // ignored. App still works without logging
        }

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static void SetupErrorLogging()
    {
        var file = Path.Combine(Directory.GetCurrentDirectory(), "ErrorLogs.log");
        if (File.Exists(file))
        {
            try
            {
                File.Move(file, file + ".previous");
            }
            catch (Exception e)
            {
                // accept just clearing the file
                File.WriteAllText(file, string.Empty);
            }
        }
        var fileListener = new TextWriterTraceListener(file);
        fileListener.TraceOutputOptions = TraceOptions.DateTime;
        Trace.AutoFlush = true;
        Trace.Listeners.Add(fileListener);
        
        AppDomain.CurrentDomain.UnhandledException += (_, e) => Trace.WriteLine(e.ToString());
        TaskScheduler.UnobservedTaskException += (_, e) => Trace.WriteLine(e.ToString());
        
        Trace.WriteLine($"Opening App at {DateTime.Now}"); 
    }

    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
}