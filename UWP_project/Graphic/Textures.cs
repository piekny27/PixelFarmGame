using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using UWP_project.Support;

namespace UWP_project.Graphic
{
    internal class Textures
    {
        //backgrounds
        public static string[] BG_GRASS = { "Background/background" };

        //crops
        public static string[] CROP_CAULIFLOWER_1 = { "Crops/cauliflower/cauliflower_1" };
        public static string[] CROP_CAULIFLOWER_2 = { "Crops/cauliflower/cauliflower_2" };
        public static string[] CROP_CAULIFLOWER_3 = { "Crops/cauliflower/cauliflower_3" };
        public static string[] CROP_CAULIFLOWER_4 = { "Crops/cauliflower/cauliflower_4" };
        public static string[] CROP_CAULIFLOWER_5 = { "Crops/cauliflower/cauliflower_5" };
        public static string[] CROP_CAULIFLOWER_6 = { "Crops/cauliflower/cauliflower_6" };

        public static string[] CROP_CARROT_1 = { "Crops/carrot/carrot_1" };
        public static string[] CROP_CARROT_2 = { "Crops/carrot/carrot_2" };
        public static string[] CROP_CARROT_3 = { "Crops/carrot/carrot_3" };
        public static string[] CROP_CARROT_4 = { "Crops/carrot/carrot_4" };
        public static string[] CROP_CARROT_5 = { "Crops/carrot/carrot_5" };
        public static string[] CROP_CARROT_6 = { "Crops/carrot/carrot_6" };


        public delegate void IncreaseLoadedPercentageDelegate(float percent);
        public delegate void OnCreateResourcesAsyncFinished();

        //singleton
        private static Textures textures = null;
        static object SingletonLock = new object();
        public static Textures Instance
        {
            get
            {
                lock (SingletonLock)
                {
                    if (textures == null)
                    {
                        textures = new Textures();
                    }
                    return textures;
                }
            }
        }

        public async Task CreateResourcesAsync(CanvasAnimatedControl sender, IncreaseLoadedPercentageDelegate increaseLoadedPercentage, OnCreateResourcesAsyncFinished onFinished)
        {
            string[][] texturesLoad =
            {
                //backgrounds
                BG_GRASS,

                //crops
                CROP_CAULIFLOWER_1,CROP_CAULIFLOWER_2,CROP_CAULIFLOWER_3,
                CROP_CAULIFLOWER_4,CROP_CAULIFLOWER_5,CROP_CAULIFLOWER_6,
                CROP_CARROT_1,CROP_CARROT_2,CROP_CARROT_3,
                CROP_CARROT_4,CROP_CARROT_5,CROP_CARROT_6
            };

            await CreateResourcesAsync(sender, increaseLoadedPercentage, onFinished);
        }

        public async Task CreateResourcesAsync(CanvasAnimatedControl sender,IncreaseLoadedPercentageDelegate increaseLoadedPercentage, OnCreateResourcesAsyncFinished onFinished, string[][] texturesLoad)
        {
            foreach (string[] textures in texturesLoad)
            {
                await LoadBitmap(sender, textures);

                if(increaseLoadedPercentage != null)
                {
                    increaseLoadedPercentage((float)100 / textures.Length);
                }
            }

            if (onFinished != null)
            {
                onFinished();
            }
        }

        private async Task LoadBitmap(CanvasAnimatedControl sender, string[] textures)
        {
            int length = textures.Length;
            CanvasBitmap[] bitmaps = new CanvasBitmap[length];
            for (int i = 0; i < length; i++)
            {
                Log.info(this,"Ładowanie textury: " + textures[i]);
                try
                {
                    bitmaps[i] = await CanvasBitmap.LoadAsync(sender, "Assets/" + textures[i]);
                }
                catch (System.IO.FileNotFoundException)
                { 
                    Log.err(this, "Błąd przy ładowaniu textury: " + textures[i]);
                }
            }

        }

        public static void DeleteInstance()
        {
            textures = null;
        }
    }
}
