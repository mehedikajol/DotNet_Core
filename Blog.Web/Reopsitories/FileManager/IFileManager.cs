using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Blog.Web.Reopsitories.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile image);
    }
}
