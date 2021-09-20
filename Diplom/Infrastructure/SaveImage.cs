using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Infrastructure
{
    public class SaveImage : ISaveImage
    {
        public readonly IWebHostEnvironment enviroment;
        public SaveImage(IWebHostEnvironment env) => enviroment = env;
        public async Task<List<string>> Save(IFormFileCollection files)
        {
            List<string> pathes = new();
            foreach (var file in files)
            {
                string wwwRootPath = enviroment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string folder = "/assets/";
                string newFileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string newPath = Path.Combine(wwwRootPath + folder, newFileName);
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                pathes.Add(folder + newFileName);
            }
            return pathes;
        }
    }
}
