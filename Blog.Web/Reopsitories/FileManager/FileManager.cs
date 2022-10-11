using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Web.Reopsitories.FileManager
{
    public class FileManager :IFileManager
    {
        private readonly string _imagePath;
        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:ImagesPath"];
        }

        public FileStream imageStream(string imageName)
        {
            return new FileStream(Path.Combine(_imagePath, imageName), FileMode.Open, FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {
                var savePath = Path.Combine(_imagePath);
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                //var fileName = image.FileName;

                var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}{mime}";

                using (var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);

                }

                return fileName;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error save file!";
            }
            
        }
    }
}
