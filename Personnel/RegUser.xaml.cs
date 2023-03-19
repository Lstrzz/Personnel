using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Логика взаимодействия для RegUser.xaml
    /// </summary>
    public partial class RegUser : Window
    {
        AppContext db;
        bool Ch = false;
        public RegUser()
        {
            InitializeComponent();
            db = new AppContext();
            List<Role> roles = db.Roles.ToList();
            RoleCb.ItemsSource = roles;
            List<Statuse> statuses = db.Statuses.Where(b=>b.Title!= "Уволен").ToList();
            StatusCb.ItemsSource = statuses;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FIOTb.Text != "" && LoginTb.Text != "" && RoleCb.Text != "" && StatusCb.Text !=""&& ((passwordTxtBox.Text != "" && passwordTxtBox2.Text != "") || (PasswordBox.Password != "" && PasswordBox2.Password != "")))
            {
                User user1 = db.Users.FirstOrDefault(b => b.Login == LoginTb.Text);
                if (user1 == null)
                {
                    string pass = null;
                    if (Ch == false && PasswordBox.Password == PasswordBox2.Password)
                    {
                        pass = PasswordBox.Password;
                        string HashPass = Methods.StringToHash(pass);
                        Role role = db.Roles.FirstOrDefault(b => b.Title == RoleCb.Text);
                        Statuse statuse = db.Statuses.FirstOrDefault(b => b.Title == StatusCb.Text);
                        User user = new User(LoginTb.Text, HashPass, role.Id, FIOTb.Text, statuse.Id);
                        db.Users.Add(user);
                        db.SaveChanges();
                        this.DialogResult = true;
                    }
                    else if (Ch == true && passwordTxtBox.Text == passwordTxtBox2.Text)
                    {
                        pass = passwordTxtBox.Text;
                        string HashPass = Methods.StringToHash(pass);
                        Role role = db.Roles.FirstOrDefault(b => b.Title == RoleCb.Text);
                        Statuse statuse = db.Statuses.FirstOrDefault(b => b.Title == StatusCb.Text);
                        User user = new User(LoginTb.Text, HashPass, role.Id, FIOTb.Text, statuse.Id);
                        db.Users.Add(user);
                        db.SaveChanges();
                        this.DialogResult = true;
                    }
                    else MessageBox.Show("Пароли не совпадают");
                }
                else MessageBox.Show("Пользователь с таким логином уже существует");
            }
            else MessageBox.Show("Не все поля заполнены");
        }
    }
}
