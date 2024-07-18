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
    /// Interaction logic for AirConditioner_Manegement.xaml
    /// </summary>
    public partial class AirConditioner_Manegement : Window
    {
        private IEnumerable<AirConditioner> airConditioners;
        private IEnumerable<SupplierCompany> supplierCompanies;
        private AirConditionerRepository repoAir;
        private SupplierRepository repoSuppli;
        private AirConditioner airConditioner;
        public AirConditioner_Manegement()
        {
            var db = new AirConditionerShop2024DbContext();
            repoAir = new AirConditionerRepository(db);
            int? role = CurrentUser.LoggedUser.Role;
            InitializeComponent();

            if (role == 1)
            {
                Supplier();
                ShowList();
            }
            else if(role == 2)
            {
                btn_add.IsEnabled = false;
                btn_del.IsEnabled = false;
                btn_update.IsEnabled = false;

                Supplier();
                ShowList();

            }
            else
            {
                btn_add.IsEnabled = false;
                btn_del.IsEnabled = false;
                btn_update.IsEnabled = false;
                btn_search.IsEnabled = false;
            }
            
        }
        private void Supplier()
        {
            var db = new AirConditionerShop2024DbContext();
            repoSuppli = new SupplierRepository(db);
            supplierCompanies = repoSuppli.GetAll();
            cbb_suppli.ItemsSource = supplierCompanies;
            cbb_suppli.DisplayMemberPath = "SupplierName";
            cbb_suppli.SelectedValuePath = "SupplierId";
        }
        private void ShowList()
        {
            airConditioners = repoAir.GetAll();
            dgv_list.ItemsSource = airConditioners;
        }
        private AirConditioner newAirConditioner()
        {
            int.TryParse(txt_id.Text, out int id);
            int.TryParse(txt_quantity.Text, out int quantity);
            double.TryParse(txt_price.Text, out double price);
            var airconditioner = new AirConditioner
            {
                AirConditionerId = id,
                AirConditionerName = txt_name.Text,
                Warranty = txt_warranty.Text,
                SoundPressureLevel = txt_level.Text,
                FeatureFunction = txt_function.Text,
                Quantity = quantity,
                DollarPrice = price,
                SupplierId = (string)cbb_suppli.SelectedValue,

            };
            return airconditioner;

        }
        private void LoadField()
        {
            txt_id.Text = airConditioner.AirConditionerId.ToString();
            txt_name.Text = airConditioner.AirConditionerName;
            txt_warranty.Text = airConditioner.Warranty;
            txt_level.Text = airConditioner.SoundPressureLevel;
            txt_function.Text = airConditioner.FeatureFunction;
            txt_price.Text = airConditioner.DollarPrice.ToString();
            txt_quantity.Text = airConditioner.Quantity.ToString();
            cbb_suppli.SelectedValue = airConditioner.SupplierId;
        }
        private void ClearAll()
        {
            txt_id.Text =string.Empty;
            txt_name.Text = string.Empty;
            txt_warranty.Text = string.Empty;
            txt_level.Text = string.Empty;
            txt_function.Text = string.Empty;
            txt_price.Text = string.Empty;
            txt_quantity.Text = string.Empty;
            cbb_suppli.SelectedValue = string.Empty;
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            var addCondtioner = newAirConditioner();
            var checkValidate = repoAir.checkValidate(addCondtioner, out string error);
            if (checkValidate)
            {
                repoAir.Add(addCondtioner);
                ShowList();
                ClearAll();
            }
            else
            {
                MessageBox.Show(error);
                return;
            }
            
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            var updateAirConditioner = newAirConditioner();
            var checkValidate = repoAir.checkValidate(updateAirConditioner, out string error);
            if (checkValidate)
            {
                repoAir.Update(updateAirConditioner);
                ShowList();
                ClearAll();
            }
            else
            {
                MessageBox.Show(error);
                return;
            }
        }

        private void dgv_list_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(dgv_list.SelectedItem is AirConditioner selected)
            {
                this.airConditioner = selected;
                LoadField();
            }
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            var deleteAircondtioner = newAirConditioner();
            var findAir = repoAir.Findbyid(deleteAircondtioner);
            if(findAir != null)
            {
                var result = MessageBox.Show("Are you sure to delete this Product", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    repoAir.Remove(findAir);
                    ShowList();
                    ClearAll() ;
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select");
                return;
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            string searchfunction = txt_function.Text;
            int.TryParse(txt_quantity.Text, out int quantity);
            if(quantity == 0 && searchfunction.IsNullOrEmpty())
            {
                ShowList();
                return;
            }
            var resultList = repoAir.Search(searchfunction, quantity);
            if(resultList != null)
            {
                dgv_list.ItemsSource = resultList;

            }
        }
    }
}
