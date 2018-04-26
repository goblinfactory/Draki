using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Draki.TestApplication.Controllers
{
    public class InputsController : Controller
    {
        //
        // GET: /Inputs/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                ViewBag.Message = "file == null or file length = 0";
                return View();
            }

            try
            {
                var dir = new DirectoryInfo(Server.MapPath("~/App_Data/Uploads"));
                if (!dir.Exists) dir.Create();
                string path = Path.Combine(dir.FullName, Path.GetFileName(file.FileName));
                file.SaveAs(path);
                ViewBag.Message = $"File saved to '{path}'.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ERROR:{ex.Message}";
            }
            return View();
        }
	}
}