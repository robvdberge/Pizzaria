using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PizzariaTonno2.Models;

namespace PizzariaTonno2.Controllers
{
    public class IngredientsApiController : ApiController
    {
        private PizzariaTonno2Context db = new PizzariaTonno2Context();

        // GET: api/IngredientsApi
        public IQueryable<Ingredient> GetIngredients()
        {
            return db.Ingredients.AsQueryable();
        }

        // GET: api/IngredientsApi/5
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult GetIngredient(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        // PUT: api/IngredientsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIngredient(int id, Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            db.Entry(ingredient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/IngredientsApi
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult PostIngredient(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ingredients.Add(ingredient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ingredient.IngredientId }, ingredient);
        }

        // DELETE: api/IngredientsApi/5
        [ResponseType(typeof(Ingredient))]
        public IHttpActionResult DeleteIngredient(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            db.Ingredients.Remove(ingredient);
            db.SaveChanges();

            return Ok(ingredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IngredientExists(int id)
        {
            return db.Ingredients.Count(e => e.IngredientId == id) > 0;
        }
    }
}