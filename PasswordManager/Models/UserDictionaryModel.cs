using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PasswordManager.Models;

public class UserDictionaryModel
{
    private readonly SimpleDictionary<string?, string?> _dict = new();
    
    public void Create(string? username, string? password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new  ArgumentException("Username and Password are required.");
        }
            
        if (_dict.UserExists(username))
        {
            throw new ArgumentException("Username already exists.");
        }

        try
        {
            _dict.Add(username, password);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        Console.WriteLine($"User {username} created.");
    }

    public void Update(string? username, string? password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new  ArgumentException("Username and Password are required.");
        }

        if (!_dict.UserExists(username))
        {
            throw new ArgumentException("Username does not exist.");
        }

        try
        {
            _dict.Update(username, password);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        Console.WriteLine($"User {username} updated.");
    }

    public void Delete(string? username)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new  ArgumentException("Username is required.");
        }

        try
        {
            _dict.Remove(username);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        Console.WriteLine($"User {username} deleted.");
    }

    public void Test()
    {
        var dict = new SimpleDictionary<string, string>();

        dict.Add("user1", "password1");
        dict.Add("user2", "password2");

        if (dict.TryGetValue("user1", out string password))
        {
            Console.WriteLine($"Password for user1: {password}");
        }

        dict.Remove("user1");

        if (!dict.TryGetValue("user1", out _))
        {
            Console.WriteLine("user1 was removed.");
        }
    }

    public List<KeyValuePair<string?, string?>> ToList()
    {
        return _dict.ToList();
    }
}