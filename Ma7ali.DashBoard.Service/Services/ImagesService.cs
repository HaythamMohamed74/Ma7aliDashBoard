using Ma7ali.DashBoard.Data.Entities.OrderEntities;
using Ma7ali.DashBoard.Service.Interfaces;
using Ma7aliDashBoard.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ma7ali.DashBoard.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IFileProvider _fileProvider;
        

        public ImageService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

  

        public async Task<List<string>> uploadImagesAsync(IFormFileCollection files, string folder)
        {
            var savedImagePaths = new List<string>();
            var folderPath = Path.Combine("images", folder);
            //var targetPath = Path.Combine("wwwroot", folderPath);
            var imageDirc= Path.Combine("wwwroot", "images",folder);

            if (!Directory.Exists(imageDirc))
                Directory.CreateDirectory(imageDirc);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var fullPath = Path.Combine(imageDirc, fileName);


                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    savedImagePaths.Add($"/{folderPath}/{fileName}");
                }
            }

            return savedImagePaths;
        }

        public void DeleteImage(string imagePath)
        {
            var fullPath = Path.Combine("wwwroot", imagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

   

       
    }
}
