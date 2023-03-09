using AppStoreRemastered.Contracts.Services;
using AppStoreRemastered.Helpers;
using AppStoreRemastered.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

using Windows.System;


namespace AppStoreRemastered.Views;

// TODO: Update NavigationViewItem titles and icons in ShellPage.xaml.
public sealed partial class ShellPage : Page
{
    public ShellViewModel ViewModel
    {
        get;
    }

    private readonly List<string> Apps = new List<string>()
        {
            "MyApp",
            "MyApp UWP",
            "MyApp UWP LTSC",
            "UltraTextEdit UWP"
        };

    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();

        ViewModel.NavigationService.Frame = NavigationFrame;
        ViewModel.NavigationViewService.Initialize(NavigationViewControl);

        // TODO: Set the title bar icon by updating /Assets/WindowIcon.ico.
        // A custom title bar is required for full window theme and Mica support.
        // https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
        App.MainWindow.Activated += MainWindow_Activated;
        AppTitle.Text = "AppDisplayName".GetLocalized();
    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        // Since selecting an item will also change the text,
        // only listen to changes caused by user entering text.
        if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        {
            var suitableItems = new List<string>();
            var splitText = sender.Text.ToLower().Split(" ");
            foreach (var app in Apps)
            {
                var found = splitText.All((key) =>
                {
                    return app.ToLower().Contains(key);
                });
                if (found)
                {
                    suitableItems.Add(app);
                }
            }
            if (suitableItems.Count == 0)
            {
                suitableItems.Add("No results found");
            }
            sender.ItemsSource = suitableItems;
        }
    }

    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";

        AppTitle.Foreground = (SolidColorBrush)App.Current.Resources[resource];
    }

    private void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
    {
        AppTitleBar.Margin = new Thickness()
        {
            Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2 : 1),
            Top = AppTitleBar.Margin.Top,
            Right = AppTitleBar.Margin.Right,
            Bottom = AppTitleBar.Margin.Bottom
        };
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }

    private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
    {
        var chosen = args.SelectedItem as string;
        if (chosen != null)
        {
            //if (chosen == "MyApp UWP")
            //{
            //    shellFrame.Navigate(typeof(MyApp_UWPPage));
            //}
            //else if (chosen == "MyApp UWP LTSC")
            //{
            //    shellFrame.Navigate(typeof(MyApp_UWP_LTSCPage));
            //}
            //else if (chosen == "MyApp")
            //{
            //    shellFrame.Navigate(typeof(MyAppPage));
            //}
            if (chosen == "UltraTextEdit UWP")
            {
               NavigationFrame.Navigate(typeof(UTEUWPPage));
                NavigationViewControl.SelectedItem= null;
            }
        }
    }

    private void NavigationViewControl_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (NavigationViewControl.SelectedItem != null)
        {
            NavigationViewControl.SelectedItem = args.SelectedItem;
        }
    }
}
