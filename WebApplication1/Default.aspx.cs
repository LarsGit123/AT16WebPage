using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Connected_Services;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        public bool IsFirstLoad { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {                        
            At16HostController myController = new At16HostController();
            myController.CreateBlobContainer("blobcontainer");
            myController.CreateBlobContainer("at16messagecontainer");

            List<string> KvikneCamblobs = myController.GetListBlobNames("blobcontainer");
            List<string> at16TempHum = myController.GetListBlobMessage("at16messagecontainer");
            Dictionary<string, string> tempDictionary = myController.GetMQTTTopicValue("temperature", at16TempHum);
            Dictionary<string, string> humDictionary = myController.GetMQTTTopicValue("humidity", at16TempHum);
            At16tempLabel.Text = tempDictionary.Last().Value;
            At16humLabel.Text = humDictionary.Last().Value;
            At16timeLabel.Text = tempDictionary.Last().Key;

            foreach (string blobname in KvikneCamblobs)
            {
                if (blobname.Substring(0, 6) == "webcam")
                {               
                    ListItem newitem = new ListItem(blobname, "https://at16hoststorage.blob.core.windows.net/blobcontainer/" + blobname);

                    if (!GreetList.Items.Contains(newitem))
                    {
                        GreetList.Items.Add(new ListItem(blobname, "https://at16hoststorage.blob.core.windows.net/blobcontainer/" + blobname));
                    }
                }
            }

            if (IsFirstLoad)
            {
                

                AreaRegistration.RegisterAllAreas();
                RouteConfig2.RegisterRoutes(RouteTable.Routes);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                BundleConfig2.RegisterBundles(BundleTable.Bundles);
                IsFirstLoad = true;
            }
            
        }

        
        protected void GreetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            img.ImageUrl = GreetList.SelectedValue;
        }
    }
}