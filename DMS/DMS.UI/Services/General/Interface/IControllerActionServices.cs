using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GenericRepo;
using DMS.UI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMS.UI.Services.General.Interface
{
    public interface IControllerActionServices : IGenericRepo<ControllerAction, int>
    {
        Task<List<ControllerActionVM>> GetAllControllerActionList();

        ControllerActionVM GetControllerActionById(int actionId);

        List<ControllerActionVM> GetAllActionDataByController(int controllerId);

        void SaveControllerActionNameBySystem(List<ControllerActionVM> controllerList);

        void SaveEditedControllerAction(List<ControllerActionVM> controllerActionList);

        Task<List<ActionRoleVM>> GetAllControllerActionRoleForEdit(string Id);
    }
}