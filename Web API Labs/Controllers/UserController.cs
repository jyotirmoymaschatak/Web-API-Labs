using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API_Labs.Database;
using Web_API_Labs.Models;

namespace Web_API_Labs.Controllers
{
    public class UserController : ApiController
    {
        //[HttpGet]
        //public string Greet (string Name)
        //{
        //    return "Welcome" + Name; 
        //}

        DatabaseContext db = new DatabaseContext();

        //api/user
        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }
        //api/user/2
        public User GetUser(int id)
        {
            return db.Users.Find(id);
        }

        //api/user
        [HttpPost]
        public HttpResponseMessage AddUser(User model)
        {
            try
            {
                db.Users.Add(model);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateUser(int id, User model)
        {
            try
            {
                if(id==model.UserId)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        //api/user
        public HttpResponseMessage DeleteUser(int id)
        {
            User usr = db.Users.Find(id);
            if(usr != null)
            {
                db.Users.Remove(usr);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}
