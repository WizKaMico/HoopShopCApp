using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace HoopShopWeb
{
    public partial class security : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Ensure that this code runs only on the initial page load
            {
                if (Session["UserEmail"] != null && Session["UID"] != null)
                {
                    string userEmail = Session["UserEmail"].ToString();
                    lblEmail.Text = "HI! " + userEmail.ToUpper();
                }
            }
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            string userEmail = Session["UserEmail"]?.ToString();
            string UID = Session["UID"]?.ToString();
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var status = "UNUSED";
            int enteredCode = int.Parse(txtCode.Text.Trim());
            string hashedCode = GetMD5Hash((enteredCode.ToString()));

            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string validateSecurity = "SELECT * FROM tbl_user_security WHERE email = '" + userEmail + "' AND code = '" + hashedCode + "' AND status = '" + status + "' AND date_created = '" + date + "' ";
                MySqlCommand cmd = new MySqlCommand(validateSecurity, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int i = cmd.ExecuteNonQuery();
                if (dt.Rows.Count > 0)
                {
                    var stat = "USED";
                    string update_the_code = "UPDATE tbl_user_security SET status = '" + stat + "' WHERE code = '" + hashedCode + "'";
                    MySqlCommand ucode = new MySqlCommand(update_the_code, connection);
                    int rowsUpdated = ucode.ExecuteNonQuery(); // ExecuteNonQuery instead of ExecuteReader


                    Response.Redirect("home.aspx");
  
                }
            }
        }

        private string GetMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}