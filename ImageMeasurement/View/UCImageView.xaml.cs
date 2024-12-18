using ImageMeasurement.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace ImageMeasurement.View
{
	/// <summary>
	/// UCImageView.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class UCImageView : UserControl
	{
		#region => Constructor

		public UCImageView()
		{
			InitializeComponent();
			DataContext = App.Current.Services.GetService<UCImageViewModel>();
		}

		#endregion => Constructor
	}
}
