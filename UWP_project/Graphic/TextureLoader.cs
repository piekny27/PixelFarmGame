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
    public sealed class TextureLoader
    {
        //backgrounds
        public static string[] BG_GRASS = { "Background/grass.png" };

        //crops
        public static string[] CROP_CAULIFLOWER_1 = { "Crops/cauliflower/cauliflower_1.png" };
        public static string[] CROP_CAULIFLOWER_2 = { "Crops/cauliflower/cauliflower_2.png" };
        public static string[] CROP_CAULIFLOWER_3 = { "Crops/cauliflower/cauliflower_3.png" };
        public static string[] CROP_CAULIFLOWER_4 = { "Crops/cauliflower/cauliflower_4.png" };
        public static string[] CROP_CAULIFLOWER_5 = { "Crops/cauliflower/cauliflower_5.png" };
        public static string[] CROP_CAULIFLOWER_6 = { "Crops/cauliflower/cauliflower_6.png" };

        public static string[] CROP_CARROT_1 = { "Crops/carrot/carrot_1.png" };
        public static string[] CROP_CARROT_2 = { "Crops/carrot/carrot_2.png" };
        public static string[] CROP_CARROT_3 = { "Crops/carrot/carrot_3.png" };
        public static string[] CROP_CARROT_4 = { "Crops/carrot/carrot_4.png" };
        public static string[] CROP_CARROT_5 = { "Crops/carrot/carrot_5.png" };
        public static string[] CROP_CARROT_6 = { "Crops/carrot/carrot_6.png" };
       
        IDictionary<string[], CanvasBitmap[]> bitmapDictionary = new Dictionary<string[], CanvasBitmap[]>();
        private static TextureLoader textures = null;
        static object SingletonLock = new object();

        public delegate void IncreaseLoadedPercentageDelegate(float percent);
        public delegate void OnCreateResourcesAsyncFinished();

        //singleton
        public static TextureLoader Instance
        {
            get
            {
                lock (SingletonLock)
                {
                    if (textures == null)
                    {
                        textures = new TextureLoader();
                    }
                    return textures;
                }
            }
        }

        public async Task CreateResourcesAsync(CanvasAnimatedControl sender, IncreaseLoadedPercentageDelegate increaseLoadedPercentage, OnCreateResourcesAsyncFinished onFinished, string[][] textureSets)
        {
            foreach (string[] textureSet in textureSets)
            {
                await LoadBitmap(sender, textureSet);

                if (increaseLoadedPercentage != null)
                {
                    increaseLoadedPercentage((float)100 / textureSets.Length);
                }
            }

            if (onFinished != null)
            {
                onFinished();
            }
        }

        private async Task LoadBitmap(CanvasAnimatedControl sender, string[] bitmap)
        {
            int length = bitmap.Length;
            CanvasBitmap[] bitmaps = new CanvasBitmap[length];
            for (int i = 0; i < length; i++)
            {
                Log.info(this,"Texture loading: " + bitmap[i]);
                try
                {
                    bitmaps[i] = await CanvasBitmap.LoadAsync(sender, "Assets/" + bitmap[i]);
                }
                catch (System.IO.FileNotFoundException)
                { 
                    Log.err(this, "Texture loading error: " + bitmap[i]);
                }
            }
            bitmapDictionary.Add(bitmap, bitmaps);
        }

        
        public CanvasBitmap[] this[string[] textureSet]
        {
            get
            {
                return bitmapDictionary[textureSet];
            }
        }

        public static void DeleteInstance()
        {
            textures = null;
        }
    }
}
