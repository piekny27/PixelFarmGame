using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.UI;
using System.Threading.Tasks;
using UWP_project.Graphic;
using UWP_project.Support;
using Windows.Media.Playback;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_project.Screen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private MediaPlayer Music;
        private bool fieldLoaded = false;

        public GamePage()
        {
            Log.info(this, "Constructor initializing");
            this.InitializeComponent();

            FieldLoaded = false;

            Log.info(this, "Constructor initialized");
        }

        public bool FieldLoaded 
        { 
            get 
            { 
                return fieldLoaded; 
            } 
            private set 
            { 
                fieldLoaded = value; 
                if (fieldLoaded)
                {
                    GamePageLoadingGrid.Visibility = Visibility.Collapsed;
                    GamePageGrid.Visibility = Visibility.Visible;
                    Log.info(this, "Set FieldLoad to true");
                }
                else
                {
                    GamePageLoadingGrid.Visibility = Visibility.Visible;
                    GamePageGrid.Visibility = Visibility.Collapsed;
                    Log.info(this, "Set FieldLoad to false");
                }
            } 
        }
        private void GamePage1Button_Click(object sender, RoutedEventArgs e)
        {
            Log.info(this, "User clicked on main menu button");
            if (!FieldLoaded) return;

            Frame.Navigate(typeof(MainMenu));
        }

        private void GamePageGrid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {

        }

        private void GamePageGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        private void GamePageGrid_PointerReleased(object sender, PointerRoutedEventArgs e)
        {

        }

        private void canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            Log.info(this, "CreateResources started");

            TextureLoader.DeleteInstance(); //reset textures

            Log.info(this, "CreateResources starting parallel task");
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());

            Log.info(this, "CreateResources finished");
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            
        }

        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {

            string[][] textures = {TextureLoader.CROP_CARROT_1, TextureLoader.CROP_CARROT_2, TextureLoader.CROP_CARROT_3,
            TextureLoader.CROP_CARROT_4,TextureLoader.CROP_CARROT_5,TextureLoader.CROP_CARROT_6,TextureLoader.CROP_CAULIFLOWER_1,
            TextureLoader.CROP_CAULIFLOWER_2,TextureLoader.CROP_CAULIFLOWER_3,TextureLoader.CROP_CAULIFLOWER_4,
            TextureLoader.CROP_CAULIFLOWER_5,TextureLoader.CROP_CAULIFLOWER_6};

            await TextureLoader.Instance.CreateResourcesAsync(
                sender,
                (increasePercentage) => { loadingProgressBar.Value += increasePercentage; },
                null,
                textures);

            Log.info(this, "Loading music");
            double volume = Utility.LoadSettings<double>("resultVolume");

            Music = await Utility.GetMusic("Farming - Don't Starve Together - 8-Bit Cover");
            Music.Volume = volume;
            Music.IsLoopingEnabled = true;
            Music.Play();

            Log.info(this, "Set FieldLoaded as true");
            FieldLoaded = true;

            Log.info(this, "CreateResourcesAsync finished");

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Log.info(this, "Page is being unloaded - removing associations");
            Music.Dispose();
            canvas.RemoveFromVisualTree();
            canvas = null;
            Log.info(this, "Page was fully unloaded");
        }

    }
}
