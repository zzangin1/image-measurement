using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageMeasurement.Model;
using Microsoft.Extensions.DependencyInjection;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.Collections.ObjectModel;

namespace ImageMeasurement.ViewModel
{
	public partial class UCImageListViewModel : ObservableObject
	{
		#region => Field

		private Mat _originalImageMat;
		private UCImageViewModel _ucImageViewModel = App.Current.Services.GetService<UCImageViewModel>();

		[ObservableProperty]
		private ImageFileModel _selectedImageFile;

		#endregion => Field

		#region => Property

		public ObservableCollection<ImageFileModel> ImageFiles { get; set; }

		#endregion => Property

		#region => Constructor

		public UCImageListViewModel()
		{
			ImageFiles = new ObservableCollection<ImageFileModel>();
		}

		#endregion => Constructor

		#region => Method

		/// <summary>
		/// DropFileCommand 동작 메서드
		/// </summary>
		/// <param name="recvFilePaths"></param>
		[RelayCommand]
		private void DropFile(object recvFilePaths)
		{
			if (recvFilePaths is string[] filePaths)
			{
				foreach (var filePath in filePaths)
				{
					ImageFiles.Add(
					new ImageFileModel
					{
						Index = ImageFiles.Count + 1,
						FilePath = filePath,
					});
				}
			}
		}

		partial void OnSelectedImageFileChanged(ImageFileModel value)
		{
			_originalImageMat = new Mat(value.FilePath);
			_ucImageViewModel.DisplayImage = _originalImageMat.Clone().ToBitmapSource();
		}

		#endregion => Method
	}
}
