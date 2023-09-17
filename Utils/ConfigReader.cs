using System.Runtime.CompilerServices;
using System.Xml;

namespace AppManagerServer.Utils
{
    public class ConfigReader
    {
        private List<App> _appFromConf;

        public List<App> Apps => _appFromConf;

        public ConfigReader()
        {
            _appFromConf = new List<App>();
            ReadConf();
        }

        private void ReadConf()
        {

                var doc = new XmlDocument();
                doc.Load("App.config");

                var appSettingsNode = doc.SelectSingleNode("/configuration/appSettings");

                if (appSettingsNode != null)
                {
                    // Iterate through the <add> elements
                    foreach (XmlNode addNode in appSettingsNode.ChildNodes)
                    {
                        if (addNode.Attributes == null) continue;
                        // Get the key and value attributes
                        var keyAttribute = addNode.Attributes["key"];
                        var valueAttribute = addNode.Attributes["value"];

                        if (keyAttribute != null && valueAttribute != null)
                        {
                            _appFromConf.Add(new App(keyAttribute.Value, valueAttribute.Value));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No appSettings found in the configuration.");
                }
        }
    }
}
