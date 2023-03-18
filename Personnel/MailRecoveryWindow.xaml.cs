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
    /// Логика взаимодействия для MailRecoveryWindow.xaml
    /// </summary>
    public partial class MailRecoveryWindow : Window
    {
        public MailRecoveryWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MailSP.Visibility = Visibility.Collapsed;
            CodeSP.Visibility = Visibility.Visible;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if(CodeSP.Visibility == Visibility.Visible)
            {
                MailSP.Visibility = Visibility.Visible;
                CodeSP.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
