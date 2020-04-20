using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API_ia.Models;
using System.Drawing;
using System.IO;

namespace API_ia.MachineLearning
{
    public class Machine
    {
        public string ImageUploaded(FileUpload fileUpload)
        {
            try
            {
                FileInfo file = new FileInfo(fileUpload.Path);

                using (Image image = Image.FromFile(file.FullName))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, image.RawFormat);
                        byte[] imageBytes = ms.ToArray();

                        string base64string = Convert.ToBase64String(imageBytes);
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }

            return "File Uploaded";
        }
    }
}