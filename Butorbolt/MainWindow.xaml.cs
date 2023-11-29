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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Butorbolt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ButorModel> butorok = new List<ButorModel>();
        List<AlapanyagModel> alapanyagok = new List<AlapanyagModel>();

        public MainWindow()
        {
            InitializeComponent();
            butorok = ButorModel.select("", null);
            adatokDG.ItemsSource = butorok;
            alapanyagok = AlapanyagModel.select();
            alapanyagok.Insert(0, new AlapanyagModel(null, "Összes alapanyag"));
            alapanyagCBX.ItemsSource = alapanyagok;
            alapanyagCBX.SelectedItem = 0;
        }

        private void ujBTN_Click(object sender, RoutedEventArgs e)
        {
            ButorModel ujButor = new ButorModel();
            var ablak = new ButorReszletek(ujButor);
            if(ablak.ShowDialog() == true)
            {
                butorok = ButorModel.select("", null);
                adatokDG.ItemsSource = butorok;
            }
        }

        private void modositBTN_Click(object sender, RoutedEventArgs e)
        {
            if (adatokDG.SelectedItem != null)
            {
                ButorModel modositando = (ButorModel)adatokDG.SelectedItem;
                var ablak = new ButorReszletek(modositando);
                if (ablak.ShowDialog() == true)
                {
                    butorok = ButorModel.select("", null);
                    adatokDG.ItemsSource = butorok;
                }
            }
        }

        private void torolBTN_Click(object sender, RoutedEventArgs e)
        {
            if (adatokDG.SelectedItem != null)
            {
                ButorModel torlendo = (ButorModel)adatokDG.SelectedItem;
                if (MessageBox.Show($"Biztosan törölni kívánja a {torlendo.megnevezes} bútort?", "Törlés", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ButorModel.delete(torlendo);
                    butorok = ButorModel.select("", null);
                    adatokDG.ItemsSource = butorok;
                }
            }
        }

        private void keresesBTN_Click(object sender, RoutedEventArgs e)
        {
            butorok = ButorModel.select(megnevezesTXB.Text ,(int?)alapanyagCBX.SelectedValue);
            adatokDG.ItemsSource = butorok;
        }
    }
}
