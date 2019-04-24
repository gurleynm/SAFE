using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using safe_web_app.Models;
using System.Web.Mvc;

namespace safe_web_app
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public CSE201Entities db;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter sda = new SqlDataAdapter();

        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.db = new CSE201Entities();
            var data = db.Applications.ToList();
            appname.Text = data[1].title;
            show();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string a;
            a = ConfigurationManager.ConnectionStrings ["Cona"].ToString ();
            SqlConnection con = new SqlConnection(a);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into comments" + "(name,com, time) values('Nathan',@com, @time)", con);
            cmd.Parameters.AddWithValue("@com", commentBox.Text);
            cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
            cmd.ExecuteNonQuery();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void show()
        {

            string a;
            a = ConfigurationManager.ConnectionStrings["Cona"].ToString();
            SqlConnection con = new SqlConnection(a);
            con.Open();
            cmd.CommandText = "select * from comments";
            cmd.Connection = con;

            sda.SelectCommand = cmd;
            sda.Fill(ds, "comments");
            Repeater1.DataSource = ds;
            Repeater1.DataBind();

        }
    }
}