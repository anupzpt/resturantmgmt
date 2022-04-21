using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GenericRepo;
using DMS.UI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMS.UI.Services.General.Interface
{
    public interface IMenuServices : IGenericRepo<MenuList, string>
    {
        List<MenuInfoVM> getAllMenuList();

        void SaveMenuListWithParent(List<MenuInfoVM> _menuinfo);

        //List<MenuListVM> MenuListForDisplay(string userId, string roleId);

        void SaveEditedSingleMenuDetails(MenuListVM menu);
        IList<MenuListVM> GetAllMenuListbyEmpType();
        MenuListVM GetMenuById(int id);
        List<MenuListVM> GetAllParentMenu();
    }
}