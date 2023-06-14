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
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Message> messages { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            string mesdes = File.ReadAllText("C:\\Users\\Elgun\\Source\\Repos\\Whatsapp\\WpfApp1\\messages.json");
            messages = new List<Message>(JsonConvert.DeserializeObject<List<Message>>(mesdes));
            Whatsapp.ItemsSource = messages;
        }

        private void DataTemplate_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrEmpty(MessageBar.Text)))
            {
                DateTime time = DateTime.Now;
                Whatsapp.ItemsSource = null;
                messages.Add(new Message() { message = MessageBar.Text, senttime = time.TimeOfDay });
                Whatsapp.ItemsSource = messages;
                MessageBar.Clear();
                string messer = JsonConvert.SerializeObject(messages);
                File.WriteAllText("C:\\Users\\Elgun\\Source\\Repos\\Whatsapp\\WpfApp1\\messages.json", messer);
            }
            else 
            {
                MessageBox.Show("Message bar is empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }

    public class Message
    {
        public string message { get; set; }
        public TimeSpan senttime { get; set; }
    }
}
