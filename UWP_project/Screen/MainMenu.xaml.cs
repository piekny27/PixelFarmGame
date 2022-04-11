using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.System.Update;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using UWP_project.Support;
using UWP_project.Graphic;
using UWP_project.Core;
using UWP_project.Core.Graphic.Background;
using UWP_project.Core.Graphic.Background.Strategy;
using UWP_project.Services;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.UI;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Background;
using Windows.Media.Playback;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_project.Screen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainMenu : Page, IField
    {
        MediaPlayer Music;
        IBackground BackgroundImage;
        BackgroundTaskStatus BackgroundTaskStatus;
        private bool fieldLoaded = false;
        private bool firstDraw = true;

        public CanvasAnimatedControl FieldControl
        {
            get
            {
                return this.canvas;
            }
        }
        public Size Size
        {
            get
            {
                return FieldControl.Size;
            }
        }
        public bool FieldLoaded
        {
            get { return fieldLoaded; }
            private set
            {
                fieldLoaded = value;
                if (fieldLoaded)
                {
                    MainMenuLoadingGrid.Visibility = Visibility.Collapsed;
                    MainMenuGrid.Visibility = Visibility.Visible;
                    Log.info(this, "FieldLoaded set to true");
                }
                else
                {
                    MainMenuLoadingGrid.Visibility = Visibility.Visible;
                    MainMenuGrid.Visibility = Visibility.Collapsed;
                    Log.info(this, "FieldLoaded set to false");
                }
            }
        }


        public MainMenu()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Page_SizeChanged;

            fieldLoaded = false;
            LoadScreenRes();
            LoadSoundVolume();
            LoadDebugSetting();
            Log.info(this, "Constructor initialized");
        }

        void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Log.info(this, "Page is being unloaded - removing associations");
            Music.Dispose();
            canvas.RemoveFromVisualTree();
            canvas = null;
            Log.info(this, "Page was fully unloaded");
        }

        private void MainMenuPlayerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MainManuCreateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainManuDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainManuContinueButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainManuNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            Log.info(this, "User clicked new game button");

            if (!FieldLoaded) return;

            Frame.Navigate(typeof(GamePage));
        }

        private void MainManuSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Log.info(this, "User clicked settings button");

            MainMenuSettingsGrid.Visibility = Visibility.Visible;
            MainMenuGrid.Visibility = Visibility.Collapsed;
            MainMenuSettingsResW.Text = ((Frame)Window.Current.Content).ActualWidth.ToString();
            MainMenuSettingsResH.Text = ((Frame)Window.Current.Content).ActualHeight.ToString();
        }

        private void MainManuExitButton_Click(object sender, RoutedEventArgs e)
        {
            Log.info(this, "User clicked exit button");

            Utility.ExitGame(this);
        }

        private void MainManuCloseSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Log.info(this, "User clicked return to menu button");

            MainMenuSettingsGrid.Visibility = Visibility.Collapsed;
            MainMenuGrid.Visibility = Visibility.Visible;

            string resolutionWidthString = MainMenuSettingsResW.Text.ToString();
            string resolutionHeightString = MainMenuSettingsResH.Text.ToString();
            bool resolutionFullscreenBool = MainMenuFullScreen.IsChecked.Value;

            Utility.SaveSettings("resolutionWidth", resolutionWidthString);
            Utility.SaveSettings("resolutionHeight", resolutionHeightString);
            Utility.SaveSettings("resolutionFullscreen", resolutionFullscreenBool);
            Utility.SaveSettings("resultVolume", MainMenuSoundVolume.Value);

            LoadScreenRes();
        }

        private void MainManuCloseCreatePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainManuCreatePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void MainManuDebugBrowseLogsButton_Click(object sender, RoutedEventArgs e)
        {
            string metroLogPath = Utility.LocalFolder.Path + @"\MetroLogs";
            Log.info(this, "Opening logs folder. Full path is: " + metroLogPath);
            await Windows.System.Launcher.LaunchFolderAsync(await Windows.Storage.StorageFolder.GetFolderFromPathAsync(metroLogPath));

        }

        private void MainManuDebugRemoveTasksButton_Click(object sender, RoutedEventArgs e)
        {
            int taskCount = 0;
            foreach(var task in BackgroundTaskRegistration.AllTasks)
            {
                task.Value.Unregister(true);
                taskCount++;
            }
            Log.info(this,taskCount.ToString() + " task removed");
        }

        async private void MainMenuDebugMode_Click(object sender, RoutedEventArgs e)
        {
            bool? debugMode = MainMenuDebugMode.IsChecked; //nullable
            Log.info(this, "Changed state of debug checkbox to " + debugMode.ToString());
            Utility.SaveSettings("debug", debugMode);

            await PopupDialog.ShowPopupDialog(
                "You need to restart. \nDo you want to exit the game?",
                "Yes",
                () =>
                {
                    Utility.ExitGame(this);
                },
                "No",
                null,
                true);
        }



        private void canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            Log.info(this, "CreateResources started");
            TextureLoader.DeleteInstance();
            Log.info(this, "CreateResources starting parallel task");
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
            Log.info(this, "CreateResources finished");
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            if (firstDraw)
            {
                firstDraw = false;
                BeforeFirstDraw();
            }
            BackgroundImage.Draw(args.DrawingSession);
            BackgroundTaskStatus.Draw(args.DrawingSession);
        }

        private void BeforeFirstDraw()
        {
            Log.info(this, "Operation before first draw started");

            BackgroundImage = new MainMenuBackground(this);
            BackgroundTaskStatus = new BackgroundTaskStatus(10, 5);
            Log.info(this, "Operation before first draw finidhed");
        }

        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            await TextureLoader.Instance.CreateResourcesAsync
            (
                sender,
                (increasePercentage) => { loadingProgressBar.Value += increasePercentage; },
                null,
                MainMenuBackground.background
            );

            Log.info(this, "Music loading");
            Music = await Utility.GetMusic("Di Young - Pixel Pig");
            Music.Volume = 0;
            LoadVolume();
            Music.Play();
            Music.IsLoopingEnabled = true;

            Log.info(this, "Set FieldLoaded as true");
            FieldLoaded = true;

            Log.info(this, "CreateResourcesAsync finished");
        }

        void LoadSoundVolume()
        {
            double? resultVolume = Utility.LoadSettings<double?>("resultVolume");

            Log.info(this, "Loading sound volume choice from settings: " + (resultVolume == null ? ("failure, defaulting to " + 80) : resultVolume.ToString()));

            //Default value
            if (resultVolume == null)
            {
                resultVolume = 80;
            }

            MainMenuSoundVolume.ValueChanged += (a, b) => LoadVolume();
            MainMenuSoundVolume.Value = resultVolume.Value;
        }

        void LoadVolume()
        {
            if (Music != null)
            {
                Music.Volume = MainMenuSoundVolume.Value;
            }
        }
        void LoadScreenRes()
        {
            bool resolutionFullscreen = Utility.LoadSettings<bool>("resolutionFullscreen");

            int resolutionWidth;
            int resolutionHeight;
            string resolutionWidthString = Utility.LoadSettings<string>("resolutionWidth");
            string resolutionHeightString = Utility.LoadSettings<string>("resolutionHeight");

            bool resultWidth = int.TryParse(resolutionWidthString, out resolutionWidth);
            bool resultHeight = int.TryParse(resolutionHeightString, out resolutionHeight);

            //Default values
            if (!resultWidth || !resultHeight)
            {
                resolutionWidth = 1600;
                resolutionHeight = 900;
            }

            Log.info(this, "Loading fullscreen choice from settings: " + (resolutionFullscreen ? "true" : "false"));
            Log.info(this, "Loading width resolution from settings: " + (resultWidth ? "success" : "failure") + ", used " + resolutionWidth.ToString());
            Log.info(this, "Loading height resolution from settings: " + (resultHeight ? "success" : "failure") + ", used " + resolutionHeight.ToString());

            Size windowSize = new Size(resolutionWidth, resolutionHeight);
            var view = ApplicationView.GetForCurrentView();

            if (resolutionFullscreen)
            {
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                view.TryEnterFullScreenMode();
            }
            else
            {
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                view.ExitFullScreenMode();
            }

            ApplicationView.PreferredLaunchViewSize = windowSize;
            view.TryResizeView(windowSize);

            MainMenuSettingsResW.Text = resolutionWidth.ToString();
            MainMenuSettingsResH.Text = resolutionHeight.ToString();
            MainMenuFullScreen.IsChecked = resolutionFullscreen;
        }

        void LoadDebugSetting()
        {
            bool debug = Utility.LoadSettings<bool>("debug");
            Log.info(this, "Loading debugging state: " + debug);

            MainMenuDebugMode.Click -= MainMenuDebugMode_Click;
            MainMenuDebugMode.IsChecked = debug;
            MainMenuDebugMode.Click += MainMenuDebugMode_Click;
        }

        private void Page_SizeChanged(object sender, object e)
        {
            double h = ((Frame)Window.Current.Content).ActualHeight;
            double w = ((Frame)Window.Current.Content).ActualWidth;

            Log.info(sender,"User changed page size manually H:" 
                + w.ToString() + " W: " + h.ToString());

            MainMenuSettingsResW.Text = w.ToString();
            MainMenuSettingsResH.Text = h.ToString();

            Utility.SaveSettings("resolutionWidth",w.ToString());
            Utility.SaveSettings("resolutionHeight", h.ToString());

        }


        public class MainMenuBackground : AbstractBackground
        {

            public static readonly string[][] background = new string[][]
            {
                TextureLoader.BG_GRASS
            };

            public MainMenuBackground(IField field) : base(field)
            {
                Speed = 2;
                Animation = background[0];
                IBackgroundStrategy backgroundStrategy = new RandomMovement(this, field);
                AddStrategy(backgroundStrategy);
            }
        } 
    }
}
