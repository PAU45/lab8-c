using lab10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab10.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        public ActionResult Index()
        {
            List<PersonModel> people;

            // ESTE ES EL ARREGLO CLAVE:
            // Inicializa la lista SÓLO si no existe en la Session.
            if (Session["people"] == null)
            {
                people = new List<PersonModel>
                {
                    // Nota: Se mantiene el error tipográfico 'FirtName' de tu modelo.
                    new PersonModel { Id = 1, FirtName = "John", LastName = "Doe" },
                    new PersonModel { Id = 2, FirtName = "Jane", LastName = "Smith" },
                    new PersonModel { Id = 3, FirtName = "Michael", LastName = "Johnson" }
                };
                Session["people"] = people;
            }
            else
            {
                // Si ya existe, la recuperamos.
                people = (List<PersonModel>)Session["people"];
            }

            return View(people);
        }

        // GET: People/Details/5
        public ActionResult Details(int id)
        {
            List<PersonModel> people = (List<PersonModel>)Session["people"];
            PersonModel model = people?.FirstOrDefault(p => p.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        public ActionResult Create(PersonModel model)
        {
            try
            {
                List<PersonModel> people = (List<PersonModel>)Session["people"];

                if (people == null)
                {
                    // Debería ser muy raro, pero es un buen chequeo de seguridad.
                    return RedirectToAction("Index");
                }

                // MEJORA: Asignar el siguiente Id consecutivo.
                int nextId = people.Any() ? people.Max(p => p.Id) + 1 : 1;
                model.Id = nextId;

                people.Add(model);

                return RedirectToAction("Index");
            }
            catch
            {
                // Si falla la validación del modelo o la conversión, se vuelve a la vista.
                return View(model);
            }
        }

        // GET: People/Edit/5
        public ActionResult Edit(int id)
        {
            List<PersonModel> people = (List<PersonModel>)Session["people"];
            PersonModel model = people?.FirstOrDefault(p => p.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: People/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PersonModel model)
        {
            try
            {
                List<PersonModel> people = (List<PersonModel>)Session["people"];

                // Encontrar el índice del elemento a modificar
                int index = people.FindIndex(p => p.Id == id);

                if (index != -1)
                {
                    // Asignar el Id original para asegurar que no se cambie accidentalmente.
                    model.Id = id;
                    // Reemplazar el elemento.
                    people[index] = model;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: People/Delete/5
        public ActionResult Delete(int id)
        {
            List<PersonModel> people = (List<PersonModel>)Session["people"];
            PersonModel model = people?.FirstOrDefault(p => p.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")] // Usamos ActionName para evitar conflicto con la sobrecarga
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                List<PersonModel> people = (List<PersonModel>)Session["people"];

                // Buscar y eliminar el elemento.
                PersonModel personToRemove = people?.FirstOrDefault(p => p.Id == id);

                if (personToRemove != null)
                {
                    people.Remove(personToRemove);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                // Puedes optar por devolver a la vista de confirmación o a Index
                return RedirectToAction("Index");
            }
        }
    }
}