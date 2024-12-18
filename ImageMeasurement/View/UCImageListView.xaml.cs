using ImageMeasurement.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace ImageMeasurement.View
{
	/// <summary>
	/// UCImageListView.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class UCImageListView : UserControl
	{
		#region => Constructor

		public UCImageListView()
		{
			InitializeComponent();
			DataContext = App.Current.Services.GetService<UCImageListViewModel>();
		}

		#endregion => Constructor

		#region => Method

		private void DataGrid_Drop(object sender, System.Windows.DragEventArgs e)
		{
			if (DataContext is UCImageListViewModel)
			{
				var vm = (UCImageListViewModel)DataContext;

				if (e.Data.GetData(DataFormats.FileDrop) is string[] filePaths)
				{
					vm.DropFileCommand.Execute(filePaths);
				}
			}
		}

		#endregion => Method
	}
}
