using Microsoft.AspNetCore.Mvc;
using System.Text;
using AppManagerServer.Models;

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
            msg.AppendLine("Application/Start -> Starts an application")
                .AppendLine("Application/Stop -> Stops an application")
                .AppendLine("Application/GetApps -> All available apps")
                .AppendLine("Application/Help -> Information about the available request");
            return msg.ToString();
        }
        // 
        // Post: /Application/Start/ 
        // https://localhost:7183/apps/start?appName=1&appName=2&appName=3&appName=4&appName=5
        public string Start(params string[] appName)
        {
            var msg = new StringBuilder("Response to start request:\n");
            if (appName.Length == 0)
                return "No application name given";
            
            foreach (var name in appName)
            {
                if (!_dataStore.Data.TryGetValue(name, out var value))
                {
                    msg.AppendLine($"Couldn't find {name} in data store");
                    continue;
                }
                
                _applicationsManager.Start(value.Path); 
                msg.AppendLine($"Starting {name}");
            }
            
            return msg.ToString();
        }
        // 
        // Post: /Application/Stop/ 
        public string Stop(params string[] appName)
        {
            var msg = new StringBuilder("Response to stop request:\n");
            if (appName.Length == 0)
                return "No application name given";
            foreach (var name in appName)
            {
                if (!_dataStore.Data.TryGetValue(name, out var value))
                {
                    msg.AppendLine($"Couldn't find {name} in data store");
                    continue;
                }

                _applicationsManager.Stop(value.Path);
                msg.AppendLine($"Stopping {name}");
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
            return "Help...";
        }
    }
}
