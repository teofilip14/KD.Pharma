using KD.WPF.Client.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace KD.WPF.Client.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            ((LoginViewModel)DataContext).CloseWindow += CloseWindow;
        }
        private void CloseWindow()
        {
            this.DialogResult = true;
            this.Close();
        }
        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
