using DMS.UI.Services.General.Interface;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMS.DAL.Repositories.GenericRepo;
using DMS.DAL.EntityModels;
using DMS.UI.ViewModels;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.DatabaseContext;
using System;

namespace DMS.UI.Services.General.Implementation
{
    public class ControllerActionServices : GenericRepo<ControllerAction, int>, IControllerActionServices
    {
        private readonly IControllerActionRepo _controllerActionRepo;
        private readonly ICFGenericRepo<ApplicationRole, string> _roleRepo;
        private readonly IGenericRepo<ActionRole, string> _actionRoleRepo;

        public ControllerActionServices(
             IControllerActionRepo controllerActionRepo, CFGenericRepo<ApplicationRole, string> roleRepo,
             GenericRepo<ActionRole, string> actionRoleRepo)
        {
            _controllerActionRepo = controllerActionRepo;
            _roleRepo = roleRepo;
            _actionRoleRepo = actionRoleRepo;
        }

        public async Task<List<ControllerActionVM>> GetAllControllerActionList()
        {
            var model = Mapper.Map<List<ControllerActionVM>>(await _controllerActionRepo.GetAll());
            return model;
        }

        public ControllerActionVM GetControllerActionById(int id)
        {
            var controllerActionData = Mapper.Map<ControllerActionVM>(_controllerActionRepo.GetByIdSync(id));
            return controllerActionData;
        }

        public List<ControllerActionVM> GetAllActionDataByController(int controllerId)
        {
            var controllerName = FirstOrDefaultSync(x => x.Id == controllerId).ControllerName;
            var ControllerActionList = GetSync(x => x.ControllerName == controllerName ).ToList(); //&& (x.Attributes.Contains("HttpGet") == true) && (x.ReturnType == "ActionResult" || x.ReturnType == "Task`1")
            var model = Mapper.Map<List<ControllerActionVM>>(ControllerActionList);
            return model;
        }

        //For Save ControllerAndActionName By System
        public void SaveControllerActionNameBySystem(List<ControllerActionVM> controllerList)
        {
            var model = Mapper.Map<List<ControllerAction>>(controllerList);
            foreach (var item in model)
            {
                try
                {
                    if (!item.ControllerName.Contains("Controller")) { continue; }
                    var controllerName = item.ControllerName;
                    var ControllerNameOnly = controllerName.Remove(controllerName.Length - 10).ToUpper();
                    var SelectedList = GetSync(x => x.ControllerName.ToUpper() == ControllerNameOnly && x.ActionName.ToUpper() == item.ActionName.ToUpper()).FirstOrDefault();
                    if (SelectedList == null)
                    {
                        item.ControllerName = ControllerNameOnly;
                        AddSync(item);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public void SaveEditedControllerAction(List<ControllerActionVM> controllerActionList)
        {
            var model = Mapper.Map<List<ControllerAction>>(controllerActionList.ToList());
            foreach (var item in model)
            {
                var data = GetByIdSync(item.Id);
                if (item.ActiveAllTime != data.ActiveAllTime)
                {
                    data.ActiveAllTime = item.ActiveAllTime;
                    Update(data);
                }
            }
        }

        //GetAllActionControllerWithRoleAccess  in List
        public async Task<List<ActionRoleVM>> GetAllControllerActionRoleForEdit(string RoleId)
        {
            var AllControllerActionList = await _controllerActionRepo.GetAll();
            var AllControllerActionRoleList = await _actionRoleRepo.Get(x => x.RoleId == RoleId);
            List<ActionRoleVM> _actionRepoRoleList = new List<ActionRoleVM>();
            foreach (var item in AllControllerActionList)
            {
                var SelectOnlyControllerActionIdMatch = AllControllerActionRoleList.Where(x => x.ActionId == item.Id).FirstOrDefault();
                ActionRoleVM actionRole = new ActionRoleVM();
                actionRole.ActionId = item.Id;
                actionRole.RoleId = RoleId;
                actionRole.controllerActionVm = Mapper.Map<ControllerActionVM>(item);
                actionRole.IsActive = (SelectOnlyControllerActionIdMatch != null ? SelectOnlyControllerActionIdMatch.IsActive : false);
                _actionRepoRoleList.Add(actionRole);
            }
            return _actionRepoRoleList;
        }
    }
}