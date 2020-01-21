using Arch.CqrsClient.Queries.EventSourcing;
using Arch.Infra.Shared.Cqrs;
using System;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class EventSourcingController : Controller
    {
        private readonly IProcessor _processor;
        public EventSourcingController(IProcessor processor)
        {
            _processor = processor;
        }
        public ActionResult History(Guid aggregateId, string controllerName, string actionName)
        {
            var history = _processor.Get(new GetHistory(aggregateId));
            return View(history);
        }
    }
}