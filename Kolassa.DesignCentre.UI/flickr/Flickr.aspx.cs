using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Infrastructure.BLL;
using FlickrNet;
using System.Configuration;


namespace TEST.UI.Pages.Account
{
    public partial class FlickrPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void odsSets_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["userId"] = ConfigurationManager.AppSettings["defaultUser"].ToString();
        }

        protected void odsPhotos_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.Arguments.MaximumRows = int.Parse(ConfigurationManager.AppSettings["defaultPageSize"]);
        }

        protected void ddlSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPager pgr = lvImages.FindControl("DataPager1") as DataPager;
            if (pgr != null && lvImages.Items.Count != pgr.TotalRowCount)
            {
                pgr.SetPageProperties(0, pgr.MaximumRows, false);
            }
        }

        public string FilckerUpload(string url, string title, string description, string tags)
        {
            OAuthAccessToken accessToken = new OAuthAccessToken();
            accessToken.FullName = "DesignCentre";
            accessToken.Token = "72157685169044166-bdf73b31146bdb07";
            accessToken.TokenSecret = "73c702618d06a9ed";
            accessToken.UserId = "61787525@N00";
            accessToken.Username = "kolassa";
         //   Flickr.OAuthAccessToken = ConfigurationManager.AppSettings("Flickr.OAuthAccessToken")
           //     Flickr.OAuthAccessTokenSecret = ConfigurationManager.AppSettings("Flickr.OAuthAccessTokenSecret")


            FlickrManager.OAuthToken = accessToken;
            Flickr flickr = FlickrManager.GetAuthInstance();
            string FileuploadedID =  flickr.UploadPicture(@url, title, description, tags, true, false, false);
            PhotoInfo oPhotoInfo = flickr.PhotosGetInfo(FileuploadedID);
            return oPhotoInfo.Small320Url;
        }

        protected void Button1_Click(object sender, EventArgs e)
        { try
            {
            
            if (FileUpload1.HasFile )
                {
       
                string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName);
                if(fileExtension.ToLower() != ".jpg" && fileExtension.ToLower() != ".png" && fileExtension.ToLower() != ".png" )
                    { lblMessage.Text = "Only png, jpg and gif formats are allowed."; }
                else
                    {          int iFileSize = FileUpload1.PostedFile.ContentLength;
                    if (iFileSize > 2097152)
                        { lblMessage.Text = "Only files under 2MB are allowed."; }
                    else { 
                        string sfile = FileUpload1.FileName;
                        FilckerUpload(FileUpload1.FileName, "The Building", "THis is a nice Place", "Design Centre");
                        lblMessage.Text = "File Uploaded Successfully.";
                        }
                    }
                 }
            else
                {
                    lblMessage.Text = "Please Select a file.";
                }
        }
        catch (Exception le)
            {
                lblMessage.Text =  "An Error has Occurred." + le.InnerException;
            }
        }

}
    public class FlickrManager
    {
        public const string ApiKey = "8fab471014541db5f9b7bab1bb60c005";
        public const string SharedSecret = "a3461fd7690c6b16";

        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        public static Flickr GetAuthInstance()
        {
            var f = new Flickr(ApiKey, SharedSecret);
            if (OAuthToken != null)
            {
                f.OAuthAccessToken = OAuthToken.Token;
                f.OAuthAccessTokenSecret = OAuthToken.TokenSecret;
            }
            return f;
        }

        public static OAuthAccessToken OAuthToken
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["OAuthToken"] == null)
                {
                    return null;
                }
                var values = HttpContext.Current.Request.Cookies["OAuthToken"].Values;
                return new OAuthAccessToken
                {
                    FullName = values["FullName"],
                    Token = values["Token"],
                    TokenSecret = values["TokenSecret"],
                    UserId = values["UserId"],
                    Username = values["Username"]
                };
            }
            set
            {
                // Stores the authentication token in a cookie which will expire in 1 hour
                var cookie = new HttpCookie("OAuthToken")
                {
                    Expires = DateTime.UtcNow.AddHours(1),
                };
                cookie.Values["FullName"] = value.FullName;
                cookie.Values["Token"] = value.Token;
                cookie.Values["TokenSecret"] = value.TokenSecret;
                cookie.Values["UserId"] = value.UserId;
                cookie.Values["Username"] = value.Username;
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
    }
}