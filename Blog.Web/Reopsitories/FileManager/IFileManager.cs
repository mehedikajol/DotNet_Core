using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Blog.Web.Reopsitories.FileManager
{
    public interface IFileManager
    {
        FileStream imageStream(string imageName);
        Task<string> SaveImage(IFormFile image);
    }
}
