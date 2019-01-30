using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace predictcheque
{
    static class Class1
    {

        static void Main()
        {

            string imageFilePath = @"D:\as.jpg";

            //Task<string> task = MakePredictionRequest(imageFilePath);
            Task<string> task = MakePredictionRequest(imageFilePath, 1);

            

            var result = task.Result ;

            Console.WriteLine(result);

            Thread.Sleep(100000);
        }

       static private async Task<string> MakePredictionRequest(string filepath, int x)
        {
            string ret = await MakePredictionRequest(filepath);
            return ret;
        }



        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        static async Task<string>  MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Prediction-Key", "226f12ea329149718db1847640d07a04");

            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/a536dc35-0205-47ef-a66d-8ee6b12e2491/image";

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(url, content);

                string contentSt;

                contentSt = await response.Content.ReadAsStringAsync();

                var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(contentSt);

                var tagname = oMycustomclassname.predictions[0].tagName;

                return tagname;






            }

          
        }

    }
}
