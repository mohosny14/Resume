using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastrucure;
using Front.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Front.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IUnitOfWork<Projects> _project;
        private readonly IHostingEnvironment _hosting;

        public ProjectsController(IUnitOfWork<Projects> project, IHostingEnvironment hosting)
        {
            _project = project;
            _hosting = hosting;
        }

      
        public IActionResult Index()
        {
            return View(_project.Entity.GetAll());
        }

       
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = _project.Entity.GetById(id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Create(ProjectsViewModel model)
        {
            

                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string FullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                }
                Projects project = new Projects
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };
                _project.Entity.Insert(project);
                _project.Save();
                return RedirectToAction(nameof(Index));

        }
    

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = _project.Entity.GetById(id);
            if (projects == null)
            {
                return NotFound();
            }
            ProjectsViewModel viewmodel = new ProjectsViewModel
            {
                Id = projects.Id,
                ProjectName = projects.ProjectName,
                Description = projects.Description,
                ImageUrl = projects.ImageUrl


            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Edit(Guid id, ProjectsViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string FullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(FullPath, FileMode.Create));
                    }
                    Projects project = new Projects
                    {
                        Id = model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };
                    if(project.ImageUrl != null && project.ProjectName != "" && project.Description != "")
                    {
                        _project.Entity.Update(project);
                        _project.Save();
                    }
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //return View(model);
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = _project.Entity.GetById(id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdmin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _project.Entity.Delete(id);
            _project.Save();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "RequireAdmin")]
        private bool ProjectsExists(Guid id)
        {
            return _project.Entity.GetAll().Any(e => e.Id == id);
        }

       

    }
}
