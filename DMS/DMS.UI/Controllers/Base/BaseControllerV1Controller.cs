﻿using DMS.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers
{
    public class BaseControllerV1Controller<MainService, VMDataType, MainDataType, PKType> :
         _BaseAuthenticatedController
         where MainService : BaseModel.IGeneralService<VMDataType, PKType>
         where VMDataType : class, new()
    {

        public readonly MainService _MainService;
        public BaseControllerV1Controller(MainService MainService) : base()
        {
            _MainService = MainService;
        }
        // GET: Base
        public virtual async Task<ActionResult> Index()
        {
            IEnumerable<VMDataType> Data = null;
            try
            {
                Data = await _MainService.GetList().ToListAsync();

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return View(Data);
        }
        protected virtual async Task InitCommon(VMDataType Data)
        {
            //will be overwritten in child inheritance
        }
        public virtual async Task<ActionResult> Create()
        {
            VMDataType Data = new VMDataType();
            try
            {
                //Data = await _MainService.ListAll();
                await InitCommon(Data);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return View(Data);
        }
        [HttpPost]
        public virtual async Task<ActionResult> Create(VMDataType Data)
        {
            try
            {
                //Data = await _MainService.ListAll();
                _MainService.Insert(Data);
                FlashBag.setMessage(true, "Data Created successfully.");
                return RedirectToAction("Index");

                //in case of error it will be thrown to catch block
            }
            catch (Exception ex)
            {
                await InitCommon(Data);
                ModelState.AddModelError("", ex.Message);
            }
            return View(Data);
        }
        public virtual async Task<ActionResult> Edit(PKType id)
        {
            VMDataType Data = default(VMDataType);
            try
            {
                Data = _MainService.GetDetail(id);
                await InitCommon(Data);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return View(Data);
        }
        [HttpPost]
        public virtual async Task<ActionResult> Edit(VMDataType Data)
        {
            try
            {
                //Data = await _MainService.ListAll();
                _MainService.Update(Data);
                FlashBag.setMessage(true, "Data Updated successfully.");
                return RedirectToAction("Index");

                //in case of error it will be thrown to catch block
            }
            catch (Exception ex)
            {
                await InitCommon(Data);
                ModelState.AddModelError("", ex.Message);
            }
            return View(Data);
        }
        public virtual async Task<ActionResult> Detail(PKType id)
        {
            VMDataType Data = default(VMDataType);
            try
            {
                Data = _MainService.GetDetail(id);
                await InitCommon(Data);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(Data);
        }


    }
}