﻿namespace Movies.Client.Models;

public class UserInfoViewModel
{
    public Dictionary<string, string> UserInfoDictionary { get; private set; } = null;

    public UserInfoViewModel(Dictionary<string, string> userInfoDictionary)
    {
        this.UserInfoDictionary = userInfoDictionary;
    }
}
