using Arch.CqrsClient.Attributes;
using Arch.CqrsClient.Commands.Customers;
using Arch.CqrsClient.Models.SearchModel;
using Arch.CqrsClient.Queries.Customers;
using Arch.Infra.Shared.Cqrs;
using Arch.Infra.Shared.DomainNotifications;
using Arch.Infra.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IProcessor _processor;

        public CustomerController(IProcessor processor, IDomainNotification notifications)
            : base(notifications) => _processor = processor;

        public ActionResult Index(Paging paging, string successMessage = null)
        {
            var aa = _processor.Get(new GetCustomersCustomSearch
            {
                Search = new CustomerSearch
                {
                    FirstName = new PropertyComparable<string>("First name test"),
                    LastName = "Last Name",
                    Email = new PropertyComparable<string>("email test", Comparateur.StartWith),
                    BirthDate = new PropertyComparable<DateTime?>(DateTime.Now),
                    Score = new PropertyComparable<int?>(51)
                }
            });


            ViewBag.MessageSuccess = successMessage;
            return View(_processor.Get(new GetCustomersPaging(paging)));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.HasError = false;
            ViewBag.Errors = new List<string>();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateCustomer createCustomer)
        {
            _processor.Send(createCustomer);
            return ViewWithValidation(createCustomer);
        }

        public ActionResult Edit(Guid customerId)
        {
            var customerForEdit = _processor.Get(new GetCustomerForUpdate(customerId));
            return View(customerForEdit);
        }

        [HttpPost]
        public ActionResult Edit(UpdateCustomer updateCustomer)
        {
            _processor.Send(updateCustomer);
            return ViewWithValidation(updateCustomer);
        }

        public ActionResult Delete(Guid id)
        {
            _processor.Send(new DeleteCustomer { Id = id });
            return RedirectToAction("Index");
        }
    }
}