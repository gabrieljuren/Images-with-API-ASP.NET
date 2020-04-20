using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using API_ia.Models;
using API_ia.MachineLearning;
using System.Web.Http.Cors;

namespace API_ia.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class uploadcontroller : ApiController
    {
        FileUpload fileUpload = new FileUpload();
        Machine _ml = new Machine();

        [HttpPost()]
        public async Task<string> Post()
        {
            string message = null;
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data");
            var provider =
                new MultipartFormDataStreamProvider(root);

            try
            {
               await Request.Content.ReadAsMultipartAsync(provider);

                foreach(var file in provider.FileData)
                {
                    var name = file.Headers
                        .ContentDisposition
                        .FileName;

                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);

                    File.Move(localFileName, filePath);

                    fileUpload.Path = filePath;

                    message = _ml.ImageUploaded(fileUpload);
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return message;
        }
    }

}
