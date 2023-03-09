using AppStoreRemastered.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace AppStoreRemastered.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private void myappuwpltsc(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }

    private void myappuwp(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }
}
