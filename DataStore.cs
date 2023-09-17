
namespace AppManagerServer;

public class DataStore
{
    private readonly Dictionary<string, App> _data;

    public Dictionary<string, App> Data => _data;
    public DataStore()
    {
        _data = new Dictionary<string, App>();
    }

    public void LoadApps(List<App> apps)
    {
        foreach (var app in apps)
        {
            _data.Add(app.Name,app);
        }
    }
}