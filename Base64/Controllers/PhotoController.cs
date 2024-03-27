using Base64.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Web;
namespace Base64.Controllers
{
    public class PhotoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConvertToBase64(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        byte[] imageData = memoryStream.ToArray();
                        string base64String = Convert.ToBase64String(imageData);
                        var model = new PhotoModel { Base64String = base64String };
                        return PartialView("_Base64Result", model);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Error occurred: {ex.Message}";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please select a file.";
            }

            return PartialView("_Error");
        }
    }
}
