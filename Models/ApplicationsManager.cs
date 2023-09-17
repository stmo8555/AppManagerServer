using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppManagerServer.Models
{
    public class ApplicationsManager
    {
        public bool Start(string exePath)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    CreateNoWindow = false, // Show a new window
                    UseShellExecute = true  // Use the system shell to execute the program
                };

                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        public bool Stop(string exePath)
        {
            try
            {
                var processList = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(exePath));
                foreach (var process in processList)
                {
                    process.Kill(true);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public string GetApps(Dictionary<string, App> apps)
        {
            var appNames = apps.Select(app => app.Key).ToList();
            return JsonConvert.SerializeObject(appNames);
        }

        public string GetStatus(Dictionary<string, App> apps, string appName = "")
        {
            var status = new Dictionary<string, string>();

            foreach (var app in apps)
            {
                var name = Path.GetFileNameWithoutExtension(app.Value.Path);
                var processes = Process.GetProcessesByName(name);
                status.Add(app.Value.Name, processes.Length > 0 ? "Started" : "Stopped");
            }

            if (string.IsNullOrWhiteSpace(appName)) 
                return JsonConvert.SerializeObject(status);
            
            if (!status.TryGetValue(appName, out var value))
                return $"Couldn't find {appName} in data store";
            
            return JsonConvert.SerializeObject(new Dictionary<string, string> { { appName, value } });

        }
    }
}
