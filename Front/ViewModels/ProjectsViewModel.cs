using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Front.ViewModels
{
    public class ProjectsViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Project Name")]
        public string ProjectName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage =("Select an Image"))]
        public string ImageUrl { get; set; }

        public IFormFile File { get; set; }
    }
}
