using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LocalClientLibrary
{
    public class ResourceService
    {
        public static string GetStringFromResources(string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                // If we don't find the resource, throw an exception otherwise we will get a null exception when we try and load it into the StreamReader
                if (stream == null)
                {
                    string message = string.Format("Couldn't find the embedded resource '{0}'.  Is the Build Action set to Embedded Resource?", resourceName);
                    throw new FileNotFoundException(message);
                }

                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
