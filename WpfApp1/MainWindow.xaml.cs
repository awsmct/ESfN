using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfApp1
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		CancellationTokenSource token = new CancellationTokenSource();
		const string command = "rundll32 powrprof.dll,SetSuspendState 0,1,0";
		public string texttime;
		public int usertime = 0;
		public bool check = true;

		public MainWindow()
		{
			InitializeComponent();
			PreStart prestart = new PreStart();
			prestart.ShowDialog();
		}

		public void InitStart()
		{
			texttime = Time.Text;
			try
			{
				usertime = Int32.Parse(texttime);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error. Check log in next message.");
				MessageBox.Show(ex.ToString());
				check = false;
			}
			return;
		}

		public void StartAsync()
		{
			if (token.IsCancellationRequested) return;
			Process cmd = new Process();
			cmd.StartInfo.FileName = "cmd.exe";
			cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			cmd.StartInfo.RedirectStandardInput = true;
			cmd.StartInfo.UseShellExecute = false;
			cmd.Start();
			cmd.StandardInput.WriteLine(command);
			Thread.Sleep(500);
			cmd.Kill();
		}

		private async void Start_Click(object sender, RoutedEventArgs e)
		{
			InitStart();
			if (!check) return;
			token = new CancellationTokenSource();
			Start.IsEnabled = false;
			try
			{
				if (token.IsCancellationRequested) return;
				await Task.Run(() => StartAsync());
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error:" + ex.ToString());
			}
		}

		private void Stop_Click(object sender, RoutedEventArgs e)
		{
			token.Cancel();
			Time.Clear();
			Start.IsEnabled = true;
		}

		private void OneH_Click(object sender, RoutedEventArgs e)
		{
			Time.Text = "3600";
		}

		private void TwoH_Click(object sender, RoutedEventArgs e)
		{
			Time.Text = "7200";
		}

		private void ThreeH_Click(object sender, RoutedEventArgs e)
		{
			Time.Text = "10800";
		}
	}
}
