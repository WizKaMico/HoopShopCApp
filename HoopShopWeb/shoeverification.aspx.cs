using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HoopShopWeb
{
    public partial class shoeverification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserEmail"] != null && Session["UID"] != null)
                {
                    string userEmail = Session["UserEmail"].ToString();
                    lblEmail.Text = "HI! " + userEmail.ToUpper();
                }

                if (!string.IsNullOrEmpty(Request.QueryString["shoe_id"]))
                {
                    string transactionId = Request.QueryString["shoe_id"];

                    // Your database connection string
                    string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    // SQL query to fetch details based on transactionId
                    string query = "SELECT *, TST.date_created AS dcreated " +
                                   "FROM tbl_shoe_transaction TST " +
                                   "LEFT JOIN tbl_shoe_verification_price TSVP ON TST.transoption = TSVP.tsoptionid " +
                                   "LEFT JOIN tbl_user_information TUI ON TST.uid = TUI.user_id " +
                                   "LEFT JOIN tbl_shoe_brands TSB ON TST.brandid = TSB.sid " +
                                   "LEFT JOIN tlb_expert_verification TEV ON TEV.transid = TST.shid " +
                                   "WHERE TST.shid = @TransactionId";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            // Add parameters to the SQL query
                            command.Parameters.AddWithValue("@TransactionId", transactionId);

                            try
                            {
                                connection.Open();
                                MySqlDataReader reader = command.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {

                                        txtShoeName.Text = reader["shoe_name"].ToString();
                                        txtShoeSize.Text = reader["shoe_size"].ToString();
                                        txtShoeCode.Text = reader["shoe_code"].ToString();
                                        txtShoeComment.Text = reader["shoe_comment"].ToString();
                                        drpFindings.SelectedValue = reader["result"].ToString();

                                        // Access the retrieved data here
                                        // For example:
                                        string shoeappearance = reader["shoeappearance"].ToString();
                                        string shoeinside = reader["shoeinside"].ToString();
                                        string shoeinsole = reader["shoeinsole"].ToString();
                                        string shoeinsolestitching = reader["shoeinsolestitching"].ToString();
                                        string shoebox = reader["shoebox"].ToString();
                                        string shoeboxdatecode = reader["shoeboxdatecode"].ToString();
                                        string transcode = reader["transcode"].ToString();
                                        lblJob.Text = "JOBID: " + transcode;

                                        string titleAppearance = "Shoe Appearance";
                                        string insideOfShoe = "Inside Of The Shoes";
                                        string shoeInsole = "Shoe Insole";
                                        string shoeInsoleStitching = "Shoe Insole Stitching";
                                        string shoeBox = "Shoe Box";
                                        string shoeBoxDateCode = "Shoe Box Date Code";

                                        // Generate HTML for the slideshow
                                        string slideshowHtml = "<div class='col-sm-12'>";
                                        slideshowHtml += "<div id='carouselExampleControls' class='carousel slide' data-ride='carousel'>";
                                        slideshowHtml += "<div class='carousel-inner'>";

                                        // Add each image to the carousel items
                                        slideshowHtml += "<div class='carousel-item active'>";
                                        slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeappearance + "' alt='Shoe Appearance' title='" + titleAppearance + "'>";
                                        slideshowHtml += "</div>";

                                        slideshowHtml += "<div class='carousel-item'>";
                                        slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeinside + "' alt='Shoe Inside' title='" + insideOfShoe + "'>";
                                        slideshowHtml += "</div>";

                                        slideshowHtml += "<div class='carousel-item'>";
                                        slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeinsole + "' alt='Shoe Inside' title='" + shoeInsole + "'>";
                                        slideshowHtml += "</div>";

                                        slideshowHtml += "<div class='carousel-item'>";
                                        slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeinsolestitching + "' alt='Shoe Inside' title='" + shoeInsoleStitching + "'>";
                                        slideshowHtml += "</div>";

                                        slideshowHtml += "<div class='carousel-item'>";
                                        slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoebox + "' alt='Shoe Inside' title='" + shoeBox + "'>";
                                        slideshowHtml += "</div>";

                                        slideshowHtml += "<div class='carousel-item'>";
                                        slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeboxdatecode + "' alt='Shoe Inside' title='" + shoeBoxDateCode + "'>";
                                        slideshowHtml += "</div>";

                                        // Add more images similarly for shoeinsole, shoeinsolestitching, shoebox, etc.

                                        slideshowHtml += "</div>";
                                        slideshowHtml += "<a class='carousel-control-prev' href='#carouselExampleControls' role='button' data-slide='prev'>";
                                        slideshowHtml += "<span class='carousel-control-prev-icon' aria-hidden='true'></span>";
                                        slideshowHtml += "<span class='sr-only'>Previous</span>";
                                        slideshowHtml += "</a>";
                                        slideshowHtml += "<a class='carousel-control-next' href='#carouselExampleControls' role='button' data-slide='next'>";
                                        slideshowHtml += "<span class='carousel-control-next-icon' aria-hidden='true'></span>";
                                        slideshowHtml += "<span class='sr-only'>Next</span>";
                                        slideshowHtml += "</a>";
                                        slideshowHtml += "</div>";
                                        slideshowHtml += "</div>";

                                        // Set the HTML content to the placeholder
                                        // Create a Literal control to hold the slideshow HTML
                                        Literal slideshowLiteral = new Literal();

                                        // Add the slideshow HTML content to the Literal control
                                        slideshowLiteral.Text = slideshowHtml;

                                        // Add the Literal control to the placeholder's Controls collection
                                        slideshowPlaceholder.Controls.Add(slideshowLiteral);

                                    }
                                }
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                // Handle exceptions or errors here
                            }
                        }
                    }

                }
            }

           
        }

        /* protected async Task proceed_Search(object sender, EventArgs e)
         {
             string upcNumber = txtUpcNumber.Text;

             if (!string.IsNullOrEmpty(Request.QueryString["shoe_id"]))
             {
                 string transactionId = Request.QueryString["shoe_id"];
                 if (!string.IsNullOrEmpty(upcNumber))
                 {
                     string apiUrl = $"https://api.barcodespider.com/v1/lookup?token=69b5047bbb6161c23fb2&upc={upcNumber}";

                     using (HttpClient client = new HttpClient())
                     {
                         try
                         {
                             HttpResponseMessage response = await client.GetAsync(apiUrl);
                             if (response.IsSuccessStatusCode)
                             {
                                 string apiResponse = await response.Content.ReadAsStringAsync();

                                 // Display the API response in lblApiResponse
                                 lblApiResponse.Text = apiResponse;
                             }
                             else
                             {
                                 lblApiResponse.Text = "Failed to fetch data from API.";
                             }
                         }
                         catch (Exception ex)
                         {
                             lblApiResponse.Text = "An error occurred: " + ex.Message;
                         }
                     }
                 }
                 else
                 {
                     lblApiResponse.Text = "Please provide a valid UPC number.";
                 }
                 Response.Redirect("shoeverification.aspx?shoe_id=" + Uri.EscapeDataString(transactionId) + "&message=success");

             }



         }*/

        protected void proceed_Search(object sender, EventArgs e)
        {
            string upcNumber = txtUpcNumber.Text;

            if (!string.IsNullOrEmpty(upcNumber))
            {
                // Create the API URL
                string apiUrl = $"https://api.barcodespider.com/v1/lookup?token=69b5047bbb6161c23fb2&upc={upcNumber}";

                // Perform the API request synchronously
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = client.GetAsync(apiUrl).Result; // Blocking call

                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = response.Content.ReadAsStringAsync().Result;
                            var result = JObject.Parse(apiResponse);

                            // Update the label with the API response
                            string imageSrc = result["item_attributes"]["image"].ToString();
                            string title = result["item_attributes"]["title"].ToString();
                            string brand = result["item_attributes"]["brand"].ToString();
                            string color = result["item_attributes"]["color"].ToString();
                            string size = result["item_attributes"]["size"].ToString();

                            // Display the image
                            productImage.Src = imageSrc;

                            // Display other details in labels
                            lblTitle.Text = title;
                            lblBrand.Text = brand;
                            lblColor.Text = color;
                            lblSize.Text = size;

                            // For stores data (if multiple stores are there)
                            var stores = result["Stores"];
                            if (stores != null && stores.Count() > 0)
                            {
                                string price = stores[0]["price"].ToString();
                                string currency = stores[0]["currency"].ToString();

                                lblPrice.Text = price;
                                lblCurrency.Text = currency;
                            }
                        }
                        else
                        {
                            lblApiResponse.Text = "Failed to fetch data from API.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblApiResponse.Text = "An error occurred: " + ex.Message;
                }
            }
            else
            {
                lblApiResponse.Text = "Please provide a valid UPC number.";
            }

            if (!string.IsNullOrEmpty(Request.QueryString["shoe_id"]))
            {
                string transactionId = Request.QueryString["shoe_id"];

                // Your database connection string
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                // SQL query to fetch details based on transactionId
                string query = "SELECT *, TST.date_created AS dcreated " +
                               "FROM tbl_shoe_transaction TST " +
                               "LEFT JOIN tbl_shoe_verification_price TSVP ON TST.transoption = TSVP.tsoptionid " +
                               "LEFT JOIN tbl_user_information TUI ON TST.uid = TUI.user_id " +
                               "LEFT JOIN tbl_shoe_brands TSB ON TST.brandid = TSB.sid " +
                               "WHERE TST.shid = @TransactionId";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the SQL query
                        command.Parameters.AddWithValue("@TransactionId", transactionId);

                        try
                        {
                            connection.Open();
                            MySqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    // Access the retrieved data here
                                    // For example:
                                    string shoeappearance = reader["shoeappearance"].ToString();
                                    string shoeinside = reader["shoeinside"].ToString();
                                    string shoeinsole = reader["shoeinsole"].ToString();
                                    string shoeinsolestitching = reader["shoeinsolestitching"].ToString();
                                    string shoebox = reader["shoebox"].ToString();
                                    string shoeboxdatecode = reader["shoeboxdatecode"].ToString();
                                    string transcode = reader["transcode"].ToString();
                                    lblJob.Text = "JOBID: " + transcode;

                                    string titleAppearance = "Shoe Appearance";
                                    string insideOfShoe = "Inside Of The Shoes";
                                    string shoeInsole = "Shoe Insole";
                                    string shoeInsoleStitching = "Shoe Insole Stitching";
                                    string shoeBox = "Shoe Box";
                                    string shoeBoxDateCode = "Shoe Box Date Code";

                                    // Generate HTML for the slideshow
                                    string slideshowHtml = "<div class='col-sm-12'>";
                                    slideshowHtml += "<div id='carouselExampleControls' class='carousel slide' data-ride='carousel'>";
                                    slideshowHtml += "<div class='carousel-inner'>";

                                    // Add each image to the carousel items
                                    slideshowHtml += "<div class='carousel-item active'>";
                                    slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeappearance + "' alt='Shoe Appearance' title='" + titleAppearance + "'>";
                                    slideshowHtml += "</div>";

                                    slideshowHtml += "<div class='carousel-item'>";
                                    slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeinside + "' alt='Shoe Inside' title='" + insideOfShoe + "'>";
                                    slideshowHtml += "</div>";

                                    slideshowHtml += "<div class='carousel-item'>";
                                    slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeinsole + "' alt='Shoe Inside' title='" + shoeInsole + "'>";
                                    slideshowHtml += "</div>";

                                    slideshowHtml += "<div class='carousel-item'>";
                                    slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeinsolestitching + "' alt='Shoe Inside' title='" + shoeInsoleStitching + "'>";
                                    slideshowHtml += "</div>";

                                    slideshowHtml += "<div class='carousel-item'>";
                                    slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoebox + "' alt='Shoe Inside' title='" + shoeBox + "'>";
                                    slideshowHtml += "</div>";

                                    slideshowHtml += "<div class='carousel-item'>";
                                    slideshowHtml += "<img class='d-block w-100' src='http://localhost/shoemarketcheckph/" + shoeboxdatecode + "' alt='Shoe Inside' title='" + shoeBoxDateCode + "'>";
                                    slideshowHtml += "</div>";

                                    // Add more images similarly for shoeinsole, shoeinsolestitching, shoebox, etc.

                                    slideshowHtml += "</div>";
                                    slideshowHtml += "<a class='carousel-control-prev' href='#carouselExampleControls' role='button' data-slide='prev'>";
                                    slideshowHtml += "<span class='carousel-control-prev-icon' aria-hidden='true'></span>";
                                    slideshowHtml += "<span class='sr-only'>Previous</span>";
                                    slideshowHtml += "</a>";
                                    slideshowHtml += "<a class='carousel-control-next' href='#carouselExampleControls' role='button' data-slide='next'>";
                                    slideshowHtml += "<span class='carousel-control-next-icon' aria-hidden='true'></span>";
                                    slideshowHtml += "<span class='sr-only'>Next</span>";
                                    slideshowHtml += "</a>";
                                    slideshowHtml += "</div>";
                                    slideshowHtml += "</div>";

                                    // Set the HTML content to the placeholder
                                    // Create a Literal control to hold the slideshow HTML
                                    Literal slideshowLiteral = new Literal();

                                    // Add the slideshow HTML content to the Literal control
                                    slideshowLiteral.Text = slideshowHtml;

                                    // Add the Literal control to the placeholder's Controls collection
                                    slideshowPlaceholder.Controls.Add(slideshowLiteral);

                                }
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            // Handle exceptions or errors here
                        }
                    }
                }
            }
        }




        /*    protected void proceed_Click(object sender, EventArgs e)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["shoe_id"]))
                {
                    string transactionId = Request.QueryString["shoe_id"];
                    string shoeName = txtShoeName.Text;
                    string shoeSize = txtShoeSize.Text;
                    string shoeCode = txtShoeCode.Text;
                    string shoeComment = txtShoeComment.Text;
                    string selectedFinding = drpFindings.SelectedValue;

                    string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        string query = "INSERT INTO tlb_expert_verification (transid, shoe_name, shoe_size, shoe_code, shoe_comment, result, date_verified) " +
                                        "VALUES (@TransId, @ShoeName, @ShoeSize, @ShoeCode, @ShoeComment, @SelectedFinding, CURDATE())";
                        MySqlCommand command = new MySqlCommand(query, connection);

                        // Use parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@TransId", transactionId);
                        command.Parameters.AddWithValue("@ShoeName", shoeName);
                        command.Parameters.AddWithValue("@ShoeSize", shoeSize);
                        command.Parameters.AddWithValue("@ShoeCode", shoeCode);
                        command.Parameters.AddWithValue("@ShoeComment", shoeComment);
                        command.Parameters.AddWithValue("@SelectedFinding", selectedFinding);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            // Handle exceptions (log the error, display an error message, etc.)
                            // For example:
                            // ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
                        }
                    }


                    // After processing, redirect the user to another page or display a success message
                    Response.Redirect("shoeverification.aspx?shoe_id=" + Uri.EscapeDataString(transactionId) + "&message=success");

                }

            }*/

        protected void proceed_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["shoe_id"]))
            {
                string transactionId = Request.QueryString["shoe_id"];
                string shoeName = txtShoeName.Text;
                string shoeSize = txtShoeSize.Text;
                string shoeCode = txtShoeCode.Text;
                string shoeComment = txtShoeComment.Text;
                string selectedFinding = drpFindings.SelectedValue;

                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the record with the given transactionId exists
                    string checkQuery = "SELECT COUNT(*) FROM tlb_expert_verification WHERE transid = @TransId";
                    MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@TransId", transactionId);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        // Update existing record
                        string updateQuery = "UPDATE tlb_expert_verification SET shoe_name = @ShoeName, " +
                            "shoe_size = @ShoeSize, shoe_code = @ShoeCode, shoe_comment = @ShoeComment, " +
                            "result = @SelectedFinding, date_verified = CURDATE() WHERE transid = @TransId";

                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@TransId", transactionId);
                        updateCommand.Parameters.AddWithValue("@ShoeName", shoeName);
                        updateCommand.Parameters.AddWithValue("@ShoeSize", shoeSize);
                        updateCommand.Parameters.AddWithValue("@ShoeCode", shoeCode);
                        updateCommand.Parameters.AddWithValue("@ShoeComment", shoeComment);
                        updateCommand.Parameters.AddWithValue("@SelectedFinding", selectedFinding);

                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        // Insert new record
                        string insertQuery = "INSERT INTO tlb_expert_verification (transid, shoe_name, shoe_size, " +
                            "shoe_code, shoe_comment, result, date_verified) VALUES (@TransId, @ShoeName, " +
                            "@ShoeSize, @ShoeCode, @ShoeComment, @SelectedFinding, CURDATE())";

                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@TransId", transactionId);
                        insertCommand.Parameters.AddWithValue("@ShoeName", shoeName);
                        insertCommand.Parameters.AddWithValue("@ShoeSize", shoeSize);
                        insertCommand.Parameters.AddWithValue("@ShoeCode", shoeCode);
                        insertCommand.Parameters.AddWithValue("@ShoeComment", shoeComment);
                        insertCommand.Parameters.AddWithValue("@SelectedFinding", selectedFinding);

                        insertCommand.ExecuteNonQuery();
                    }

                    // Update tbl_shoe_transaction status to 'COMPLETED'
                    string updateTransactionQuery = "UPDATE tbl_shoe_transaction SET status = 'COMPLETED',findings = @SelectedFinding  WHERE shid = @TransactionId";
                    MySqlCommand updateTransactionCommand = new MySqlCommand(updateTransactionQuery, connection);
                    updateTransactionCommand.Parameters.AddWithValue("@TransactionId", transactionId);
                    updateTransactionCommand.Parameters.AddWithValue("@SelectedFinding", selectedFinding);
                    updateTransactionCommand.ExecuteNonQuery();
                }

               

                // After processing, redirect the user to another page or display a success message

                string redirectUrl = "shoeverification.aspx?shoe_id=" + Uri.EscapeDataString(transactionId) + "&message=success";

                // Add script to show SweetAlert on redirect
                string script = "<script>Swal.fire('Success!', 'Record updated/inserted successfully!', 'success').then(function() { window.location.href = '" + redirectUrl + "'; });</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowSweetAlert", script);
            }
        }



    }
}
