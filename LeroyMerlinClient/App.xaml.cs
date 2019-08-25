using System.Windows;

namespace LeroyMerlinClient
{
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Win.mainWindow = new MainWindow();
			Win.OpenSettings();
			Win.OpenThisBase();
			try
			{
				new StartInfo().Show();
			}
			catch { }
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{
			try
			{
				Win.SaveThisBase();
				Win.SaveSettings();
				Win.mainWindow.Updater.Abort();
			}
			catch
			{ }
		}
	}
}