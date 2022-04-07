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

using UWP_project.Support;
using UWP_project.Graphic;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_project.Screen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            this.InitializeComponent();
        }


        void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Log.info(this, "Strona jest jeszcze niezaładowana - przygotowywnie");
            canvas.RemoveFromVisualTree();
            canvas = null;
            Log.info(this, "Strona została wyczyszczona");
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

        }

        private void MainManuSettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainManuExitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainManuCloseSettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainManuCloseCreatePlayerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MainManuCreatePlayerButton_Click(object sender, RoutedEventArgs e)
        {

        }



        private void canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            Log.info(this, "Rozpoczęcie createResources");
            Graphic.Textures.DeleteInstance();

        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {

        }
    }
}
