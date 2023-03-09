// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using AppStoreRemastered.Views;
using System.ComponentModel;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage.Pickers;
using Windows.Storage;
using CommunityToolkit.WinUI.Notifications;
using System.Net;
using Windows.Storage.Pickers.Provider;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AppStoreRemastered.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class UTEUWPPage : Page
{
    public UTEUWPPage()
    {
        this.InitializeComponent();
        Uri sourcetext = new Uri("https://github.com/jpbandroid/UpdateZips/raw/main/UTE-23H1-version.txt");
        WebClient client = new WebClient();
        client.DownloadFile(sourcetext, "C:/Users/Kuba/AppStore/UTE-23H1-version.txt");
        // Use it on your StreamReader
        var filename = "C:/Users/Kuba/AppStore/UTE-23H1-version.txt";
        StreamReader sr = new StreamReader(filename);
        versiontext.Text = "version " + sr.ReadToEnd();
    }

    private async void download(object sender, RoutedEventArgs e)
    {
        try
        {
            WebClient client = new WebClient();
            var myappuri = new Uri("https://github.com/jpbandroid/UpdateZips/raw/main/UTE-23H1-latest.msixbundle");
            //BackgroundDownloader downloader = new BackgroundDownloader();
            Uri source = myappuri;
            client.DownloadFile(source, "C:/Users/Kuba/Downloads/UTEUWP23H1.msixbundle");
        }
        catch (Exception ex)
        {
        }
        new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", 9813)
            .AddText("UltraTextEdit UWP download complete")
            .AddText("Take a look!")
            .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 5, your TFM must be net5.0-windows10.0.17763.0 or greater
    }

    

    private void hyperlinkclick(object sender, RoutedEventArgs e)
    {
        this.Frame.Navigate(typeof(jpb_AppsPage));
    }
}
