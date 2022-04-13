using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;

namespace UWP_project.Support
{
    public static class Utility
    {
        private static Random Random = new Random();
        public static ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;
        public static StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;

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

        public static int RandomBetween(int a, int b)
        {
            if (a <= b)
            {
                return Random.Next(a, b + 1);
            }
            else
            {
                return Random.Next(b, a + 1);
            }
        }

        private async static Task<MediaPlayer> GetMusic(string additionalPath, string name)
        {
            //Initializing Audio
            StorageFolder folder;
            StorageFile musicFile;
            string path = @"Assets\Music";
            if (additionalPath != null)
            {
                path += @"\" + additionalPath;
            }
            folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(path);
            musicFile = await folder.GetFileAsync(name + ".mp3");
            MediaPlayer music = new MediaPlayer();
            music.Source = MediaSource.CreateFromStorageFile(musicFile);
            
            return music;
        }
        public async static Task<MediaPlayer> GetMusic(string mp3)
        {
            return await GetMusic(null, mp3);
        }

        public async static void ExitGame(object context)
        {
            Log.info(context, "Exiting Game");
            await MetroLog.LazyFlushManager.FlushAllAsync(new MetroLog.LogWriteContext());
            Windows.UI.Xaml.Application.Current.Exit();
        }

    }
}
