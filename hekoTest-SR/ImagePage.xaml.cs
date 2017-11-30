using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.Media;
using Xamarin.Forms;

namespace hekoTestSR
{
    public partial class ImagePage : ContentPage
    {

        private readonly VisionServiceClient visionClient;

        public ImagePage()
        {
            InitializeComponent();
            this.visionClient = new VisionServiceClient("9e03011d348c4089a72353becd3516f3","https://westcentralus.api.cognitive.microsoft.com/vision/v1.0");
        }

        async void OnPickImage(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        async void OnTakeImage(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        async Task OnUploadImageAsync(object Sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No upload", "Picking a photo is not supported.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;
            image.Source = ImageSource.FromStream(() => file.GetStream());
            var analysisResult = await AnalyzePicture(file.GetStream());
            imageDescription.Text = analysisResult.Description.Captions[0].Text;
        }

        private async Task<AnalysisResult> AnalyzePicture(Stream imageStream)
        {
            VisualFeature[] visualFeatures = new VisualFeature[] { 
                VisualFeature.Adult,
                VisualFeature.Categories, 
                VisualFeature.Color, 
                VisualFeature.Description,
                VisualFeature.Faces, 
                VisualFeature.ImageType, 
                VisualFeature.Tags };
            AnalysisResult analysisResult =
                await visionClient.AnalyzeImageAsync(imageStream,
              visualFeatures);
            return analysisResult;
        }
    }
}
