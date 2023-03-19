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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Personnel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext db;
        public MainWindow()
        {
            InitializeComponent();
            db= new AppContext();
            List<User> users = db.Users.ToList();
            UsersDg.ItemsSource = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegUser regUser = new RegUser();
            if (regUser.ShowDialog() == true)
            {
                List<User> users = db.Users.ToList();
                UsersDg.ItemsSource = users;
                AllCB.IsChecked = true;
                ProbationCb.IsChecked = false;
                WorksCb.IsChecked = false;
                DismissedCb.IsChecked = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(UsersDg.SelectedItem is User Selected) 
            {
                User user = db.Users.FirstOrDefault(b=>b.Login==Selected.Login);
                db.Users.Remove(user);
                db.SaveChanges();
                List<User> users = db.Users.ToList();
                UsersDg.ItemsSource = users;
                AllCB.IsChecked = true;
                ProbationCb.IsChecked = false;
                WorksCb.IsChecked = false;
                DismissedCb.IsChecked = false;
                MessageBox.Show("Пользователь удален");
            }
        }

        private void AllCB_Click(object sender, RoutedEventArgs e)
        {
            AllCB.IsChecked = true;
            ProbationCb.IsChecked = false;
            WorksCb.IsChecked = false;
            DismissedCb.IsChecked = false;
            List<User> users = db.Users.ToList();
            UsersDg.ItemsSource = users;
        }

        private void ProbationCb_Checked(object sender, RoutedEventArgs e)
        {
            AllCB.IsChecked = false;
            ProbationCb.IsChecked = true;
            WorksCb.IsChecked = false;
            DismissedCb.IsChecked = false;
            List<User> users = db.Users.Where(b=>b.FK_Status==1).ToList();
            UsersDg.ItemsSource = users;
        }

        private void WorksCb_Checked(object sender, RoutedEventArgs e)
        {
            AllCB.IsChecked = false;
            ProbationCb.IsChecked = false;
            WorksCb.IsChecked = true;
            DismissedCb.IsChecked = false;
            List<User> users = db.Users.Where(b => b.FK_Status == 2).ToList();
            UsersDg.ItemsSource = users;
        }

        private void DismissedCb_Checked(object sender, RoutedEventArgs e)
        {
            AllCB.IsChecked = false;
            ProbationCb.IsChecked = false;
            WorksCb.IsChecked = false;
            DismissedCb.IsChecked = true;
            List<User> users = db.Users.Where(b => b.FK_Status == 3).ToList();
            UsersDg.ItemsSource = users;
        }
    }
}
