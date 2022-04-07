using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_project.Support
{
    public static class Utility
    {

        public static Windows.Storage.ApplicationDataContainer LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public static Windows.Storage.StorageFolder LocalFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public static void SaveSettings<T>(string name, T value)
        {
            LocalSettings.Values[name] = value;
        }

        public static T LoadSettings<T>(string name)
        {
            object value = LocalSettings.Values[name];
            if(value is T)
            {
                return (T)value;
            }else
            { 
                return default(T);
            }
        }
    }
}
