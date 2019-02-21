using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;
using Microsoft.Win32;

namespace WpfContacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Contact> contactList { get; set; }

        private BitmapImage image = new BitmapImage(); 
        private string imagePath = "/Image/def_person.png";

        public MainWindow()
        {
            InitializeComponent();
            contactList = new ObservableCollection<Contact>();
        }

        private void Add_save_btn_Click(object sender, RoutedEventArgs e)
        {
            if(add_Number_txtBox.Text == "")
            {
                add_Number_txtBox.Background = new SolidColorBrush(Colors.LightYellow);
            }
            if(add_Name_txtBox.Text == "")
            {
                add_Name_txtBox.Background = new SolidColorBrush(Colors.LightYellow);
            }

            if(add_Number_txtBox.Text != "" && add_Number_txtBox.Text != "")
            {
                contactList.Add(new Contact(add_Name_txtBox.Text, add_Number_txtBox.Text, imagePath));
            }
        }

        private void Add_Name_txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            add_Name_txtBox.Background = new SolidColorBrush(Colors.Black);
        }
        
        private void Add_Number_txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            add_Number_txtBox.Background = new SolidColorBrush(Colors.Black);
        }

        private void Add_Photo_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                
            }
        }
    }
}
