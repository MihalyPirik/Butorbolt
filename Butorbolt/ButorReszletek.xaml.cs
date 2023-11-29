using Butorbolt.Models;
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

namespace Butorbolt
{
    /// <summary>
    /// Interaction logic for ButorReszletek.xaml
    /// </summary>
    public partial class ButorReszletek : Window
    {
        public ButorModel Bmodel { get; set; }
        List<AlapanyagModel> alapanyagok = new List<AlapanyagModel>();

        int id = 0;

        public ButorReszletek(ButorModel model)
        {
            InitializeComponent();
            this.Bmodel = model;
            this.DataContext = Bmodel;
            id = model.id;
            alapanyagok = AlapanyagModel.select();
            alapanyagCBX.ItemsSource = alapanyagok;
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Bmodel.megnevezes != null && this.Bmodel.szin != null && alapanyagCBX.SelectedItem != null)
            {
                if (id == 0)
                {
                    //Insert
                    this.Bmodel.id = ButorModel.insert(this.Bmodel);
                }
                else
                {
                    //Update
                    ButorModel.update(this.Bmodel);
                }
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("A bútor megnevezésének, az alapanyagának és színének megadása kötelező!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
