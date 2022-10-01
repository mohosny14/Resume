using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Adress : EntityBase
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int Number { get; set; }
    }
}
