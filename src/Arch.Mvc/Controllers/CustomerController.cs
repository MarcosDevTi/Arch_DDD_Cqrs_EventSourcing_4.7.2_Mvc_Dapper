using Arch.CqrsClient.Commands.Customers;
using Arch.CqrsClient.Queries;
using Arch.CqrsClient.Queries.Customers;
using Arch.Infra.Shared.Cqrs;
using Arch.Infra.Shared.DomainNotifications;
using System;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IProcessor _processor;

        public CustomerController(IProcessor processor, IDomainNotification notifications)
            : base(notifications) => _processor = processor;

        public ActionResult Index()
        {
            return View(_processor.Get(new GetCustomers(5)));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateCustomer createCustomer)
        {
            _processor.Send(createCustomer);
            return RedirectToAction("Index");
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
    }
}