using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebGrease;
using SendGrid;

namespace SendGridDemo.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mail/Create
        [HttpPost]
        public async Task< ActionResult> Create(FormCollection collection)
        {
            try
            {
                string to = "", from = "", subject = "", content="";

                foreach (var key in collection.AllKeys)
                {
                    switch (key)
                    {
                        case "To":
                            to = collection[key];
                            break;
                        case "From":
                            from = collection[key];
                            break;
                        case "Subject":
                            subject = collection[key];
                            break;
                        case "Content":
                            content = collection[key];
                            break;
                    }
                }

                var myMessage = new SendGridMessage();
                myMessage.AddTo( to);
                myMessage.From = new System.Net.Mail.MailAddress(
                                    from);
                myMessage.Subject = subject;
                myMessage.Text = content;
                //myMessage.Html = message.Body;

               // myMessage.EnableTemplateEngine(Template);

                var credentials = new NetworkCredential(
                           "azure_<azurecode>@azure.com",
                          "<password>"
                           );

                // Create a Web transport for sending email.
                var transportWeb = new SendGrid.Web(credentials);

                try
                {
                    // Send the email.
                    if (transportWeb != null)
                    {
                        await transportWeb.DeliverAsync(myMessage);
                    }
                    else
                    {
                        Trace.TraceError("Failed to create Web transport.");
                        await Task.FromResult(0);
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message + " SendGrid probably not configured correctly.");

                    
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message + " SendGrid probably not configured correctly.");

                return View();
            }
            return View();
        }

        // GET: Mail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mail/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Mail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mail/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
