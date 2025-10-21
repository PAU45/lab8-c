using lab10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab10.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<ProductModel> products;

            // Inicializa la lista SÓLO si no existe en la Session.
            if (Session["products"] == null)
            {
                // Datos iniciales para la lista de productos
                products = new List<ProductModel>
                {
                    new ProductModel { Id = 1, Name = "Laptop", Price = 1200.50m, StockQuantity = 15 },
                    new ProductModel { Id = 2, Name = "Mouse Inalámbrico", Price = 25.99m, StockQuantity = 50 },
                    new ProductModel { Id = 3, Name = "Monitor 4K", Price = 450.00m, StockQuantity = 8 }
                };
                Session["products"] = products;
            }
            else
            {
                // Si ya existe, la recuperamos.
                products = (List<ProductModel>)Session["products"];
            }

            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            List<ProductModel> products = (List<ProductModel>)Session["products"];
            ProductModel model = products?.FirstOrDefault(p => p.Id == id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel model)
        {
            try
            {
                // Si la validación del modelo falla (p. ej., campos requeridos), regresa a la vista.
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                List<ProductModel> products = (List<ProductModel>)Session["products"];

                if (products == null)
                {
                    return RedirectToAction("Index");
                }

                // Asignar el siguiente Id consecutivo.
                int nextId = products.Any() ? products.Max(p => p.Id) + 1 : 1;
                model.Id = nextId;

                products.Add(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            List<ProductModel> products = (List<ProductModel>)Session["products"];
            ProductModel model = products?.FirstOrDefault(p => p.Id == id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductModel model)
        {
            try
            {
                // Si la validación del modelo falla, regresa a la vista.
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                List<ProductModel> products = (List<ProductModel>)Session["products"];

                // Encontrar el índice del elemento a modificar
                int index = products.FindIndex(p => p.Id == id);

                if (index != -1)
                {
                    // Mantener el Id original y reemplazar el elemento.
                    model.Id = id;
                    products[index] = model;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            List<ProductModel> products = (List<ProductModel>)Session["products"];
            ProductModel model = products?.FirstOrDefault(p => p.Id == id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                List<ProductModel> products = (List<ProductModel>)Session["products"];

                // Buscar y eliminar el elemento.
                ProductModel productToRemove = products?.FirstOrDefault(p => p.Id == id);

                if (productToRemove != null)
                {
                    products.Remove(productToRemove);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}