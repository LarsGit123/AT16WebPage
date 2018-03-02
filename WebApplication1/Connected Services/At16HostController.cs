using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Connected_Services
{
    public class At16HostController : Controller
    {
       

        // GET: At16Host
        public ActionResult Index()
        {
            return View();
        }

        private CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("at16hoststorage_AzureStorageConnectionString")); 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            return container;
        }
        public ActionResult CreateBlobContainer(string containerName)
        {
            // The code in this section goes here.
            CloudBlobContainer container = GetCloudBlobContainer(containerName);
            ViewBag.Success = container.CreateIfNotExists();
            ViewBag.BlobContainerName = container.Name;
            return View();
        }

        public List<string> GetListBlobNames(string containerName) {
            var bloblist=GetListBlob(containerName);
            List<string> BlobNameList = new List<string>();
            foreach (var blob in bloblist)
            {
                BlobNameList.Add(blob.Name);
            }
            return BlobNameList;
        }

        public List<string> GetListBlobMessage (string containerName)
        {
            
            List<string> BlobMessages = new List<string>();
            var bloblist=GetListBlob(containerName);

            foreach(var blob in bloblist)
            {                
                using(var stream = blob.OpenRead())
                {
                    using(StreamReader reader= new StreamReader(stream))
                    {
                        BlobMessages.Add(reader.ReadToEnd());                        
                    }
                }                
            }
            return BlobMessages;
        }

        
        private List<CloudBlockBlob> GetListBlob(string containerName)
        {
            List<CloudBlockBlob> blobs = new List<CloudBlockBlob>();
            CloudBlobContainer container = GetCloudBlobContainer(containerName);
            foreach (IListBlobItem item in container.ListBlobs())
            {
                if (item is CloudBlockBlob)
                {                    
                    blobs.Add((CloudBlockBlob)item);
                }
                else if (item is CloudPageBlob)
                {
                    //skip
                }
                else if(item is CloudBlobDirectory)
                {
                    List<IListBlobItem> bloblist = new List<IListBlobItem>();
                    RecursiveSearchThroughDirectory((CloudBlobDirectory)item, bloblist);
                    
                    foreach(var myblob in bloblist)
                    {
                        if(myblob is CloudBlockBlob)
                        {
                            blobs.Add(((CloudBlockBlob)myblob));
                        }
                    }
                }
            }
            
            return blobs;
            // The code in this section goes here.

        }
        private void RecursiveSearchThroughDirectory(CloudBlobDirectory dir, List<IListBlobItem> Blobs )
        {                        
            IEnumerable<IListBlobItem> subItems = dir.ListBlobs();
            foreach(var subitem in subItems)
            {
                if (subitem is CloudBlobDirectory)
                    RecursiveSearchThroughDirectory((CloudBlobDirectory)subitem, Blobs);
                else if (subitem is CloudBlockBlob || subitem is CloudPageBlob || subitem is CloudBlobStream)
                    Blobs.Add(subitem);
            }                                    
        }
        public Dictionary<string, string> GetMQTTTopicValue(string topicname, List<string> blobmessages)
        {            
            Dictionary<string, string> topicvalues = new Dictionary<string, string>();
            string val;
            string time;
            int indexOfValue;

            foreach (string message in blobmessages)
            {
                if (message.Contains(topicname))
                {
                    indexOfValue = message.LastIndexOf(topicname);
                    val = message.Substring(indexOfValue + topicname.Length+3, 5);
                }
                else
                    val = "No Value";

                if (message.Contains("enqueuedTime8"))
                {
                    indexOfValue = message.LastIndexOf("enqueuedTime8");
                    time = message.Substring(indexOfValue + 13, 19);
                }
                else
                    time = "";

                string dummystring = "";
                if (!topicvalues.TryGetValue(time, out dummystring))
                    topicvalues.Add(time, val);                
            }            
                return topicvalues;

           
        }
        public static TimeSpan ConvertStringToTimeSpan(string datetime)
        {
            string yy_mm_dd = datetime.Substring(0, 10);
            string hh_mm_ss = datetime.Substring(11, 8);
            DateTime yyyy_mm_dd;
            DateTime dthh_mm_ss;
            bool canparse;
            canparse = DateTime.TryParse(datetime, out yyyy_mm_dd);

            if (canparse)
                return DateTime.Now - (yyyy_mm_dd);
            else
                return TimeSpan.Zero;
        }
    }
}