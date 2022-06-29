using camera.Data;
using camera.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;

namespace camera.Controllers
{
    public class ImagemController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly DataBaseContext _ctx;

        public ImagemController(IWebHostEnvironment env, DataBaseContext ctx)
        {
            _env = env;
            _ctx = ctx;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Capture(string name)
        {

            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var fileExtension = Path.GetExtension(fileName);
                            var newFileName = string.Concat(myUniqueFileName, fileExtension);
                            var filePath = Path.Combine(_env.WebRootPath, "CameraPhotos") + $@"\{newFileName}";
                            Console.WriteLine(filePath);
                            if (!string.IsNullOrEmpty(filePath))
                            {
                                StoreInFolder(file, filePath);
                            }
                            var imageBytes = System.IO.File.ReadAllBytes(filePath);
                            if (imageBytes != null)
                            {
                                StoreInDatabase(imageBytes, newFileName);
                            }
                        }
                    }
                    return Json(true);
                }
                return Json(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void StoreInFolder(IFormFile file, string fileName)
        {

            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        private void StoreInDatabase(byte[] imageBytes, string imageFileName)
        {
            try
            {
                if (imageBytes != null)
                {
                    string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
                    string imageUrl = string.Concat("data:image/jpg;base64", base64String);
                    ImageStore imageStore = new ImageStore()
                    {
                        CreateDate = DateTime.Now,
                        ImageBase64String = imageUrl,
                        ImageId = 0,
                        ImageFileName = imageFileName
                    };
                    _ctx.image.Add(imageStore);
                    _ctx.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
