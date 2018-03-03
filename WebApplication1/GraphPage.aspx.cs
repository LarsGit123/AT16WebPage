using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class GraphPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Chart1.DataSource = GetData();
            Chart1.Series["Series1"].XValueMember = "Timer";
            Chart1.Series["Series1"].YValueMembers = "GarasjeTemp";
            //Chart1.Series["Series2"].XValueMember = "Years";
            //Chart1.Series["Series2"].YValueMembers = "Sales2";
            Chart1.DataBind();

            //The value will be displayed near marker style
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            //Chart1.Series["Series2"].IsValueShownAsLabel = false;

            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            Chart1.Series["Series1"].BorderWidth = 3;
            //Chart1.Series["Series2"].BorderWidth = 3;
            Chart1.Series["Series1"].MarkerStyle = MarkerStyle.None;
            //Chart1.Series["Series2"].MarkerStyle = MarkerStyle.Triangle;
        }
        private DataTable GetData()
        {
            DataTable dt = new DataTable("Analysis");
            dt.Columns.Add(new DataColumn("GarasjeTemp"));
            //dt.Columns.Add(new DataColumn("Sales2"));
            dt.Columns.Add(new DataColumn("Timer"));
            //dt.Rows.Add(1400, 1600, 2008);
            //dt.Rows.Add(400, 1300, 2009);
            //dt.Rows.Add(2050, 1200, 2010);
            //dt.Rows.Add(1500, 1900, 2012);
           //if (dataClass.TempList.Count == dataClass.HourList.Count)
            {
                for (int index = 0; index < dataClass.TempList.Count; index++)
                {
                    dt.Rows.Add(dataClass.TempList[index], index);
                }
            }
            
            return dt;
        }

        protected void ReturnToMainPage_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("/Default.aspx");
        }
    }
}