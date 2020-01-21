using Arch.CqrsClient.Commands.Inserts;
using Arch.Infra.Shared.Cqrs;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class InsertController : Controller
    {
        private readonly IProcessor _processor;
        public InsertController(IProcessor processor) => _processor = processor;
        public ActionResult Index() => View();

        public ActionResult InsertListLot500()
        {
            _processor.Send(new InsertVolumeCustomers(500));
            return RedirectToAction("Index", "Customer", new { });
        }

        public ActionResult InsertListLot5000()
        {
            _processor.Send(new InsertVolumeCustomers(5000));
            return RedirectToAction("Index", "Customer", new { });
        }

        public ActionResult TruncateTableCustomers()
        {
            _processor.Send(new TrucateCustomers());
            return RedirectToAction("Index", "Customer", new { });
        }

    }
}