using System;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
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
		}

		public async void StartTimer()
		{
			await Task.Run(() => Thread.Sleep((usertime * 60) * 1000));
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
				await Task.Run(() => StartTimer());
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
			Time.Text = "60";
		}

		private void TwoH_Click(object sender, RoutedEventArgs e)
		{
			Time.Text = "120";
		}

		private void ThreeH_Click(object sender, RoutedEventArgs e)
		{
			Time.Text = "180";
		}

		private void OpenCMD(object sender, RoutedEventArgs e)
		{
			Process cmd1 = new Process();
			cmd1.StartInfo.FileName = "cmd.exe";
			cmd1.StartInfo.Verb = "runas";
			cmd1.Start();
		}

		private void ExitClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void CopyClbd(object sender, RoutedEventArgs e)
		{
			Clipboard.SetText("powercfg /h off");
		}

		private void OpenAbout(object sender, RoutedEventArgs e)
		{
			About OpenAbt = new About();
			OpenAbt.Show();
		}

		private void ConverterOpen(object sender, RoutedEventArgs e)
		{
			Converter OpenConv = new Converter();
			OpenConv.Show();
		}
	}
}


