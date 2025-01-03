﻿using ImageMeasurement.ViewModel;
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
		private Point _previousMousePosition;
		private UCImageListViewModel _ucImageListViewModel;

		#endregion => Field

		#region => Property
		#endregion => Property

		#region => Constructor

		public UCImageView()
		{
			InitializeComponent();
			DataContext = App.Current.Services.GetService<UCImageViewModel>();
			_ucImageListViewModel = App.Current.Services.GetService<UCImageListViewModel>();
			_ucImageListViewModel.ResetTransform = ResetTransform;
		}

		#endregion => Constructor

		#region => Method

		private void Canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			_isDragging = true;
			_clickCanvasPosition = e.GetPosition(this);
			MoveCanvas.CaptureMouse();

			MousePositionX.Content = (int)_previousMousePosition.X;
			MousePositionY.Content = (int)_previousMousePosition.Y;
		}

		private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (_isDragging)
			{
				var currentCanvasPosition = e.GetPosition(this);

				double offsetX = currentCanvasPosition.X - _clickCanvasPosition.X;
				double offsetY = currentCanvasPosition.Y - _clickCanvasPosition.Y;

				CanvasTranslateTransform.X += offsetX;
				CanvasTranslateTransform.Y += offsetY;

				_clickCanvasPosition = currentCanvasPosition;
			}
		}

		private void Canvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			_isDragging = false;
			MoveCanvas.ReleaseMouseCapture();
		}

		private void MoveCanvas_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
		{
			Point mousePosition = e.GetPosition(MoveCanvas);

			double zoomFactor = e.Delta > 0 ? 1.1 : 0.9;

			// 확대/축소할 위치 계산
			double absoluteX = mousePosition.X * ImageScaleTransform.ScaleX + CanvasTranslateTransform.X;
			double absoluteY = mousePosition.Y * ImageScaleTransform.ScaleY + CanvasTranslateTransform.Y;

			ImageScaleTransform.ScaleX *= zoomFactor;
			ImageScaleTransform.ScaleY *= zoomFactor;

			// 새로운 확대/축소 중심 계산
			double newAbsoluteX = mousePosition.X * ImageScaleTransform.ScaleX + CanvasTranslateTransform.X;
			double newAbsoluteY = mousePosition.Y * ImageScaleTransform.ScaleY + CanvasTranslateTransform.Y;

			CanvasTranslateTransform.X -= (newAbsoluteX - absoluteX);
			CanvasTranslateTransform.Y -= (newAbsoluteY - absoluteY);
		}

		private void DisplayImage_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			Point mousePoint = e.GetPosition(DisplayImage);

			MousePositionX.Content = (int)mousePoint.X;
			MousePositionY.Content = (int)mousePoint.Y;

			_previousMousePosition = mousePoint;
		}

		private void DisplayImage_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			MousePositionX.Content = string.Empty;
			MousePositionY.Content = string.Empty;
		}

		private void ResetTransform()
		{
			CanvasTranslateTransform.X = 0;
			CanvasTranslateTransform.Y = 0;
			ImageScaleTransform.ScaleX = 1.0;
			ImageScaleTransform.ScaleY = 1.0;
		}

		#endregion => Method
	}
}
