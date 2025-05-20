using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma7ali.DashBoard.Service.Interfaces
{
    public interface IImageService
    {
        Task<List<string>> uploadImagesAsync(IFormFileCollection files, string scr);

        void DeleteImage(string imagePath);
    }
}
