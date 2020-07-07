using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Algolia.Application.Domains.CreateIndex;
using Algolia.Application.Enums;
using Algolia.Application.Models.CreateIndex;
using Algolia.Application.Models.IndexSettings;
using Algolia.Application.Models.UpdateIndex;
using Algolia.Search.Models.Search;
using Algolia.TestData;
using Microsoft.AspNetCore.Mvc;

namespace Algolia.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IIndexService _indexService;

        public HomeController(IIndexService indexService)
        {
            _indexService = indexService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Search()
        {
            return View(new SearchIndexRequestDto()
            {
                IndexName = "Products"
            });
        }
        public async Task<IActionResult> CreateIndex()
        {
            //DUMMY DATA DUMMY DATA DUMMY DATA
            //DUMMY DATA DUMMY DATA DUMMY DATA
            //DUMMY DATA DUMMY DATA DUMMY DATA
            List<Customer> customers = new List<Customer>();
            List<Product> products = new List<Product>();
            List<Order> orders = new List<Order>();
            List<Category> categories = new List<Category>();

            for (int i = 1; i <= 10; i++)
            {
                Customer customer = new Customer();
                customer.Firstname = "Firstname" + i;
                customer.Lastname = "Lastname" + i;
                customer.CustomerId = i;
                customers.Add(customer);
            }
            for (int i = 1; i <= 4; i++)
            {
                Category category = new Category();
                category.CategoryId = i;
                category.CategoryName = "Category" + i;
                categories.Add(category);
            }
            for (int i = 1; i <= 30000; i++)
            {
                Random rand = new Random();
                Product product = new Product();
                product.ProductId = i;
                product.Name = "Product" + i;
                product.CategoryId = rand.Next(1, 4);
                product.Price = (rand.NextDouble() + .01) * 1000;
                products.Add(product);
            }
            for (int i = 1; i <= 30000; i++)
            {
                Order order = new Order();
                Random rand = new Random();
                var randProductId = rand.Next(1, 30000);
                var randCustomerd = rand.Next(1, 10);
                order.ProductId = products.FirstOrDefault(x => x.ProductId == randProductId).ProductId;
                order.CustomerId = customers.FirstOrDefault(x => x.CustomerId == randCustomerd).CustomerId;
                order.OrderDate = DateTime.Now.AddDays(i);
                order.OrderId = i;
                orders.Add(order);
            }

            var response = await _indexService.CreateIndexAsync<Order>(new CreateIndexRequestDto<Order>()
            {
                Data = orders,
                IndexName = "Orders"
            });
            var response1 = await _indexService.CreateIndexAsync<Category>(new CreateIndexRequestDto<Category>()
            {
                Data = categories,
                IndexName = "Categories"
            });
            var response2 = await _indexService.CreateIndexAsync<Product>(new CreateIndexRequestDto<Product>()
            {
                Data = products,
                IndexName = "Products"
            });
            var response3 = await _indexService.CreateIndexAsync<Customer>(new CreateIndexRequestDto<Customer>()
            {
                Data = customers,
                IndexName = "Customers"
            });
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> UpdateIndexSettings()
        {
            //This settings can be edit on Algolia dashboard
            IndexOptions options = new IndexOptions();
            List<SearchableAttribute> searchableAttributes = new List<SearchableAttribute>();
            List<CustomRankingAttribute> customRankingAttributes = new List<CustomRankingAttribute>();

            CustomRankingAttribute customAttributeSalary = new CustomRankingAttribute();
            customAttributeSalary.Attribute = "Salary";
            customAttributeSalary.RankingType = RankingFunctionsEnum.Asc;

            SearchableAttribute attributeSalary = new SearchableAttribute();
            attributeSalary.Attribute = "Salary";
            attributeSalary.SearchableAttributeFunctionType = SearchableAttributeFunctionsEnum.Ordered;

            SearchableAttribute attributeFirstname = new SearchableAttribute();
            attributeFirstname.Attribute = "Firstname";

            searchableAttributes.Add(attributeSalary);
            searchableAttributes.Add(attributeFirstname);
            customRankingAttributes.Add(customAttributeSalary);
            options.CustomRankingAttributes = customRankingAttributes;
            options.SearchableAttributes = searchableAttributes;

            var response = await _indexService.UpdateIndexSettings<Employee>(new UpdateIndexSettingsRequestDto()
            {
                IndexName = "Employees",
                IndexOptions = options

            });
            TempData["Message"] = response.Message;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateRecords()
        {
            //DUMMY CUSTOMER DATA
            //DUMMY CUSTOMER DATA
            List<Customer> customers = new List<Customer>();
            for (int i = 11; i <= 200000; i++)
            {
                Customer customer = new Customer();
                customer.Firstname = "Firstname" + i;
                customer.Lastname = "Lastname" + i;
                customer.CustomerId = i;
                customers.Add(customer);
            }
            var response = await _indexService.UpdateRecordsAsync(new UpdateIndexRecordsRequestDto<Customer>()
            {
                Data = customers,
                IndexName = "Customers"
            });
            TempData["Message"] = response.Message;
            return RedirectToAction("Index", "Home");

        }
    }
}