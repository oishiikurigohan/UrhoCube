using Urho;
using Xamarin.Forms;

namespace UrhoCube
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UrhoCubeSurface.Show<Cube>(new ApplicationOptions("Data") { Orientation = ApplicationOptions.OrientationType.LandscapeAndPortrait });
        }
    }
}
