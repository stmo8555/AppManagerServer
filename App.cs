namespace AppManagerServer;

public class App
{
    public string Key { get; private set; }
    public string trueName { get; private set; }
    public  string ExePath { get; private set; }

    public App(string key, string exePath)
    {
        Key = key;
        trueName = Path.GetFileNameWithoutExtension(exePath);
        ExePath = exePath;
    }
}