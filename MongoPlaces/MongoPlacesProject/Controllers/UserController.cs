using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
//using MongoPlaces.Filters;
using MongoPlaces.Models;
using System.Web.Helpers;
using MongoDB.Bson;
using MongoDBProjectLibrary;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

namespace MongoPlaces.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class UserController : Controller
    {
        private MongoDBEntities db = new MongoDBEntities();

        //[HttpPost]
       public ActionResult AddPlace(string id)
        {

           var user =  db.GetUser(User.Identity.Name);
           var obj = user.listOfFavouriteObjects;
            if (obj == null)
                obj = new List<ObjectId>();
            MongoDB.Bson.ObjectId newObjectId = new MongoDB.Bson.ObjectId(id);
            if (!obj.Contains(newObjectId))
            {
                obj.Add(new MongoDB.Bson.ObjectId(id));
                db.UpdateFavPlaces(User.Identity.Name, obj);
            }

            return RedirectToAction("UserInfo", new { userEmail = user.email });
        }

        

        public ActionResult RemovePlace(string objectId)
        {
            var user = db.GetUser(User.Identity.Name);
            var obj = user.listOfFavouriteObjects;
            if (obj == null)
                return RedirectToAction("UserInfo", new { userEmail = user.email });
            MongoDB.Bson.ObjectId newObjectId = new MongoDB.Bson.ObjectId(objectId);
            if (obj.Contains(newObjectId))
            {
                obj.Remove(new MongoDB.Bson.ObjectId(objectId));
                db.UpdateFavPlaces(User.Identity.Name, obj);
            }
            return RedirectToAction("UserInfo", new { userEmail = user.email });
        }

        public ActionResult UserInfo(string userEmail)
        {
            return View(db.GetUser(userEmail));
        }

        public ActionResult Places()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View(db.GetAllUsers());
        }

        public ActionResult GetType(string id)
        {
            string res = db.GetType(id);

            return RedirectToAction("Details", res, new { Id = id });
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //MongoProvider provider = new MongoProvider();
                //bool loginSuccessful = true;// provider.ValidateUser(model.Email, model.Password);
                //if(loginSuccessful)
                //{
                //    SetAuthenticationCookie(model.Email);
                //    return RedirectToLocal(returnUrl);
                //}
                UserModels user = new UserModels
                {
                    email = model.Email,
                    password = Crypto.SHA1(model.Password)
                };
                if (db.Exist(user))
                {
                    LogOn(user.email);
                    return RedirectToLocal(returnUrl);
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public virtual void SetAuthenticationCookie(string email)
        {
            FormsAuthentication.SetAuthCookie(email, false);
        }

        public void LogOn(string email)
        {
            SetAuthenticationCookie(email);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Map", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    UserModels user = new UserModels
                    {
                        email = model.Email,
                        password = Crypto.SHA1(model.Password),
                        listOfFavouriteObjects = new List<MongoDB.Bson.ObjectId>()
                    };
                    if (!db.Exist(user))
                        db.CreateUser(user);
                    LogOn(user.email);
                    return RedirectToAction("Map", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            //ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Map", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
