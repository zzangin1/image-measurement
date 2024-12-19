using ImageMeasurement.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace ImageMeasurement.View
{
	/// <summary>
	/// UCImageView.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class UCImageView : UserControl
	{
		#region => Field

		private bool _isDragging = false;
		private Point _clickCanvasPosition;

		#endregion => Field

		#region => Constructor

		public UCImageView()
		{
			InitializeComponent();
			DataContext = App.Current.Services.GetService<UCImageViewModel>();
		}

		#endregion => Constructor

		#region => Method

		private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			_isDragging = true;
			_clickCanvasPosition = e.GetPosition(this);
			MoveCanvas.CaptureMouse();
		}

		private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (_isDragging)
			{
				var currentCanvasPosition = e.GetPosition(this);

				double offsetX = currentCanvasPosition.X - _clickCanvasPosition.X;
				double offsetY = currentCanvasPosition.Y - _clickCanvasPosition.Y;

				CanvasTransform.X += offsetX;
				CanvasTransform.Y += offsetY;

				_clickCanvasPosition = currentCanvasPosition;
			}
		}

		private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			_isDragging = false;
			MoveCanvas.ReleaseMouseCapture();
		}

		#endregion => Method


	}
}
