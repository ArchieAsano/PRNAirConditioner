using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Repositories;
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

namespace AirConditionerShop_ThuAn
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private StaffRepository repoStaf;
        public Login()
        {
            var db = new AirConditionerShop2024DbContext();
            repoStaf = new StaffRepository(db);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txt_user.Text;
            string password = txt_password.Text;    
            if(username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                MessageBox.Show("Missing UserName or Password");
                return;
            }
            var user = repoStaf.checkLogin(username, password);
            if(user != null)
            {
                CurrentUser.LoggedUser = user;
                var newwindow = new AirConditioner_Manegement();
                newwindow.Show();
                //this.Close();
            }
            else
            {
                MessageBox.Show("You dont have permission to access this function");
                return;
            }
        }
    }
}
