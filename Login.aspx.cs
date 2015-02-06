using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
    SqlDataReader rd=null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string id = txtid.Text;
            string pwd = txpwd.Text;
            int flag = 0;
            string str = "select * from logint";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                if (rd.HasRows)
                {
                    if (id.Equals(rd[0]) && pwd.Equals(rd[1]))
                    {
                        Session["id"] = Session.SessionID;
                        Session["user"] = id;
                        if (rd[2].Equals("a"))
                        {
                            flag++;
                            Response.Redirect("Admin.aspx");

                        }
                        else if (rd[2].Equals("f"))
                        {
                            flag++;
                            Response.Redirect("Faculty.aspx");

                        }
                        else
                        {
                            flag++;
                            Response.Redirect("Student.aspx");

                        }

                    }

                }
                if (flag == 0)
                {
                    lblmsg.Text = "Invalid urseid or password";
                }

            }

        }
        catch (SqlException ex)
        {
            lblmsg.Text = "Error";
        }
        finally
        {
            if (rd != null)
            {
                rd.Close();
            }
            con.Close();
        
        }
    }
}