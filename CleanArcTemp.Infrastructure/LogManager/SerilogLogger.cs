using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;
using ILogger = CleanArcTemp.Application.Common.Interfaces.ILogger;

namespace CleanArcTemp.Infrastructure.LogManager;

public class SerilogLogger : ILogger
{

    private readonly object _lock = new object();

    private readonly bool _hasValue = false;

    public SerilogLogger()
    {

        if (_hasValue is false)
        {
            lock (_lock)
            {
                if (_hasValue is false)
                {
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("LogSetting.json")
                        .Build();


                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();

                    _hasValue = true;
                }
            }
        }
    }

    public void Debug(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null)
    {
        SendLog("Debug", messageTemplate, exception, parameters);
    }

    public void Error(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null)
    {
        SendLog("Error", messageTemplate, exception, parameters);
    }

    public void Fatal(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null)
    {
        SendLog("Fatal", messageTemplate, exception, parameters);
    }

    public void Information(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null)
    {
        SendLog("Information", messageTemplate, exception, parameters);
    }

    public void Verbose(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null)
    {
        SendLog("Verbose", messageTemplate, exception, parameters);
    }

    public void Warning(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null)
    {
        SendLog("Warning", messageTemplate, exception, parameters);
    }


    private void SendLog(string level, string messageTemplate, Exception? exception,
        List<KeyValuePair<object, object>>? parameters)
    {

        LogContext.Reset();

        if (exception != null)
        {
            var stackTrace = new StackTrace(exception);

            for (int i = 0; i < stackTrace.GetFrames().Length; i++)
            {

                LogContext.PushProperty(

                    $"starck trace error list index-{i}",
                    $"the error has occurred in file line " +
                    $"number {stackTrace.GetFrames()[i].GetFileLineNumber()}" +
                    $"in file name {stackTrace.GetFrames()[i].GetFileName()}" +
                    $"in method name {stackTrace.GetFrames()[i].GetMethod()}"

                );

            }

        }

        if (parameters != null)
        {
            foreach (var para in parameters)
            {

                LogContext.PushProperty(
                    para.Key.ToString()!,
                    para.Value
                );

            }
        }


        switch (level)
        {
            case "Debug":
                Log.Debug(messageTemplate, exception);
                break;
            case "Error":
                Log.Error(messageTemplate, exception);
                break;
            case "Fatal":
                Log.Fatal(messageTemplate, exception);
                break;
            case "Information":
                Log.Information(messageTemplate, exception);
                break;
            case "Verbose":
                Log.Verbose(messageTemplate, exception);
                break;
            case "Warning":
                Log.Warning(messageTemplate, exception);
                break;

        }
    }

}

