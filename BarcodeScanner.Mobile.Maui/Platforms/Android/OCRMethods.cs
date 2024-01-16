using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.Graphics;
using Android.Util;
using Xamarin.Google.MLKit.Vision.Common;
using Xamarin.Google.MLKit.Vision.Text;

namespace BarcodeScanner.Mobile
{
    public class OCRMethods
    {

        public class OnSuccessListener : Java.Lang.Object, IOnSuccessListener
        {
            public void OnSuccess(Java.Lang.Object result)
            {

            }
        }

        public class OnFailureListener : Java.Lang.Object, IOnFailureListener
        {
            public void OnFailure(Java.Lang.Exception e)
            {
                Log.Debug(nameof(BarcodeAnalyzer), e.ToString());
            }
        }

        public static OCRResult ProcessOCRResult(Java.Lang.Object result)
        {
            var textResult = (Xamarin.Google.MLKit.Vision.Text.Text)result;

            var ocrResult = new OCRResult { AllText = textResult.GetText() };

            foreach (var block in textResult.TextBlocks)
            {
                var ocrBlock = new Block(block.Text, ToMauiPoints(block.GetCornerPoints()), []);
                foreach (var line in block.Lines)
                {
                    var ocrLine = new Line(line.Text, line.Confidence, ToMauiPoints(line.GetCornerPoints()), line.Angle, []);
                    foreach (var element in line.Elements)
                    {
                        var ocrElement = new Element(element.Text, element.Confidence, ToMauiPoints(element.GetCornerPoints()), element.Angle);
                        ocrLine.Elements.Add(ocrElement);
                    }
                    ocrBlock.Lines.Add(ocrLine);
                }
                ocrResult.Blocks.Add(ocrBlock);
            }
            ocrResult.Success = true;
            return ocrResult;
        }

        private static List<Microsoft.Maui.Graphics.Point> ToMauiPoints(Android.Graphics.Point[] androidPoints)
        {
            var mauiPoints = new List<Microsoft.Maui.Graphics.Point>();
            foreach (var androidPoint in androidPoints)
                mauiPoints.Add(new(androidPoint.X, androidPoint.Y));

            return mauiPoints;
        }

        public static async Task<OCRResult> ScanFromImage(byte[] imageArray)
        {
            using Bitmap bitmap = await BitmapFactory.DecodeByteArrayAsync(imageArray, 0, imageArray.Length);
            if (bitmap == null)
                return null;
            using var image = InputImage.FromBitmap(bitmap, 0);

            using (var textScanner = TextRecognition.GetClient(Xamarin.Google.MLKit.Vision.Text.Latin.TextRecognizerOptions.DefaultOptions))
            {
                return OCRMethods.ProcessOCRResult(await textScanner.Process(image).AddOnSuccessListener(new OnSuccessListener()).AddOnFailureListener(new OnFailureListener()));
            }

        }

    }

}
