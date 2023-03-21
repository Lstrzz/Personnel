using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EdtUser.xaml
    /// </summary>
    public partial class EdtUser : Window
    {
        AppContext db;
        bool Ch = false;
        public EdtUser( int Id)
        {
            InitializeComponent();
            db = new AppContext();
            User user = db.Users.FirstOrDefault(b=>b.Id==Id);
            UsrtL.Content = user.FIO;
            if(user.Phone!=null) PhoneTb.Text = user.Phone;
            if(user.Mail!=null) MailTb.Text = user.Mail;
            if(user.FIO!=null)FIOTb.Text = user.FIO;
            if(user.Post!=null) PostTb.Text = user.Post;
            if(user.Salary!=null) SalayTb.Text=user.Salary;
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passwordTxtBox.Text = PasswordBox.Password;
            PasswordBox.Visibility = Visibility.Collapsed;
            passwordTxtBox.Visibility = Visibility.Visible;
            passwordTxtBox2.Text = PasswordBox2.Password;
            PasswordBox2.Visibility = Visibility.Collapsed;
            passwordTxtBox2.Visibility = Visibility.Visible;
            Ch = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = passwordTxtBox.Text;
            passwordTxtBox.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordBox2.Password = passwordTxtBox2.Text;
            passwordTxtBox2.Visibility = Visibility.Collapsed;
            PasswordBox2.Visibility = Visibility.Visible;
            Ch = false;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SalayTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        { 
            if(SalayTb.Text.Contains(',')&&(SalayTb.Text.Substring(SalayTb.Text.IndexOf(',')).Length==3))
            {
                e.Handled = true;
            }
            else if (SalayTb.Text == "0" && e.Text != "," && (Char.IsDigit(e.Text, 0) || (e.Text == "," && !SalayTb.Text.Contains(','))))
            {
                SalayTb.Text = "";
                e.Handled = false;
            }
            else if (Char.IsDigit(e.Text, 0) || (e.Text == ","&& !SalayTb.Text.Contains(',')))
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }
    }
}
