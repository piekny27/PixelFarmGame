using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UWP_project.Core.Player;
using UWP_project.Support;
using Windows.Storage;
using Newtonsoft.Json;


namespace UWP_project.Services
{
    public class JSON
    {
        private static readonly string FILE = "Database.json";
        public List<Player> Players { get; set; }
        private static JSON json = null;
        static object SingletonLock = new object();

        //singleton
        public static JSON Instance
        {
            get
            {
                lock (SingletonLock)
                {
                    if (json == null)
                    {
                        json = new JSON();
                    }
                    return json;
                }
            }
        }

        JSON()
        {
            Players = new List<Player>();
        }

        public async static Task<IList<string>> LoadJSON(object context, string name)
        {
            IList<string> json = null;
            StorageFile jsonFile;

            StorageFolder root = ApplicationData.Current.LocalFolder;
            name = name + ".json";

            if (await root.TryGetItemAsync(name) != null)
            {
                Log.info(context, "Loading " + name + " from " + root.Path.ToString() + " folder");
                jsonFile = await root.GetFileAsync(name);
                json = await FileIO.ReadLinesAsync(jsonFile);
            }
            else
            {
                Log.info(context, "File doesn't exist: " + name + " in " + root.Path.ToString() + " folder");
            }
            return json;
        }
        public async static Task SaveJSON(object context, IList<string> lines, string name)
        {
            StorageFile jsonFile;

            Log.info(context, "Start saving JSON");

            name = name + ".json";
            StorageFolder root = ApplicationData.Current.LocalFolder;

            if (await root.TryGetItemAsync(name) != null)
            {
                Log.info(context, "File " + name + " exist in " + root.Path.ToString() + " folder");
                jsonFile = await root.GetFileAsync(name);
                await FileIO.WriteLinesAsync(jsonFile, lines);
            }
            else
            {
                Log.info(context, "Create " + name + " in " + root.Path.ToString() + " folder");
                await root.CreateFileAsync(name);
                jsonFile = await root.GetFileAsync(name);
                await FileIO.WriteLinesAsync(jsonFile, lines);
            }
            Log.info(context, "Saving JSON finished");
        }
        public async Task Save()
        {
            StorageFile jsonFile;
            string s = JsonConvert.SerializeObject(this);

            Log.info(this, "Start saving JSON");

            StorageFolder root = ApplicationData.Current.LocalFolder;

            if (await root.TryGetItemAsync(FILE) != null)
            {
                Log.info(this, "File " + FILE + " exist in " + root.Path.ToString());
                jsonFile = await root.GetFileAsync(FILE);
                await FileIO.WriteTextAsync(jsonFile, s);
            }
            else
            {
                Log.info(this, "Creating " + FILE + " in " + root.Path.ToString());
                try
                {
                    await root.CreateFileAsync(FILE);
                }
                catch(Exception ex)
                {
                    Log.err(this, "Creating file exception: " + ex.Message);
                    return;
                }
                
                jsonFile = await root.GetFileAsync(FILE);
                await FileIO.WriteTextAsync(jsonFile, s);
            }
            Log.info(this, "Saving JSON finished");
        }
        public async Task Load()
        {
            string s = null;
            StorageFile jsonFile;
            StorageFolder root = ApplicationData.Current.LocalFolder;

            if (await root.TryGetItemAsync(FILE) != null)
            {
                Log.info(this, "Loading " + FILE + " from " + root.Path.ToString() + " folder");
                jsonFile = await root.GetFileAsync(FILE);
                s = await FileIO.ReadTextAsync(jsonFile);
                json = JsonConvert.DeserializeObject<JSON>(s);
            }
            else
            {
                Log.info(this, "File doesn't exist: " + FILE + " in " + root.Path.ToString() + " folder");
            }
        }
    }
}
