﻿using System;

using Windows.UI.Xaml.Media.Imaging;

namespace SampleSecuredWebAPI.Models
{
    public class UserData
    {
        public string Name { get; set; }

        public string UserPrincipalName { get; set; }

        public BitmapImage Photo { get; set; }
    }
}
