using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WebApp.Core.Models;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly string apiEndpoint = "http://localhost:5299/api/customer";

        public ActionResult Index()
        {
            IEnumerable<CustomerViewModel> customerList = [];
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiEndpoint);
                var response = client.GetAsync(apiEndpoint);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResult = result.Content.ReadFromJsonAsync<IList<CustomerViewModel>>();
                    readResult.Wait();

                    customerList = readResult.Result.AsEnumerable().OrderBy(x => x.Id);
                }
                else
                {
                    customerList = Enumerable.Empty<CustomerViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }

            return View(customerList);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel customer)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiEndpoint);
                    var postTask = client.PostAsJsonAsync<CustomerViewModel>("customer/create", customer);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Failed to create customer");
                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            CustomerViewModel customer = new();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiEndpoint);
                var response = client.GetAsync("customer/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResult = result.Content.ReadFromJsonAsync<CustomerViewModel>();
                    readResult.Wait();

                    customer = readResult.Result;
                }
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiEndpoint);
                var postTask = client.PostAsJsonAsync<CustomerViewModel>("customer/update", customer);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Failed to update customer");
            return View(customer);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiEndpoint);
                var postTask = client.DeleteAsync("customer/delete/" + id);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}
