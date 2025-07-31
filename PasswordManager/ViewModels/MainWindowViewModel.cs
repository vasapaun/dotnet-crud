using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Models;
using Tmds.DBus.Protocol;

namespace PasswordManager.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _username;
    [ObservableProperty]
    private string? _password;
    
    [ObservableProperty]
    private string? _statusMessage = "";
    [ObservableProperty]
    private string? _statusMessageColor = "";
    
    private readonly UserDictionaryModel _model =  new UserDictionaryModel();
    
    public ObservableCollection<UserEntry> UserList { get; } = new();

    [RelayCommand]
    public void Create()
    {
        try
        {
            _model.Create(Username, Password);
            StatusMessage = $"User {Username} created";
            StatusMessageColor = "Green";
        }
        catch (ArgumentException ex)
        {
            StatusMessage = ex.Message;
            StatusMessageColor = "Red";
        }
        
        RefreshUserList();
    }

    [RelayCommand]
    public void Update()
    {
        try
        {
            _model.Update(Username, Password);
            StatusMessage = $"User {Username} updated";
            StatusMessageColor = "Green";
        }
        catch (ArgumentException ex)
        {
            StatusMessage = ex.Message;
            StatusMessageColor = "Red";
        }
        
        RefreshUserList();
    }

    [RelayCommand]
    public void Delete()
    {
        try
        {
            _model.Delete(Username);
            StatusMessage = $"User {Username} deleted";
            StatusMessageColor = "Green";
        }
        catch (ArgumentException ex)
        {
            StatusMessage = ex.Message;
            StatusMessageColor = "Red";
        }
        
        RefreshUserList();
    }

    public void RefreshUserList()
    {
        UserList.Clear();

        foreach (var (username, password) in _model.ToList())
        {
            UserList.Add(new UserEntry
            {
                Username = username,
                Password = password
            });
        }
    }
}