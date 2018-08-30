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
using System.Diagnostics;
using System.Data;

namespace TastkManager
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class DataObject
        {
            public String NOMBRE { get; set; }
            public int ID { get; set; }
            public bool STATUS { get; set; }
            public String MEMORIA { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.dataGrid1.FontSize = 24;

            var list = new ObservableCollection<DataObject>();

            Process[] processList = Process.GetProcesses();

            foreach (Process process in processList)
            {

                list.Add(new DataObject() {
                    NOMBRE = process.ProcessName,
                    ID = process.Id,
                    STATUS = process.Responding,
                    MEMORIA = parceBytes(process.PrivateMemorySize64) });
            }


            this.dataGrid1.ItemsSource = list;
        }
        public string parceBytes(long number)
        {
            List<string> suffixes = new List<string> { " B", " KB", " MB", " GB", " TB", " PB" };

            for (int i = 0; i < suffixes.Count; i++)
            {
                long temp = number / (int)Math.Pow(1024, i + 1);

                if (temp == 0)
                {
                    return (number / (int)Math.Pow(1024, i)) + suffixes[i];
                }
            }

            return number.ToString();
        }

        void ShowHideDetails(object sender, RoutedEventArgs e)
        {
            DataObject row = (DataObject)((Button)e.Source).DataContext;
     
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList)
            {
                if (process.Id == row.ID) {
                    process.Kill();
                    break;
                }
            }
        }
    }
}
