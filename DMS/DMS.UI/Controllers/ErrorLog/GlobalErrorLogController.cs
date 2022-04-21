using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GenericRepo;
using DMS.UI.ViewModels.ErrorLog;

namespace DMS.UI.Controllers.ErrorLog
{
    public class GlobalErrorLogController : Controller
    {
        private readonly ICFGenericRepo<GlobalErrorLog, string> _globalerror;

        public GlobalErrorLogController(ICFGenericRepo<GlobalErrorLog, string> globalerror)
        {
            _globalerror = globalerror;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var globalerrordata = Mapper.Map<List<GlobalErrorLogVM>>(_globalerror.GetAllSync());
                return View(globalerrordata.OrderByDescending(x => x.EnglishDate).Take(200));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: GlobalErrorLog/Create
        public ActionResult Details(string id)
        {
            try
            {
                var data = _globalerror.GetByIdSync(id);
                return View(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        // GET: GlobalErrorLog/Create
        public ActionResult Delete(string id)
        {
            try
            {
                _globalerror.DeleteData(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}