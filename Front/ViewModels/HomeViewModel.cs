using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.ViewModels
{
    public class HomeViewModel
    {
        public Owner Owner { get; set; }

        public IEnumerable<Projects> Projects { get; set; }
    }
}
