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

namespace Final_Capstone.Models
{
    public class CarsController : ApiController
    {
        private CarContext db = new CarContext();

        // GET: api/Cars
        public IQueryable<Car> GetCars()
        {
            return db.Cars;
        }

        // GET: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult GetCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpGet]
        public List<Car> GetModel(string model)
        {
            List<Car> carsByModel = new List<Car>();
            var query = db.Cars.Where(c => c.Model.Contains(model));
            carsByModel = query.ToList();
          
            return carsByModel;
        }

        [HttpGet]
        public List<Car> GetMake(string make)
        {
            List<Car> carsByMake = new List<Car>();

            var query = db.Cars.Where(c => c.Make.Contains(make));
            carsByMake = query.ToList();

            return carsByMake;
        }

        [HttpGet]
        public List<Car> GetYear(int year)
        {
            List<Car> carsByYear = new List<Car>();

            var query = db.Cars.Where(c => c.Year == year);
            carsByYear = query.ToList();

            return carsByYear;
        }

        [HttpGet]
        public List<Car> GetColor(string color)
        {
            List<Car> carsByColor = new List<Car>();

            var query = db.Cars.Where(c => c.Color.Contains(color));
            carsByColor = query.ToList();

            return carsByColor;
        }


        // PUT: api/Cars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.ID)
            {
                return BadRequest();
            }

            db.Entry(car).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cars.Add(car);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = car.ID }, car);
        }

        // DELETE: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.ID == id) > 0;
        }
    }
}