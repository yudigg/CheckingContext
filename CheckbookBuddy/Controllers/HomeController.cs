using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using System.Web.Security;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Services.Description;
using System.Configuration;

namespace CheckbookBuddy.Controllers
{
    public class HomeController : Controller
    {
        private UserRepo userMgr;
        private FileRepo fileMgr;
        private OrderRepo orderRepo;
        string connStr = ConfigurationManager.ConnectionStrings["CheckbookBuddyContext"].ConnectionString;
        private string emailValue = Environment.GetEnvironmentVariable("CheckbookEmail");

        public HomeController()
        {
            userMgr = new UserRepo(connStr);
            fileMgr = new FileRepo(connStr);
            orderRepo = new OrderRepo(connStr);
        }

        [HttpPost]
        public ActionResult UploadFile(int orderID)
        {
            try
            {
                HttpPostedFileBase hpf = HttpContext.Request.Files["file"] as HttpPostedFileBase;
                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Tmp/Files"));// If you don't have the folder yet, you need to create.
                string savedFileName = Path.Combine(di.FullName, hpf.FileName);
                hpf.SaveAs(savedFileName);
                fileMgr.AddFile(new Image { FileName = savedFileName, OrderID = orderID });
                var msg = new { msg = "File Uploaded", filename = hpf.FileName, url = savedFileName };

                return Json(msg);
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change in file jquery.uploadfile.js, the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
        }

        public ActionResult Send(int orderId)
        {
            try
            {              
              var order = orderRepo.GetOrder(orderId);//check for null!
                orderRepo.GetImagePaths(orderId);
                List<string> paths = orderRepo.GetImagePaths(orderId);
           
                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("ygoldgrab@gmail.com");
                        mailMessage.Subject = "Test Email From App";
                        mailMessage.Body = "This is a test email with attachment";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(new MailAddress("ygoldgrab@gmail.com"));
                    //Add input file stream as attachment here and send the mail
                    foreach (string file in paths)
                    {
                        //Attachment attachment = new Attachment(filePath);
                        //mailMessage.Attachments.Add(attachment);
                        Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                        // Add time stamp information for the file.
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(file);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                        // Add the file attachment to this e-mail message.
                        mailMessage.Attachments.Add(data);
                    }
                    var smtp = new SmtpClient { Host = "smtp.gmail.com", EnableSsl = true };
                    var networkCred = new System.Net.NetworkCredential
                    {
                        UserName = mailMessage.From.Address,
                        Password = emailValue//////////////
                        };
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCred;
                        smtp.Port = 587;
                        smtp.Send(mailMessage);
                    }
                  
                
                var msg = new { msg = "File Uploaded" };

                return Json(msg);
            }

            catch (Exception e)
            {
                //If you want this working with a custom error you need to change in file jquery.uploadfile.js, the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
           
        }


        [HttpPost]
        public ActionResult Register(string username, string password)
        {

            userMgr.AddUser(username, password);
         
            SetupFormsAuthTicket(username);
            //return RedirectToAction("LogIn");
            return RedirectToAction("Index");
        }

        //public ActionResult Login()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            User user = userMgr.GetUser(username, password);

            if (user == null)
            {
                return Json("no user");//where?
            }


            if (userMgr.HasPendingOrder(user.UserID))
            {
                /////
                /////
            }

                SetupFormsAuthTicket(username);
            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "Cabinets");
        }

        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
               
            }

                return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            bool k = Roles.IsUserInRole("usercode", "Admin");
            
            var cc = User.Identity.IsAuthenticated;
            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private User SetupFormsAuthTicket(string userName)
        {
            User user;
            using (var usersContext = new CheckingContext())
            {
                user = usersContext.GetUser(userName);
            }
            var userId = user.UserID;
            var userData = userId.ToString(CultureInfo.InvariantCulture);
            var authTicket = new FormsAuthenticationTicket(1, //version
                                userName, // user name
                                DateTime.Now,             //creation
                                DateTime.Now.AddMinutes(30), //Expiration
                               false, //persistanceFlag, //Persistent
                                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return user;
        }

    }
}