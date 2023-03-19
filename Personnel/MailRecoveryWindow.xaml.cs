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
    /// Логика взаимодействия для MailRecoveryWindow.xaml
    /// </summary>
    public partial class MailRecoveryWindow : Window
    {
        AppContext db;
        int code;
        bool Ch = false;
        User user;
        public MailRecoveryWindow()
        {
            InitializeComponent();
            db = new AppContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTb.Text != "")
            {
                user = db.Users.FirstOrDefault(b => b.Login == LoginTb.Text);
                if (user != null)
                {
                    if (user.Mail != null)
                    {
                        var rand = new Random();
                        code = rand.Next(10000, 99999);
                        if (Methods.SendMailRecovery(user.Mail, code))
                        {
                            MailSP.Visibility = Visibility.Collapsed;
                            CodeSP.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            LoginTb.Text = "";
                            MessageBox.Show("Ошибка при отправлении кода.\nОбратитесь к администратору.");
                        }
                    }
                    else
                    {
                        LoginTb.Text = "";
                        MessageBox.Show("Почта не прикреплена к аккаунту.\nОбратитесь к администратору.");
                    }
                }
                else
                {
                    LoginTb.Text = "";
                    MessageBox.Show("Пользователь не найден.");
                }
            }
            else MessageBox.Show("Поле не заполнено.");
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if(CodeSP.Visibility == Visibility.Visible)
            {
                MailSP.Visibility = Visibility.Visible;
                CodeSP.Visibility = Visibility.Collapsed;
            }
            else if(PassSP.Visibility == Visibility.Visible)
            {
                CodeSP.Visibility = Visibility.Visible;
                PassSP.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(CodeTb.Text != "")
            { 
                if(code == Convert.ToInt32(CodeTb.Text))
                {
                    CodeSP.Visibility = Visibility.Collapsed;
                    PassSP.Visibility = Visibility.Visible;
                }
                else MessageBox.Show("Код не верный");
            }
            else MessageBox.Show("Введите код");
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if ((passwordTxtBox.Text != "" && passwordTxtBox2.Text != "") || (PasswordBox.Password != "" && PasswordBox2.Password != ""))
            {
                if (Ch == false && PasswordBox.Password == PasswordBox2.Password)
                {
                    user.Password = Methods.StringToHash(PasswordBox.Password);
                    db.SaveChanges();
                    MessageBox.Show("Пароль изменен");
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else if (Ch == true && passwordTxtBox.Text == passwordTxtBox2.Text)
                {
                    user.Password = Methods.StringToHash(passwordTxtBox.Text);
                    db.SaveChanges();
                    MessageBox.Show("Пароль изменен");
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else MessageBox.Show("Пароли не совпадают");
            }
            else MessageBox.Show("Не все поля заполнены");
        }

        private void CodeSP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
