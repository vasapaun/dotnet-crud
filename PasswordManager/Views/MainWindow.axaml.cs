using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PasswordManager.ViewModels;

namespace PasswordManager.Views;

public partial class MainWindow : Window
{
    
    private readonly MainWindowViewModel _vm;
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = _vm = new MainWindowViewModel();
    }

    private void CreateUser_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _vm.Create();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}