using KD.WPF.Client.APIClient.RestServices;
using KD.WPF.Client.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KD.WPF.Client.ViewModels
{
    public class UserViewModel : BindableBase
    {
        #region Properties
        private readonly UserRestService userRestService;
        private ObservableCollection<UserModel> users;
        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        private UserModel selectedUser;
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set { SetProperty(ref selectedUser, value); }
        }

        public DelegateCommand DeleteUserCommand { get; private set; }
        public DelegateCommand AddUserCommand { get; private set; }
        public DelegateCommand UpdateUserCommand { get; private set; }
        #endregion Properties

        #region Constructor
        public UserViewModel(UserRestService userRestService)
        {
            this.userRestService = userRestService;
            DeleteUserCommand = new DelegateCommand(DeleteUser);
            AddUserCommand = new DelegateCommand(AddUser);
            UpdateUserCommand = new DelegateCommand(UpdateUser);
            Task.Run(() => this.Initialize()).Wait();
        }
        #endregion Constructor

        #region Methods
        private async Task Initialize()
        {
            try
            {
                await GetUsers();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void DeleteUser()
        {
            if(selectedUser == null)
            {
                MessageBox.Show("Please select the user you want to delete !");
                return;
            }
            Guid userId = selectedUser.UserId;
            await userRestService.DeleteUserAsync(userId);
            await GetUsers();
        }

        private async void AddUser()
        {
            UserModel newUser = Users.FirstOrDefault(s => s.UserId == Guid.Empty);
            if(newUser == null)
            {
                MessageBox.Show("Please enter a user into the grid !");
            }
            await userRestService.CreateUserAsync(newUser);
            await GetUsers();
        }
              
        private async void UpdateUser()
        {
            if(selectedUser == null)
            {
                MessageBox.Show("Please select the user you want to update !");
                return;
            }
            await userRestService.UpdateUserAsync(selectedUser.UserId, selectedUser);
            await GetUsers();
        }

        private async Task GetUsers()
        {
            Users = new ObservableCollection<UserModel>(await userRestService.GetAllUsersAsync());
        }

        #endregion Methods
    }
}