﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Projects : EntityBase
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
