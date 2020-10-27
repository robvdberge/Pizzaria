using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzariaTonno2.Models;

namespace PizzariaTonno2.Controllers
{
    public class PizzasController : Controller
    {
        private PizzariaTonno2Context db = new PizzariaTonno2Context();

        // GET: Pizzas
        public ActionResult Index()
        {
            return View(db.Pizzas.ToList());
        }

        // GET: Pizzas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // GET: Pizzas/Create
        public ActionResult Create()
        {
            ViewBag.IngredientId1 = new SelectList(db.Ingredients, "IngredientId", "Naam", 1);
            ViewBag.IngredientId2 = new SelectList(db.Ingredients, "IngredientId", "Naam", 2);
            ViewBag.IngredientId3 = new SelectList(db.Ingredients, "IngredientId", "Naam", 7);
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PizzaId,PizzaSoort,IngredientId1,IngredientId2,IngredientId3,Price")] Pizza pizza)
        {
            pizza.Price = GetTotalPrice(pizza.IngredientId1, pizza.IngredientId2, pizza.IngredientId3);
            if (ModelState.IsValid)
            {
                db.Pizzas.Add(pizza);
                db.SaveChanges();
                return RedirectToAction("Bedankt", pizza);
            }
            ViewBag.IngredientId1 = new SelectList(db.Ingredients, "IngredientId", "Naam");
            ViewBag.IngredientId2 = new SelectList(db.Ingredients, "IngredientId", "Naam");
            ViewBag.IngredientId3 = new SelectList(db.Ingredients, "IngredientId", "Naam");
            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            ViewBag.IngredientId1 = new SelectList(db.Ingredients, "IngredientId", "Naam", pizza.IngredientId1);
            ViewBag.IngredientId2 = new SelectList(db.Ingredients, "IngredientId", "Naam", pizza.IngredientId2);
            ViewBag.IngredientId3 = new SelectList(db.Ingredients, "IngredientId", "Naam", pizza.IngredientId3);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PizzaId,PizzaSoort,IngredientId1,IngredientId2,IngredientId3,Price")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizza).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizza pizza = db.Pizzas.Find(id);
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Pizzas/Bedankt
        public ActionResult Bedankt(Pizza pizza)
        {
            ViewBag.Total = GetTotalPrice(pizza.IngredientId1, pizza.IngredientId2, pizza.IngredientId3);
            return View();
        }

        // Get the total price of a pizza
        public decimal GetTotalPrice(int topping1, int topping2, int topping3)
        {
            decimal basePrice = 4;
            Ingredient ingredient1 = db.Ingredients.Find(topping1);
            Ingredient ingredient2 = db.Ingredients.Find(topping2);
            Ingredient ingredient3 = db.Ingredients.Find(topping3);
            decimal Total = basePrice;
            Total += ingredient1.Price + ingredient2.Price + ingredient3.Price;

            return Total;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
