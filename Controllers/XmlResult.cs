using HealthPortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HealthPortal.Controllers
{
    public class XmlResult:ActionResult
    {

        private readonly XDocument _document;

        public Formatting Formatting { get; set; }
        public string MimeType { get; set; }

        public XmlResult(XDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            _document = document;

            // Default values
            MimeType = "text/xml";
            Formatting = Formatting.None;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = MimeType;

            using (var writer = new XmlTextWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8) { Formatting = Formatting })
                _document.WriteTo(writer);
        }
       
        
        //public override void ExecuteResult(ControllerContext context)
        //{
        //    HttpContextBase httpcontextbase = context.HttpContext;
        //    httpcontextbase.Response.Buffer = true;
        //    httpcontextbase.Response.Clear();

        //    string filename = "newxml.xml";
        //    httpcontextbase.Response.AddHeader("content-disposition","attachment; filename=" + filename);
        //    httpcontextbase.Response.ContentType = "text/xml";
        //    using (StringWriter writer=new StringWriter())
        //    {
        //        XmlSerializer xml = new XmlSerializer(typeof(PdfModel));
        //        xml.Serialize(writer,data);
        //        httpcontextbase.Response.Write(writer);
        //    }
        //}

        //public PDFViewModel data { get; set; }
    }
}
