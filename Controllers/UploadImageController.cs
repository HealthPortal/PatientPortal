using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthPortal.Controllers
{
    public class UploadImageController : Controller
    {
        private static readonly IInsuranceBillRepository _insbill = new InsuranceBillRepository();
        //
        // GET: /UploadImage/
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileext = ".png";
                    userId = Session["UserId"].ToString();
                    using (Image Img = Image.FromStream(file.InputStream))
                    {
                        String imgPath = String.Format("img/{0}{1}", userId, fileext);
                        Size newimgsize = NewImageSize(Img.Height, Img.Width, 45, 45);
                        using (Image newimg = new Bitmap(Img, newimgsize.Width, newimgsize.Height))
                        {
                            newimg.Save(String.Format("{0}{1}", Server.MapPath("~"), imgPath), Img.RawFormat);
                        }
                        username = Session["UserName"].ToString();
                        string newfileext = ".jpg";
                        String newimgPath = String.Format("img/{0}{1}", username, newfileext);
                        if (System.IO.File.Exists(newimgPath))
                        {
                            System.IO.File.Delete(newimgPath);
                        }
                        Size newimgprofile = NewImageSize(Img.Height, Img.Width, 250, 250);
                        using (Image newprof = new Bitmap(Img, newimgprofile.Width, newimgprofile.Height))
                        {
                            newprof.Save(String.Format("{0}{1}", Server.MapPath("~"), newimgPath));
                        }
                    }
                    ViewBag.Message = "Upload successful";
                }

            }
            catch
            {
                ViewBag.Message = "Upload Failed";
            }
            return View();
        }

        [HttpPost]
        public ActionResult InscardUpload(HttpPostedFileBase file1)
        {
            try
            {
                if (file1 != null && file1.ContentLength > 0)
                {
                    var filename = file1.FileName;
                    using (Image Img = Image.FromStream(file1.InputStream))
                    {
                        String imgPath = String.Format("img/{0}", filename);
                        Size newimgsize = NewImageSize(Img.Height, Img.Width, 280, 180);
                        using (Image newimg = new Bitmap(Img, newimgsize.Width, newimgsize.Height))
                        {
                            newimg.Save(String.Format("{0}{1}", Server.MapPath("~"), imgPath), Img.RawFormat);
                        }
                        userId = Session["UserId"].ToString();
                        InsuranceBilling imgurl = _insbill.GetByUserId(userId).First();
                        imgurl.Image1 = "../../" + imgPath;
                        var insbill = _insbill.UpdateInsBill(imgurl);
                    }
                    ViewBag.Message = "Upload successful";
                }

            }
            catch
            {
                ViewBag.Message = "Upload Failed";
            }
            return View();
        }

        [HttpPost]
        public ActionResult InscardUploadImg2(HttpPostedFileBase file2)
        {
            try
            {
                if (file2 != null && file2.ContentLength > 0)
                {
                    var filename = file2.FileName;
                    using (Image Img = Image.FromStream(file2.InputStream))
                    {
                        String imgPath = String.Format("../../img/{0}", filename);
                        Size newimgsize = NewImageSize(Img.Height, Img.Width, 280, 180);
                        using (Image newimg = new Bitmap(Img, newimgsize.Width, newimgsize.Height))
                        {
                            newimg.Save(String.Format("{0}{1}", Server.MapPath("~"), imgPath), Img.RawFormat);
                        }
                        userId = Session["UserId"].ToString();
                        InsuranceBilling imgurl = _insbill.GetByUserId(userId).First();
                        imgurl.Image2 = "../../" + imgPath;
                        var insbill = _insbill.UpdateInsBill(imgurl);
                    }
                    ViewBag.Message = "Upload successful";
                }

            }
            catch
            {
                ViewBag.Message = "Upload Failed";
            }
            return View();
        }


        public string userId { get; set; }

        public Size NewImageSize(int OriginalHeight, int OriginalWidth, int thumbWidth, int thumbHeight)
        {
            Size NewSize;
            NewSize = new Size(Convert.ToInt32(thumbWidth), Convert.ToInt32(thumbHeight));
            return NewSize;
        }




        public string username { get; set; }
    }
}
