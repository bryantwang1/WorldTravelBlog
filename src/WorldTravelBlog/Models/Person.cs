using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldTravelBlog.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ExperiencePerson> ExperiencePersons { get; set; }
    }
}
