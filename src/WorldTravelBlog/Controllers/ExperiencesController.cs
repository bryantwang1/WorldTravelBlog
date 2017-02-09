using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldTravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldTravelBlog.Controllers
{
    public class ExperiencesController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();

        public IActionResult Index()
        {
            return View(db.Experiences.Include(experiences => experiences.Location).ToList());
        }

        public IActionResult Location(int id)
        {
            ViewBag.thisLocation = db.Locations.FirstOrDefault(locations => locations.LocationId == id);
            return View("Index", db.Experiences.Include(experiences => experiences.Location).Where(experiences => experiences.LocationId == id).ToList());
        }

        public IActionResult Details(int id)
        {
            var thisExperience = db.Experiences.Include(ep => ep.Location).FirstOrDefault(experiences => experiences.ExperienceId == id);
            ViewBag.People = db.ExperiencePersons.Include(ep => ep.Person).Where(ep => ep.ExperienceId == id).ToList();
            return View(thisExperience);
        }

        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Experience experience)
        {
            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Index", new { experience.LocationId });
        }

        public IActionResult Edit(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View(thisExperience);
        }

        [HttpPost]
        public IActionResult Edit(Experience experience)
        {
            db.Entry(experience).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            return View(thisExperience);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            db.Experiences.Remove(thisExperience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CreatePerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
            return RedirectToAction("People");
        }

        public IActionResult EditPerson(int id)
        {
            var thisPerson = db.Persons.FirstOrDefault(persons => persons.PersonId == id);
            
            return View(thisPerson);
        }

        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("People");
        }
        public IActionResult DeletePerson(int id)
        {
            var thisAnything = db.Persons.FirstOrDefault(anythings => anythings.PersonId == id);
            return View(thisAnything);
        }

        [HttpPost, ActionName("DeletePerson")]
        public IActionResult DeletePersonConfirmed(int id)
        {
            var thisAnything = db.Persons.FirstOrDefault(anythings => anythings.PersonId == id);
            db.Persons.Remove(thisAnything);
            db.SaveChanges();
            return RedirectToAction("People");
        }

        public IActionResult People()
        {
            return View(db.Persons.ToList());
        }

        public IActionResult DetailsPerson(int id)
        {
            var thisPerson = db.Persons.FirstOrDefault(persons => persons.PersonId == id);
            ViewBag.Experiences = db.ExperiencePersons.Include(ep => ep.Experience).ThenInclude(experience => experience.Location).Where(ep => ep.PersonId == id).ToList();
            return View(thisPerson);
        }

        public IActionResult AddPerson(int id)
        {
            ViewBag.thisExperience = db.Experiences.FirstOrDefault(experiences => experiences.ExperienceId == id);
            //ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Title");
            ViewBag.PersonId = new SelectList(db.Persons, "PersonId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(ExperiencePerson experiencePerson)
        {
            var relationList = db.ExperiencePersons.Where(ep => ep.ExperienceId.Equals(experiencePerson.ExperienceId)).ToList();
            bool alreadyExist = false;

            foreach(var existingEP in relationList)
            {
                if(existingEP.PersonId == experiencePerson.PersonId)
                {
                    alreadyExist = true;
                }
            }

            if(!alreadyExist)
            {
                db.ExperiencePersons.Add(experiencePerson);
            }
            //if(!db.ExperiencePersons.Contains())
            //{
            //    db.ExperiencePersons.Add(experiencePerson);
            //}
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AddExperience(int id)
        {
           ViewBag.thisPerson = db.Persons.FirstOrDefault(persons => persons.PersonId == id);
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Title");

            return View();
        }

        [HttpPost]
        public IActionResult AddExperience(ExperiencePerson experiencePerson)
        {
            var relationList = db.ExperiencePersons.Where(ep => ep.PersonId.Equals(experiencePerson.PersonId)).ToList();
            bool alreadyExist = false;

            foreach (var existingEP in relationList)
            {
                if (existingEP.ExperienceId == experiencePerson.ExperienceId)
                {
                    alreadyExist = true;
                }
            }

            if (!alreadyExist)
            {
                db.ExperiencePersons.Add(experiencePerson);
            }

            db.SaveChanges();
            return RedirectToAction("People");
        }
    }
}

