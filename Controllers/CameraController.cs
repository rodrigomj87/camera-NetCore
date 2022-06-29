using camera.Data;
using camera.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace camera.Controllers
{
    public class CameraController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly DataBaseContext _ctx;

        public CameraController(IWebHostEnvironment env, DataBaseContext ctx)
        {
            _env = env;
            _ctx = ctx;
        }
        public IActionResult Capture()
        {
            return View();
        }

        
    }
}
