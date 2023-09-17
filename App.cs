namespace AppManagerServer;

public class App
{
    public string Name { get; private set; }
    public  string Path { get; private set; }

    public App(string name, string path)
    {
        Name = name;
        Path = path;
    }
}