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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Converter.xaml
    /// </summary>
    public partial class Converter : Window
    {
		string hours, days;
		int HoursInt, DaysInt, PreResult;

		private void GetResult(object sender, RoutedEventArgs e)
		{
			GetNumbers();
			Convert();
		}

		public Converter()
        {
            InitializeComponent();
        }

		public void GetNumbers()
		{
			hours = Hours.Text;
			days = Days.Text;
			try
			{
				HoursInt = Int32.Parse(hours);
				DaysInt = Int32.Parse(days);
			} 
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		public void Convert()
		{
			PreResult = (HoursInt * 3600) + (DaysInt * 86400);
			Result.Text = PreResult.ToString();
		}
    }
}
