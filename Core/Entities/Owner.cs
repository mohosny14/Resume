using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Owner : EntityBase
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Berif { get; set; }
        public string Image { get; set; }
        public Adress Adress { get; set; }
    }
}
