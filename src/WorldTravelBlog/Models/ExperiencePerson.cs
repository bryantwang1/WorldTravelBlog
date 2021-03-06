﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorldTravelBlog.Models
{
    public class ExperiencePerson
    {
        [Key]
        public int ExperiencePersonId { get; set; }
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
