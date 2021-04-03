using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConsoleApp1;

namespace WebAPI.Controllers
{
    public class CarController : ApiController
    {
        public CarController()
        {
        }

        public IHttpActionResult Get(string userid, int carid)
        {
            Car temp = null;
            using (CarEntities entities = new CarEntities())
            {
                try
                {
                    temp = entities.Cars.Where(t => t.Id == carid).Where(t => t.email == userid).FirstOrDefault();
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {

                        return Ok(entities.Cars.Where(t => t.Id == carid).Where(t => t.email == userid).FirstOrDefault());
                    }
                }
                catch (Exception e)
                {
                    return BadRequest("Can not find ToDo");
                }
            }
        }
        public IEnumerable<Car> Get(string userid)
        {
            using (CarEntities entities = new CarEntities())
            {
                return entities.Cars.Where(t => t.email == userid).ToList();
            }
        }

        public IEnumerable<Car> Get()
        {
            using (CarEntities entities = new CarEntities())
            {
                return entities.Cars.ToList();
            }
        }

        public IHttpActionResult Post([FromBody] Car car)
        {
            using (CarEntities entities = new CarEntities())
            {
                try
                {
                    entities.Cars.Add(car);
                    entities.SaveChanges();
                    return Ok(entities.Cars.Where(t => t.email == car.email).ToList());
                }
                catch (Exception e)
                {
                    return BadRequest("Can not add the car");
                }
            }
        }

        [HttpPut]
        public IHttpActionResult Put(string userid, [FromBody] Car todo)
        {
            Car temp = null;
            using (CarEntities entities = new CarEntities())
            {

                try
                {
                    temp = entities.Cars.Where(t => t.name == todo.name).FirstOrDefault();
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {                        
                        temp.email = todo.email;


                        entities.SaveChanges();
                        return Ok(entities.Cars.Where(t => t.email == userid).ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not insert Car");
                }
            }
        }

        public IHttpActionResult Put(string userid, int carId)
        {
            Car temp = null;
            using (CarEntities entities = new CarEntities())
            {

                try
                {
                    temp = entities.Cars.Where(t => t.Id == carId).FirstOrDefault();
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        temp.email = "Not rented";


                        entities.SaveChanges();
                        return Ok(entities.Cars.Where(t => t.email == userid).ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not insert Car");
                }
            }
        }

        public IHttpActionResult Delete(string userid, int id)
        {
            Car temp;
            using (CarEntities entities = new CarEntities())
            {

                try
                {
                    temp = entities.Cars.Where(t => t.Id == id).Where(t => t.email == userid).FirstOrDefault();
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        entities.Cars.Remove(entities.Cars.Where(t => t.Id == id).Where(t => t.email == userid).FirstOrDefault());
                        entities.SaveChanges();
                        return Ok(entities.Cars.Where(t => t.email == userid).ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Unable to Delete todo");
                }
            }
        }
    }
}
