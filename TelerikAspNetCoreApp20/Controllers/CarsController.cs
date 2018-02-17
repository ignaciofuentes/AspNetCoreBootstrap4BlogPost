using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelerikAspNetCoreApp20.Models;

namespace TelerikAspNetCoreApp20.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsDb _context;

        public CarsController(CarsDb context)
        {
            _context = context;
        }

    }
}
