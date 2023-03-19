using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Personnel
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        bool Ch = false;
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passwordTxtBox.Text = PasswordBox.Password;
            PasswordBox.Visibility = Visibility.Collapsed;
            passwordTxtBox.Visibility = Visibility.Visible;
            Ch = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = passwordTxtBox.Text;
            passwordTxtBox.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
            Ch = false;
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MailRecoveryWindow mailRecoveryWindow = new MailRecoveryWindow();
            mailRecoveryWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string log = Methods.EncodeDecrypt(LoginTb.Text);
            MessageBox.Show(log);
            log = Methods.EncodeDecrypt(log);
            MessageBox.Show(log);
        }
    }
}
