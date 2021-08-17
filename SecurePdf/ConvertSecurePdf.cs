using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.Pdf;
using System.Collections.Generic;
using System.IO;
using System;
using System.Drawing;
using System.Linq;

namespace SecurePdf
{
    public class ConvertSecurePdf
    {
        public string ConvertWordFileToPdf(string file)
        {
            //Creates a new Word document.
            WordDocument m_wordDocument = new WordDocument();

            //Adds new section to the document.
            IWSection section = m_wordDocument.AddSection();

            //Sets the page margins to zero.
            section.PageSetup.Margins.All = 0;

            //Adds new paragraph to the section.
            IWParagraph firstParagraph = section.AddParagraph();

            SizeF defaultPageSize = new SizeF(m_wordDocument.LastSection.PageSetup.PageSize.Width, m_wordDocument.LastSection.PageSetup.PageSize.Height);

            //Loads the PDF document from the given file path.
            using (PdfLoadedDocument m_loadedDocument = new PdfLoadedDocument(file))
            {
                for (int i = 0; i < m_loadedDocument.Pages.Count; i++)
                {
                    //Exports the PDF document page as image with the given size.
                    using (Image image = m_loadedDocument.ExportAsImage(i, defaultPageSize, false))
                    {
                        //Adds image to the paragraph
                        IWPicture picture = firstParagraph.AppendPicture(image);

                        //Sets width and height for the image.
                        picture.Width = image.Width;
                        picture.Height = image.Height;
                    }
                }
            };

            //Saves the Word document
            m_wordDocument.Save("C://Users//AhsanFarooq//Downloads//Save.docx");

            //Close the document.
            m_wordDocument.Dispose();
            //PdfLoadedDocument loadedDocument = new PdfLoadedDocument("C://Users//AhsanFarooq//Downloads//cisco.pdf");
            ////Setting permission flags to default
            //loadedDocument.Security.Permissions = PdfPermissionsFlags.Default;
            return "Success";
        }
        //public string ConvertWordFileToPdf(string file)
        //{
        //    string fileName = Path.GetFileName(file);
        //    string pdfFileName = fileName.Substring(0, fileName.Length - 5);
        //    string dest = file.Substring(0, file.Length - fileName.Length);
        //    //Load an existing Word document
        //    WordDocument wordDocument = new WordDocument(file, FormatType.Docx);
        //    //Create an instance of DocToPDFConverter
        //    DocToPDFConverter converter = new DocToPDFConverter();
        //    //Convert Word document into PDF document
        //    PdfDocument pdfDocument = converter.ConvertToPDF(wordDocument);
        //    PdfSecurity secutity = pdfDocument.Security;
        //    secutity.OwnerPassword = "Syncfusion";
        //    secutity.Permissions = PdfPermissionsFlags.Default;
        //    //Save the PDF file
        //    pdfDocument.Save(dest + "\\" + pdfFileName + ".pdf");
        //    //Close the instance of document objects
        //    pdfDocument.Close(true);
        //    wordDocument.Close();
        //    return "Success";
        //}

    }
}
