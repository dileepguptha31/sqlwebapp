using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

namespace DileepBlobApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=dileepstorageac;AccountKey=zihZrIwU9D9g1Qi5sBXgKVYOTaZdt55NoJjscVXQ4W8qWjDPMtgMzo1FeEU3nObJTth5sdA7JxR6+ASt6x6Npg==;EndpointSuffix=core.windows.net";
            string containerName = "data1";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //Console.WriteLine("Creating the container");

            //blobServiceClient.CreateBlobContainer(containerName);

            /*
            If you want to specify properties for the container

           blobServiceClient.CreateBlobContainerAsync(containerName,Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            */
            //Console.WriteLine("Container creation complete");

            string blobName = "script.sql";
            string filePath = "D:\\UserData\\z003f28n\\AzureLearning\\script.sql";
            BlobContainerClient blobServiceClient1 = new BlobContainerClient(connectionString, containerName);

            BlobClient blobClient = blobServiceClient1.GetBlobClient(blobName);
            blobClient.UploadAsync(filePath, true);

            //Console.WriteLine("Uploaded the blob");

            Console.WriteLine("List the blobs");
            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);

            foreach (BlobItem blobItem in blobContainerClient.GetBlobs())
            {
                Console.WriteLine("The Blob Name is {0}", blobItem.Name);
                Console.WriteLine("The Blob Size is {0}", blobItem.Properties.ContentLength);
            }

            Console.WriteLine("Download blobs");
            //BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);

            blobClient.DownloadToAsync(filePath);

            Console.WriteLine("The blob is downloaded");


            Console.ReadKey();
        }
    }
}