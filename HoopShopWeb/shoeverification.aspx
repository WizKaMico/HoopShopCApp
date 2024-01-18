<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shoeverification.aspx.cs" Inherits="HoopShopWeb.shoeverification" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="./main/home-style.css">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/5.0.0-alpha1/css/bootstrap.min.css" integrity="sha384-r4NyP46KrjDleawBgD5tp8Y7UzmLA05oM1iAEQ17CSuDqnUK2+k9luXQOfXJCJ4I" crossorigin="anonymous">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
    <body>
    
    <nav class="navbar navbar-dark fixed-top  flex-md-nowrap p-0 shadow" style="background-color:#6610f2;">
        <img src="./main/logo/hoop-logo-white.png" class="navbar-brand col-sm-3 col-md-2 mr-0"/>
        <input type="text" class="form-control form-control-primary w-100" placeholder="Search...">
        <ul class="navbar-nav px-3">
            <li class="nav-item text-nowrap">
                <a class="nav-link" href="#">Logout</a>
            </li>
        </ul>
    </nav>
    <div class="container-fluid">
        <div class="row">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light d-none d-md-block sidebar">
                <div class="left-sidebar">
                    <ul class="nav flex-column sidebar-nav" style="margin-top:50px;">
                        <li class="nav-item">
                            <a class="nav-link active" href="home.aspx">
                                <svg class="bi bi-chevron-right" width="16" height="16" viewBox="0 0 20 20"
                                    fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd"
                                        d="M6.646 3.646a.5.5 0 01.708 0l6 6a.5.5 0 010 .708l-6 6a.5.5 0 01-.708-.708L12.293 10 6.646 4.354a.5.5 0 010-.708z"
                                        clip-rule="evenodd" /></svg>
                                Jobs
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">
                                <svg class="bi bi-chevron-right" width="16" height="16" viewBox="0 0 20 20"
                                    fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                    <path fill-rule="evenodd"
                                        d="M6.646 3.646a.5.5 0 01.708 0l6 6a.5.5 0 010 .708l-6 6a.5.5 0 01-.708-.708L12.293 10 6.646 4.354a.5.5 0 010-.708z"
                                        clip-rule="evenodd" /></svg>
                                Verified Shoe For Sale
                            </a>
                        </li>
                       
                    </ul>
                </div>
            </div>
            <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-4" style="margin-top:50px;">
               <asp:Label ID="lblEmail" runat="server" Text="" Style="font-weight: bold; text-align:center;"></asp:Label> 
                <hr>
                   <div class="row">
                    <div class="col-sm-6">
                        <div class="card">
                            <div class="card-body">
                                <asp:PlaceHolder ID="slideshowPlaceholder" runat="server" ></asp:PlaceHolder>
                            </div>
                        </div>

                    </div>
                    <div class="col-sm-6">
                        <div class="card">
                            <div class="card-body">
                               <h5 class="card-title"><asp:Label ID="lblJob" runat="server" Text="" Style="font-weight: bold; text-align:center;"></asp:Label></h5>
                                <div class="tab">
                                  <button class="tablinks active" onclick="openShoeVerifOption(event, 'authentication')">Authentication</button>
                                  <button class="tablinks" onclick="openShoeVerifOption(event, 'codecheck')">Verify Upc</button>
                                </div>
                                <form id="form1" runat="server">
                                <div id="authentication" class="tabcontent active"> <!-- Adding 'active' class to the initial tab content -->
                                  
                                    <div class="form-group">
                                      <label for="email">Shoe Name:</label>
                                         <asp:TextBox ID="txtShoeName" runat="server" class="form-control"  TextMode="SingleLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                      <label for="pwd">Shoe Size:</label>
                                      <asp:TextBox ID="txtShoeSize" runat="server" class="form-control"  TextMode="SingleLine"></asp:TextBox>
                                    </div>
                                      <div class="form-group">
                                      <label for="pwd">Shoe Code:</label>
                                      <asp:TextBox ID="txtShoeCode" runat="server" class="form-control"  TextMode="SingleLine"></asp:TextBox>
                                    </div>
                                     <div class="form-group">
                                      <label for="pwd">Shoe Comment:</label>
                                      <asp:TextBox ID="txtShoeComment" runat="server" class="form-control"  TextMode="SingleLine"></asp:TextBox>
                                    </div>
                                      <div class="form-group">
                                        <label for="ddlShoeResult">Shoe Authenticity:</label>
                                      <asp:DropDownList ID="drpFindings" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="LEGIT" Value="LEGIT"></asp:ListItem>
                                        <asp:ListItem Text="FAKE" Value="FAKE"></asp:ListItem>
                                    </asp:DropDownList>

                                    </div>
                                    <hr />
                                     <asp:Button ID="proceed" class="form-control" runat="server" Text="Submit" OnClick="proceed_Click" />
                                
                                </div>

                                <div id="codecheck" class="tabcontent"> <!-- Removing 'active' class from other tab contents -->
                                    
                                  
                                     <div class="form-group">
                                      <label for="email">UPC NUMBER:</label>
                                         <asp:TextBox ID="txtUpcNumber" runat="server" class="form-control"  TextMode="SingleLine"></asp:TextBox>
                                    </div>
                                    <hr />
                                     <asp:Button ID="search" class="form-control" runat="server" Text="Submit" OnClick="proceed_Search" />
                                     <hr />
                                    <asp:Label ID="lblApiResponse" runat="server" Text="" Style="font-weight: bold; text-align:center;"></asp:Label>
                                    <img id="productImage" runat="server" />
                                    <hr />
                                    <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblBrand" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblColor" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblSize" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                                </div>

                                 </form>

                                  
                            </div>
                        </div>
                    </div>
                </div>


             

            </main>
        </div>
    </div>

    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/5.0.0-alpha1/js/bootstrap.min.js" integrity="sha384-oesi62hOLfzrys4LxRF63OJCXdXDipiYWBnvTl9Y9/TRlw5xlKIEHpNyvvDShgf/" crossorigin="anonymous"></script>
<script src="./js/shoeverif.js"> </script>

</body>
</html>
