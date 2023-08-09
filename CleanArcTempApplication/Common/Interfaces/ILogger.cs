namespace CleanArcTemp.Application.Common.Interfaces;

public interface ILogger
{
    public void Verbose(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null);
    public void Debug(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null);
    public void Information(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null);
    public void Warning(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null);
    public void Error(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null);
    public void Fatal(string messageTemplate, List<KeyValuePair<object, object>>? parameters = null, Exception? exception = null);

}

