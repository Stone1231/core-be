using System.IO;
using Microsoft.AspNetCore.Http;

namespace Backend.Services
{
    public class FileService
    {
        public static string Upload(IFormFile file, string folder)
        {
            var folderPath = Path.Combine("Static", folder);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            } 

            var filePath = Path.Combine(
                                Path.Combine(Directory.GetCurrentDirectory(), folderPath),
                                file.FileName);
                                               
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                //await file.CopyToAsync(fileStream);
                file.CopyTo(fileStream);
            }

            return file.FileName;
        }

        public static void Clear(string folder){
            var folderPath = Path.Combine("Static", folder);

            if (!Directory.Exists(folderPath)){
                return;
            }

            var dir = new DirectoryInfo(
                Path.Combine(
                    Path.Combine(
                        Directory.GetCurrentDirectory(), folderPath))                
                );

            foreach (var f in dir.GetFiles())
            {
                f.Delete(); 
            }
            foreach (var d in dir.GetDirectories())
            {
                d.Delete(true); 
            }
        }
    }
}