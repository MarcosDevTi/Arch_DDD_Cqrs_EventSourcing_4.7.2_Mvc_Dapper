using Arch.Infra.Shared.Cqrs.Contracts;
using Arch.Infra.Shared.DomainNotifications;
using System.Linq;
using System.Web.Mvc;

namespace Arch.Mvc.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotification _notifications;

        public BaseController(IDomainNotification notifications) => _notifications = notifications;

        public bool IsValidOperation() => !_notifications.HasNotifications();

        public ActionResult ViewWithValidation(CommandAction command)
        {
            ViewBag.Errors = _notifications.GetNotifications().Select(_ => _.Value).ToList();
            ViewBag.HasError = _notifications.HasNotifications();
            if (_notifications.HasNotifications())
                return View(command);
            return RedirectToAction("Index", new { successMessage = "Operation Successfully Completed" });
        }
    }
}