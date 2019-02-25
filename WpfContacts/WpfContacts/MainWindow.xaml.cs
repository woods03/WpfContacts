using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace WpfContacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string defimgpath = "/Image/def_person.png";
        private string imgp = "/Image/def_person.png";
        private string info_imgp = "/Image/def_person.png";

        private int indexOfSelectedItem;
        
        public ObservableCollection<Contact> contactList { get; set; }
        
        public string imagePath
        {
            get
            {
                return imgp;
            }

            set
            {
                imgp = value;
            }
        }
        public string info_imagePath
        {
            get
            {
                return info_imgp;
            }

            set
            {
                info_imgp = value;
            }
        }

        public MainWindow()
        {
            if (File.Exists("contacts.json"))
            {
                contactList = DeserializeJson();
            }
            else { contactList = new ObservableCollection<Contact>(); }

            InitializeComponent();
            
            contList.ItemsSource = contactList;
            this.DataContext = this;
        }

        #region Json
        private void SerializeJson()
        {
            using (StreamWriter file = File.CreateText("contacts.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, contactList);
            }
        }

        private ObservableCollection<Contact> DeserializeJson()
        {
            ObservableCollection<Contact> desMS = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(File.ReadAllText("contacts.json"));
            return desMS;
        }
        #endregion

        #region add contact

        private void add_contact_btn_Click(object sender, RoutedEventArgs e)
        {
            InfoColumn.Width = new GridLength(0, GridUnitType.Star);
            AddColumn.Width = new GridLength(50, GridUnitType.Star);
            MainColumn.Width = new GridLength(50, GridUnitType.Star);
        }

        private void Add_save_btn_Click(object sender, RoutedEventArgs e)
        {
            bool checker = false;
            if(add_Number_txtBox.Text == "")
            {
                add_Number_txtBox.Background = new SolidColorBrush(Colors.OrangeRed);
            }
            if(add_Name_txtBox.Text == "")
            {
                add_Name_txtBox.Background = new SolidColorBrush(Colors.OrangeRed);
            }

            if(add_Number_txtBox.Text != "" && add_Name_txtBox.Text != "")
            {
                foreach(var item in contactList.ToList())
                {
                    if(item.Name == add_Name_txtBox.Text)
                    {
                        checker = true;
                        MessageBox.Show("Please, choose another name");
                        break;
                    }
                }
                if(!checker)
                {
                    contactList.Add(new Contact(add_Name_txtBox.Text, add_Number_txtBox.Text, imagePath));
                }
            }

            add_Name_txtBox.Text = "";
            add_Number_txtBox.Text = "";
            UpdateAddImage(defimgpath);

            SerializeJson();
        }

        private void add_cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            add_Name_txtBox.Text = "";
            add_Number_txtBox.Text = "";
            UpdateAddImage(defimgpath);

            
            InfoColumn.Width = new GridLength(0, GridUnitType.Star);
            AddColumn.Width = new GridLength(0, GridUnitType.Star);
            MainColumn.Width = new GridLength(100, GridUnitType.Star);
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
                UpdateAddImage(op.FileName);
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

        private void UpdateAddImage(string path)
        {
            imagePath = path;
            add_Photo_btn_image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
        }

        #endregion

        #region info contact
        
        private void contList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoColumn.Width = new GridLength(50, GridUnitType.Star);
            AddColumn.Width = new GridLength(0, GridUnitType.Star);
            MainColumn.Width = new GridLength(50, GridUnitType.Star);

            var list = (ListBox)sender;
            var cnt = (Contact)list.SelectedItem;

            indexOfSelectedItem = contactList.IndexOf(cnt);

            info_Name_txtBox.Text = cnt.Name;
            info_Number_txtBox.Text = cnt.Number;
            info_imagePath = cnt.img;

            info_Photo_btn_image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
        }

        private void info_edit_btn_Click(object sender, RoutedEventArgs e)
        {
            info_save_btn.Visibility = Visibility.Visible;
            info_cancel_btn.Visibility = Visibility.Visible;

            info_Name_txtBox.IsReadOnly = false;
            info_Number_txtBox.IsReadOnly = false;

            info_Name_txtBox.Background = new SolidColorBrush(Colors.White);
            info_Number_txtBox.Background = new SolidColorBrush(Colors.White);
            
            info_Photo_btn.IsEnabled = true;
        }

        private void info_Photo_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                info_imagePath = op.FileName;
                info_Photo_btn_image.GetBindingExpression(Image.SourceProperty).UpdateTarget();
            }
        }

        private void info_save_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                contactList.Add(new Contact(info_Name_txtBox.Text, info_Number_txtBox.Text, info_imagePath));
                contactList.Remove((Contact)contList.SelectedItem);
            }
            catch
            {
                
            }

            closeInfoColumn();
            SerializeJson();
        }

        private void info_cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            closeInfoColumn();
        }

        private void closeInfoColumn()
        {
            info_save_btn.Visibility = Visibility.Hidden;
            info_cancel_btn.Visibility = Visibility.Hidden;

            info_Name_txtBox.IsReadOnly = true;
            info_Number_txtBox.IsReadOnly = true;

            info_Name_txtBox.Background = new SolidColorBrush(Colors.Black);
            info_Number_txtBox.Background = new SolidColorBrush(Colors.Black);

            info_Photo_btn.IsEnabled = false;

            InfoColumn.Width = new GridLength(0, GridUnitType.Star);
            AddColumn.Width = new GridLength(0, GridUnitType.Star);
            MainColumn.Width = new GridLength(50, GridUnitType.Star);
        }

        #endregion

        #region delete contact

        private void info_delte_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                contactList.Remove((Contact)contList.SelectedItem);
            }
            catch
            {

            }

            closeInfoColumn();
            SerializeJson();
        }

        #endregion

        #region search from list

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in contactList)
            {
                if(item.Name.ToUpper().Contains(search_txt.Text.ToUpper()))
                {
                    contList.SelectedItem = item;
                    break;
                }
            }
        }

        private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in contactList)
            {
                if (item.Name.ToUpper().Contains(search_txt.Text.ToUpper()))
                {
                    contList.SelectedItem = item;
                    break;
                }
            }
        }

        #endregion

        //ne razreshayet dobavku k numberTxtBox bukvi
        private void add_Number_txtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
