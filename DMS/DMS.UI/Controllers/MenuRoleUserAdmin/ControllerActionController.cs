using DMS.UI.Services.General.Interface;
using DMS.UI.Services.Helper;
using DMS.UI.ViewModels;
using System;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace IMS.UI.Controllers.MenuRoleUser_Admin
{
    public class ControllerActionController : Controller
    {
        // GET: ControllerAction
        private IControllerActionServices _controllerAction;

        public ControllerActionController(IControllerActionServices controllerAction)
        {
            _controllerAction = controllerAction;
        }

        //Update Controller Action Names From The System To Database
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var allControllerAction = await _controllerAction.GetAllControllerActionList();
            return View(allControllerAction);

        }
        public ActionResult CreateControllerAction()
        {
            var allcotroller = GetAllControllerAndAction.getAllControllerActionList();
            _controllerAction.SaveControllerActionNameBySystem(allcotroller);
            TempData["Message"] = "Process Successfully";
            return RedirectToAction("EditControllerActionList");
        }

        [HttpGet]
        public ActionResult DisplayPage() => View();

        public async Task<ActionResult> DisplayList()
        {
            var allControllerAction = await _controllerAction.GetAllControllerActionList();
            return PartialView("~/Views/ControllerAction/EditorTemplates/IndexPagePartialView.cshtml", allControllerAction);
        }

        [HttpGet]
        public async Task<ActionResult> EditControllerActionList()
        {
            var allControllerAction = new ControllerActionVM();
            allControllerAction.controllerActionList = await _controllerAction.GetAllControllerActionList();
            return View(allControllerAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditControllerActionList(ControllerActionVM controllerAction)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    _controllerAction.SaveEditedControllerAction(controllerAction.controllerActionList);
                    ts.Complete();
                    ts.Dispose();
                    TempData["Message"] = "Process Successfully";
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Error Occure .. Process Not Complete" + ex;
                    ts.Dispose();
                    throw ex;
                }
            }
            return RedirectToAction("DisplayPage");
        }
    }
}