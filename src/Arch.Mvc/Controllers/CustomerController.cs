using Arch.CqrsClient.Commands.Customers;
using Arch.CqrsClient.Queries;
using Arch.Infra.Shared.Cqrs;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IProcessor _processor;

        public CustomerController(IProcessor processor) => _processor = processor;

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
            return View(createCustomer);
        }


    }
}