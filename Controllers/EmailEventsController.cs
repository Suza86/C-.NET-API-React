using Agency04.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using System.Net;
using Agency04.Resource;

namespace Agency04.Controllers
{
    public class EmailEventsController : Controller
    {

        public IActionResult Index()
        {
            try
            {

            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailEvents emailEvents)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string emailMsg = "Dear member " + emailEvents.ToEmail + ", <br /><br /> We are sending our new important events <br />";

                     string emailSubject = EmailInfo.EMAIL_SUBJECT_DEFAUALT + " IMPORTANT EVENTS";

                    await this.SendEmailAsync(emailEvents.ToEmail, emailMsg, emailSubject);

                    return this.Json(new { EnableSuccess = true, SuccessTitle = "Success", SuccessMsg = "Notification email has been sent successfully!" });
                }
            }
            catch (Exception ex) {
                Console.Write(ex);
                return this.Json(new { EnableError = true, ErrorTitle = "Error", ErrorMsg = ex.Message });
            }

            return this.Json(new { EnableError = true, ErrorTitle = "Error", ErrorMsg = "Something is wrong, please try again later!" });
        }

        public async Task<bool> SendEmailAsync(string email, string msg, string subject = "")
        {
     
            bool isSend = false;

            try
            {
        
                var body = msg;
                var message = new MailMessage();

 
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress(EmailInfo.FROM);
                message.Subject = !string.IsNullOrEmpty(subject) ? subject : EmailInfo.EMAIL_SUBJECT_DEFAUALT;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
 
                    var credential = new NetworkCredential
                    {
                        UserName = EmailInfo.SMTP_USERNAME,
                        Password = EmailInfo.SMTP_PASSWORD
                    };

                    smtp.Credentials = credential;
                    smtp.Host = EmailInfo.SMTP_HOST;
                    smtp.Port = Convert.ToInt32(EmailInfo.SMTP_PORT);
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(message);
 
                    isSend = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSend;
        }
    }
}
