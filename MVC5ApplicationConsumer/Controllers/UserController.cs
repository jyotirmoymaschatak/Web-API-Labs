using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using MVC5ApplicationConsumer.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;

namespace MVC5ApplicationConsumer.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44370/api");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        // GET: User
        public ActionResult Index()
        {
            List<UserViewModel> modelList = new List<UserViewModel>();
            HttpResponseMessage responseMessage = client.GetAsync(client.BaseAddress+"/user").Result;
            if(responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return View(modelList);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/user", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            UserViewModel modelList = new UserViewModel();
            HttpResponseMessage responseMessage = client.GetAsync(client.BaseAddress + "/user/"+id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<UserViewModel>(data);
            }
            return View("Create", modelList);
        }

        [HttpPut]
        public ActionResult Edit(UserViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/user"+model.UserId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create", model);
        }
        public ActionResult Delete(int id)
        {
            UserViewModel modelList = new UserViewModel();
            HttpResponseMessage responseMessage = client.DeleteAsync(client.BaseAddress + "/user/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}