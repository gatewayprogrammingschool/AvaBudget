using AvaBudget.ViewModels;

using Avalonia.Controls;

namespace AvaBudget.Views;

public partial class MainView : UserControl
{
    public MainViewModel ViewModel => DataContext as MainViewModel;
    public MainView()
    {
        InitializeComponent();
    }
}
