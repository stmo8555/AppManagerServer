using Microsoft.AspNetCore.Mvc;
using System.Text;
using AppManagerServer.Models;
using Microsoft.VisualBasic;

namespace AppManagerServer.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly ApplicationsManager _applicationsManager;
        private readonly DataStore _dataStore;
        public ApplicationController(DataStore dataStore)
        {
            _dataStore = dataStore;
            _applicationsManager = new ApplicationsManager();
        }
        // 
        // GET: /Application/
        public string Index()
        {
            var msg = new StringBuilder("Endpoint request that the web server provide:").AppendLine();
            msg.AppendLine("Application/Start -> Start application/s")
                .AppendLine("Application/Stop -> Stop application/s")
                .AppendLine("Application/GetApps -> All available applications")
                .AppendLine("Application/GetStatus -> Status for one or all applications")
                .AppendLine("Application/Help -> Information about the available request");
            return msg.ToString();
        }
        // 
        // Post: /Application/Start/ 
        public string Start(params string[] appName)
        {
            var msg = new StringBuilder();
            if (appName.Length == 0)
                return "No application name given";

            foreach (var name in appName)
            {
                if (!_dataStore.Data.TryGetValue(name, out var value))
                {
                    msg.AppendLine($"Couldn't find {name} in data store");
                    continue;
                }

                if (_applicationsManager.Start(value.ExePath))
                    msg.AppendLine($"Starting {name}");
                else
                    msg.AppendLine($"Failed to start {name}");
            }

            return msg.ToString();
        }
        // 
        // Post: /Application/Stop/ 
        public string Stop(params string[] appName)
        {
            var msg = new StringBuilder();
            if (appName.Length == 0)
                return "No application name given";
            foreach (var name in appName)
            {
                if (!_dataStore.Data.TryGetValue(name, out var value))
                {
                    msg.AppendLine($"Couldn't find {name} in data store");
                    continue;
                }

                if (_applicationsManager.Stop(value.trueName))
                    msg.AppendLine($"Stopping {name}");
                else
                    msg.AppendLine($"Failed to stop {name}");
            }

            return msg.ToString();
        }
        // 
        // GET: /Application/GetApps/ 
        public string GetApps()
        {
            return _applicationsManager.GetApps(_dataStore.Data);
        }
        // 
        // GET: /Application/GetStatus/ 
        public string GetStatus(string appName)
        {
            return _applicationsManager.GetStatus(_dataStore.Data, appName);
        }

        // 
        // GET: /Application/Help/ 
        public string Help()
        {
            var msg = new StringBuilder();
            
            msg.AppendLine(
                    "Start: Start one or more processes. Parameters: one app name or multiple. The app needs to be in the data store to be able to do this operation")
                .AppendLine(
                    "Example requests: http://localhost:1234/application/start?appName=1&appName=2&appName=3&appName=4&appName=5 or http://localhost:1234/application/start?appName=1")
                .AppendLine();

            msg.AppendLine(
                    "Stop: Stops one or more processes. Parameters: one app name or multiple. The app needs to be in the data store to be able to do this operation")
                .AppendLine(
                    "Example requests: http://localhost:1234/application/stop?appName=1&appName=2&appName=3&appName=4&appName=5 or http://localhost:1234/application/stop?appName=1")
                .AppendLine();

            msg.AppendLine(
                    "GetApps: returns all apps in data store. Parameters: None.")
                .AppendLine();

            msg.AppendLine(
                    "GetStatus: Get the status if process is started or not. If no argument is given the request will give status for all apps in data store.")
                .AppendLine(
                    "Parameters: one app name or none. The app needs to be in the data store to be able to do this operation")
                .AppendLine(
                    "Example requests: http://localhost:1234/application/GetStatus?appName=1 or http://localhost:1234/application/GetStatus");

            return msg.ToString();
        }
    }
}
