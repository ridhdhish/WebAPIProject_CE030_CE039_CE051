using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConsoleApp1;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult Get(string id)
        {
            User temp = null;
            using (CarEntities entities = new CarEntities())
            {
                try
                {
                    temp = entities.Users.FirstOrDefault(u => u.email == id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {

                        return Ok(entities.Users.Where(u => u.email == id).FirstOrDefault());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not find User");
                }
            }
        }
        public IEnumerable<User> Get()
        {
            using (CarEntities entities = new CarEntities())
            {
                return entities.Users.ToList();
            }
        }

        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody] User user)
        {
            User temp = null;
            using (CarEntities entities = new CarEntities())
            {

                try
                {
                    temp = entities.Users.FirstOrDefault(u => u.email == id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        temp.password = temp.password;
                        temp.email = user.email;
                        

                        entities.SaveChanges();
                        return Ok(entities.Users.ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not update User");
                }
            }
        }

        public IHttpActionResult Delete(string id)
        {
            User temp;
            using (CarEntities entities = new CarEntities())
            {

                try
                {
                    temp = entities.Users.FirstOrDefault(u => u.email == id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        entities.Users.Remove(entities.Users.FirstOrDefault(u => u.email == id));
                        entities.SaveChanges();
                        return Ok(entities.Users.ToList());
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Unable to Delete user");
                }
            }
        }

        public IHttpActionResult Get(string id, string password)
        {
            User temp;
            using (CarEntities entities = new CarEntities())
            {
                try
                {
                    temp = entities.Users.FirstOrDefault(u => u.email == id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (temp.email.Equals(id) && temp.password.Equals(password))
                        {
                            return Ok("Login Successful");
                        }
                        else
                        {
                            return BadRequest("Login Failed");
                        }

                    }
                }


                catch (Exception e)
                {
                    return BadRequest("Login Failed");
                }
            }
        }


        public IHttpActionResult Post([FromBody] User user)
        {
            using (CarEntities entities = new CarEntities())
            {
                try
                {
                    if (entities.Users.FirstOrDefault(u => u.email == user.email) == null)
                    {
                        entities.Users.Add(user);
                        entities.SaveChanges();
                        return Ok(entities.Users.ToList());
                    }
                    else
                    {
                        return BadRequest("Can not insert the User");
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("Can not insert the User");
                }
            }
        }
    }

}
