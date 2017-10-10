using System.Windows;

namespace Kelvius.Shell
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = new MainViewModel();
		}

		private void celsiusTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			// Quick and dirty way to change viewmodel property
			(this.DataContext as MainViewModel).MasterUnit = TemperatureUnit.Celsius;
		}

		private void kelvinTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			// Quick and dirty way to change viewmodel property
			(this.DataContext as MainViewModel).MasterUnit = TemperatureUnit.Kelvin;
		}

		private void fahrenheitTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			// Quick and dirty way to change viewmodel property
			(this.DataContext as MainViewModel).MasterUnit = TemperatureUnit.Fahrenheit;
		}
	}
}
