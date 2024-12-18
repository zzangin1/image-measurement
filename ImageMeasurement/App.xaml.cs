using ImageMeasurement.View;
using ImageMeasurement.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ImageMeasurement
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		#region => Property

		public IServiceProvider Services { get; }
		public new static App Current => (App)Application.Current;

		#endregion => Property

		#region => Constructor

		public App()
		{
			Services = ConfigureServices();
		}

		#endregion => Constructor

		#region => Method

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			var startView = App.Current.Services.GetService<WImageMeasurementView>();
			startView.Show();
		}

		private static IServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();

			// 뷰
			services.AddSingleton<WImageMeasurementView>();

			// 뷰모델
			services.AddSingleton<WImageMeasurementViewModel>();

			return services.BuildServiceProvider();
		}

		#endregion => Method
	}

}
