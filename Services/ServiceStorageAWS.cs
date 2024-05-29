using Amazon.S3;
using Amazon.S3.Model;
using MvcPeliculasApiAWS.Models;

namespace MvcPeliculasApiAWS.Services
{
    public class ServiceStorageAWS
    {
        private IAmazonS3 client;
        private string BucketName;

/*        public ServiceStorageAWS
            (IConfiguration configuration, IAmazonS3 client)
        {
            this.BucketName =
                configuration.GetValue<string>("AWS:S3BucketName");
            this.client = client;
        }     */   
        public ServiceStorageAWS
            (KeysModel keys, IAmazonS3 client)
        {
            this.BucketName =
                keys.BucketName;
            this.client = client;
        }

        public async Task<bool>
            UploadFileAsync(string fileName, Stream stream)
        {
            PutObjectRequest request =
                new PutObjectRequest
                {
                    InputStream = stream,
                    Key = fileName,
                    BucketName = this.BucketName
                };
            PutObjectResponse response = 
                await this.client.PutObjectAsync(request);
            if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
