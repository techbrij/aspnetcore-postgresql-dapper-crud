using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ASPCoreSample.Models;
using ASPCoreSample.Repository;

namespace ASPCoreSample.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerRepository customerRepository;

        public CustomerController(IConfiguration configuration)
        {
            customerRepository = new CustomerRepository(configuration);
        }


        public IActionResult Index()
        {
            return View(customerRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public IActionResult Create(Customer cust)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Add(cust);
                return RedirectToAction("Index");
            }
            return View(cust);

        }

        // GET: /Customer/Edit/1
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer obj = customerRepository.FindByID(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST: /Customer/Edit   
        [HttpPost]
        public IActionResult Edit(Customer obj)
        {

            if (ModelState.IsValid)
            {
                customerRepository.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET:/Customer/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            customerRepository.Remove(id.Value);
            return RedirectToAction("Index");
        }
    }
}
