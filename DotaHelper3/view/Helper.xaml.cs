using Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace DotaHelper3.view
{
    /// <summary>
    /// Interaction logic for Helper.xaml
    /// </summary>
    public partial class Helper : UserControl
    {
        private BindingList<string> _dataCBList = new BindingList<string>();
        public Helper()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var heroes = File.ReadAllLines("src/heroes.txt");
            foreach (var hero in heroes)
            {
                _dataCBList.Add(hero);
            }
            comboB1.ItemsSource = _dataCBList;
        }

        private bool isThreadWorks = false;
        string[] results = null;
        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!isThreadWorks && comboB1.SelectedItem?.ToString() != null && comboB1.SelectedItem.ToString() != string.Empty)
            {
                var text = comboB1.SelectedItem?.ToString()?.ToLower().Replace(' ', '-') ?? "null";
                string[] builds = null;
                button1.Visibility = Visibility.Hidden;
                results = await Task.Run(() =>
                {
                    isThreadWorks = true;
                    builds = SDotabuff.getDataSet(text).ToArray();
                    isThreadWorks = false;
                    return Task.FromResult(builds);
                });
                build1.Content = results[0];
                button1.Visibility = Visibility.Visible;
                build1.Visibility = Visibility.Visible;
                BLeft.Visibility = Visibility.Visible;
                BRight.Visibility = Visibility.Visible;
                selectedResult = 0;
            }
        }

        private int selectedResult { get; set; }
        private void BLeft_Click(object sender, RoutedEventArgs e)
        {
            if(results != null && selectedResult > 0)
            {
                selectedResult--;
                build1.Content = results[selectedResult];
            }
        }

        private void BRight_Click(object sender, RoutedEventArgs e)
        {
            if (results != null && selectedResult < 4)
            {
                selectedResult++;
                build1.Content = results[selectedResult];
            }
        }
    }
}
