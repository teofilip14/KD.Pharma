﻿using Prism.Mvvm;
using System;

namespace KD.WPF.Client.Models
{
    public class UserModel : BindableBase
    {
        #region Constructor
        public UserModel()
        {

        }
        #endregion Constructor

        #region Properties
        private Guid userId;
        public Guid UserId
        {
            get { return userId; }
            set { SetProperty(ref userId, value); }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private bool isAdmin;
        public bool IsAdmin 
        { 
            get { return isAdmin; }
            set { SetProperty(ref isAdmin, value); } 
        }

        #endregion Properties
    }
}
