using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NayanTraders.Models;

namespace RegistrationandLogin.Controllers
{
    public class UserController : Controller
    {
        //Registration Action
    [HttpGet]
    public ActionResult Registration()
        { 
          return View();
        }

        //Registration get method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {

            user.UserTypeID = 2;
            bool Status = false;
            string Message = "";
            //Model Validation
            if(ModelState.IsValid)
            {
                #region //checking wheather the email is exists or not
                var IsEmailExist = IsEmailExists(user.EmailID);
                if(IsEmailExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already Exists");
                    return View(user);
                }

                #endregion
                #region//Generate Code
                user.ActivationCode = Guid.NewGuid();
                #endregion
                #region//Password Hassing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion
                user.IsEmailVerified = false;
                #region//Save to database
                using (DataBaseContext db = new DataBaseContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    SendverificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    Message = "Registration successully done.Account activation Link" +
                        "has been sent to your Email Account:" + user.EmailID;
                    Status = true;
                }
                #endregion


            }
            else
            {
                Message = "Invalid Request";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;

            return View(user);

        }

        //Verify Account
        [HttpDelete]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (DataBaseContext db = new DataBaseContext())
            {
                db.Configuration.ValidateOnSaveEnabled = false;//?This has been done to avoid confirm password does not match issue
                var v = db.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();//?
                if(v!=null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
                return View();
        }

        //Login
     
        public ActionResult Login(string ret)
        {
            ViewBag.ret = ret;
            return View();
        }

        //LogIn Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string returnurl)
        {
            string Message = "";
            using (DataBaseContext db = new DataBaseContext())
            {
                var b = db.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if(b!=null)
                {
                    if(string.Compare(Crypto.Hash(login.Password),b.Password)==0)
                    {
                        Session["name"] = b.FirstName;
                        Session["id"] = b.UserId;
                        Session["type"] = b.UserType.Name.ToString();
                        int timeout = login.RememberMe ? 525600 : 20;//oneyear=525600 minute
                       
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        var encrypt = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                        cookie.Expires= DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        if (Session["type"].ToString()== "Admin")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index2", "Home");
                        }

                        //if (Url.IsLocalUrl(returnurl))
                        //{
                        //    return RedirectToAction(returnurl);
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Home");
                        //}
                    }
                    else
                    {
                        Message = "Invalid Credential Provided";
                    }
                }
                else
                {
                    Message = "Invalid Credential";
                }
            }
                ViewBag.Message = Message;
            return View();
        }

        //Logout
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        //Forgot Password
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //forgot password Post method
        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            bool status = false;

            using (DataBaseContext dc = new DataBaseContext())
            
            {
                var account = dc.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendverificationLinkEmail(account.EmailID, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the Resetcode
            //Find Account associate with the Link
            //Redirect to reset Password Page

            using (DataBaseContext db = new DataBaseContext())
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if(user!=null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();

                }
            }
        }

        //Reset Password Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            string message = "";
            using (DataBaseContext db = new DataBaseContext())
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();

                if(user!=null)
                {
                    user.Password = Crypto.Hash(model.NewPassword);
                    user.ResetPasswordCode = "";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "New Password Updated successfully";
                }
                else
                {
                    message = "something Invalid Here.";
                }
            }


                ViewBag.message = message;
            return View(model);
        }

        [NonAction]
        public bool IsEmailExists(string EmailID)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                var user = db.Users.Where(u => u.EmailID == EmailID).FirstOrDefault();
                return user != null;
            }
        }
        [NonAction]
        public void SendverificationLinkEmail(string emailID,string activationcode, string emailFor= "VerifyAccount")
        {
           var verifyurl="/User/"+emailFor+"/" + activationcode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyurl);
            var fromEmail = new MailAddress("nayandey07@gmail.com", "Coders.org");
            var toEmail = new MailAddress(emailID);
            var fromPassword = "n01682616787a";

            string subject = "";
            string body = "";

            if (emailFor == "VerifyAccount")
            {
                subject = "Your Account has successfully created";
                body = "<br/><br/>We are excited to to tell you that you account for the Coders.org has been created successfully" +
                    "Please click on the below link to verify your account" + "<br/><br/><a href='" + link + "'>" + link + "</a>";
            }
            else if (emailFor == "ResetPassword")
            {
                 subject = "Reset Password";
                 body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }



            var smtp = new SmtpClient
            {
                Host="smtp.gmail.com",
                Port=587,
                EnableSsl=true,
                DeliveryMethod=SmtpDeliveryMethod.Network,
                UseDefaultCredentials=false,
                Credentials=new NetworkCredential(fromEmail.Address,fromPassword)
            };
            using (var Message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            }
            )
               smtp.Send(Message);
          
        }

    }  
}