using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace SQL_CLIENT.Views;

public partial class NavigatorView : Window
{
    private ListBox listBox;
    public NavigatorView(List<string> queryResults)
    {
        InitializeComponent();
        
        listBox = this.FindControl<ListBox>("ResultListBox");
        listBox.ItemsSource = queryResults;
    }
}