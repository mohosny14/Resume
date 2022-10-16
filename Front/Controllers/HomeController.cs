using Core.Entities;
using Core.Interfaces;
using Front.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _onwer;
        private readonly IUnitOfWork<Projects> _projects;

        public HomeController(IUnitOfWork<Owner> onwer, IUnitOfWork<Projects> projects)
        {
            _onwer = onwer;
            _projects = projects;
        }
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Owner = _onwer.Entity.GetAll().First(),
                Projects = _projects.Entity.GetAll()
            };
            return View(viewModel);
        }
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPdf()
        {
            string filePath = "~/pdf/Juinor.NETDeveloper.pdf";
            Response.Headers.Add("Content-Disposition", "inline; filename=Juinor.NETDeveloper.pdf");
            return File(filePath, "application/pdf");
        }
    }
}
