using ImageMeasurement.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ImageMeasurement.View
{
	/// <summary>
	/// WImageMeasurementView.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class WImageMeasurementView : Window
	{
		#region => Constructor

		public WImageMeasurementView()
		{
			InitializeComponent();
			DataContext = App.Current.Services.GetService<WImageMeasurementViewModel>();
		}

		#endregion => Constructor
	}
}
