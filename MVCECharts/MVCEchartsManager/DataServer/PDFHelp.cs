using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Repository_基础结构层.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCEchartsManager.DataServer
{
    public class PDFHelp
    {
        public byte[] ConvertHtmlTextToPDF(string htmltext)
        {
            if (string.IsNullOrEmpty(htmltext))
            {
                return null;
            }
            //避免htmlText没有任何html tag标签的純文字时，转PDF时会挂掉，所以一律加上<p>标签
            //htmlText = "<p>" + htmltext + "</p>";

            MemoryStream stream = new MemoryStream();
            byte[] data = Encoding.UTF8.GetBytes(htmltext);
            MemoryStream msInput = new MemoryStream(data);
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, stream);
            //指定文件默认缩放标准100%
            PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);
            doc.Open();
            //使用XMLWorkerHelper把Html parse到PDF
            XMLWorkerHelper .GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8, new UnicodeFontFactory());
            //將pdfDest 写入到PDF
            PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);
            writer.SetOpenAction(action);
            doc.Close();
            msInput.Close();
            stream.Close();
            //回传PDF 
            return stream.ToArray();
        }
    }
}