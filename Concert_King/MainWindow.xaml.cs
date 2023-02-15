using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Concert_King
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String SeatName;
        List<SeatClass> seatclass = new List<SeatClass>();
        List<Button> listButtons = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
            GetButtonNames(this, listButtons);
            for (int j = 0; j < listButtons.Count; j++)
            {
                if (listButtons[j].Name != "BtnDelete")
                {
                    listButtons[j].Background = Brushes.GreenYellow;
                }
            }
        }

        private void ftone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((sender as Button).Background != Brushes.Red)
                {
                    popup.IsOpen = true;
                    txtCutomer.Text = "";
                    txtSeat1.IsReadOnly = true;
                    txtSeat1.Text = (sender as Button).Content.ToString();
                    SeatName = (sender as Button).Name.ToString();
                    //  txtSeatNUmber.Visibility = Visibility.Hidden;
                    //InitializeComponent();
                    this.container.MouseDown += delegate (object sender, MouseButtonEventArgs e)
                    {
                        if (Mouse.Captured == container && e.OriginalSource == container)
                        {
                            popup.IsOpen = false;
                        }
                    };

                    this.container.LostKeyboardFocus += delegate
                    {
                        popup.IsOpen = false;
                    };

                    this.popup.Opened += delegate
                    {
                        Mouse.Capture(container, CaptureMode.SubTree);
                    };

                    this.popup.Closed += delegate
                    {
                        if (Mouse.Captured == container)
                        {
                            Mouse.Capture(container, CaptureMode.None);
                        }
                    };

                    this.closeButton.Click += delegate
                    {
                        popup.IsOpen = false;
                        //frfive.Content = "Radha";
                    };
                    this.BookBtn.Click += delegate
                    {

                        SeatClass seatinfo = new SeatClass();
                        if (txtCutomer.Text != "" && txtCutomer.Text != null)
                        {
                            seatinfo.SeatNo = Convert.ToInt32(txtSeat1.Text)!;
                            seatinfo.customerName = txtCutomer.Text;
                            seatinfo.SeatName = SeatName;

                            seatclass.Add(seatinfo);
                            DisplayAllCustomerName();
                        }


                        //     GetLogicalChildCollection(this, listButtons);
                    };
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("That seat is already taken");
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
        }
        private void Closepopup(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }
        private void Book(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }
        public void DisplayAllCustomerName()
        {
            try
            {
                GetButtonNames(this, listButtons);
                for (int i = 0; i < seatclass.Count; i++)
                {
                    for (int j = 0; j < listButtons.Count; j++)
                    {
                        if (listButtons[j].Name.ToLower() == seatclass[i].SeatName.ToLower())
                        {
                            listButtons[j].Content = seatclass[i].customerName;
                            listButtons[j].Background = Brushes.Red;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
        }
        private static void GetButtonNames<T>(DependencyObject parent, List<T> logicalCollection) where T : DependencyObject
        {
            IEnumerable children = LogicalTreeHelper.GetChildren(parent);
            foreach (object child in children)
            {
                if (child is DependencyObject)
                {
                    DependencyObject? depChild = child as DependencyObject;
                    if (child is T)
                    {
                        logicalCollection.Add(child as T);
                    }
                    GetButtonNames(depChild, logicalCollection);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SearchString = TxtSearch.Text;
                bool Varify = int.TryParse(SearchString, out int Resulr);
                for (int i = 0; i < seatclass.Count; i++)
                {
                    if (Varify == false)
                    {
                        if (seatclass[i].customerName.ToLower() == SearchString.ToLower())
                        {

                            for (int j = 0; j < listButtons.Count; j++)
                            {
                                if (listButtons[j].Name.ToLower() == seatclass[i].SeatName.ToLower())
                                {
                                    listButtons[j].Content = seatclass[i].SeatNo;
                                    listButtons[j].Background = Brushes.GreenYellow;
                                    break;
                                }
                            }
                            seatclass.RemoveAt(i);
                        }
                    }
                    else
                    {
                        if (seatclass[i].SeatNo == Convert.ToInt32(SearchString))
                        {
                            for (int j = 0; j < listButtons.Count; j++)
                            {
                                if (listButtons[j].Content == seatclass[i].customerName)
                                {
                                    listButtons[j].Content = seatclass[i].SeatNo;
                                    listButtons[j].Background = Brushes.GreenYellow;
                                    seatclass.RemoveAt(i);
                                    break;

                                }


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            seatclass = ReaddatafromJson();
            DisplayAllCustomerName();

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < seatclass.Count; i++)
                {
                    for (int j = 0; j < listButtons.Count; j++)
                    {
                        if (listButtons[j].Content == seatclass[i].customerName)
                        {
                            listButtons[j].Content = seatclass[i].SeatNo;
                            listButtons[j].Background = Brushes.GreenYellow;
                            seatclass.RemoveAt(i);
                            break;
                        }

                    }
                }
                seatclass.Clear();
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Savebooking = JsonConvert.SerializeObject(seatclass);
                SaveJson(Savebooking);
                MessageBoxResult result = MessageBox.Show("Data saved successfully");
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
        }
        public List<SeatClass> ReaddatafromJson()
        {
            string ReadCustomerData = File.ReadAllText("Registration.json");
            return JsonConvert.DeserializeObject<List<SeatClass>>(ReadCustomerData)!;


        }
        public void SaveJson(string SaveBooking)
        {
            try
            {
                File.WriteAllText("Registration.json", SaveBooking);
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
        }
    }
}
