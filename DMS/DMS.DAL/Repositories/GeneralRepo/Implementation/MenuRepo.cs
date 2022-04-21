using DMS.DAL.DatabaseContext;
using DMS.DAL.EntityModels;
using DMS.DAL.Repositories.GeneralRepo.Interfaces;
using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.GeneralRepo.Implementation
{
    public class MenuRepo : IMenuRepo
    {
        private IdentityEntities _db = new IdentityEntities();

        public void SaveMenuListWithParent(List<MenuList> model)
        {
            //var systemSession = (SystemInfoForSession)HttpContext.Current.Session["SystemSession"];
            foreach (var item in model)
            {
                var menuData = _db.MenuLists.FirstOrDefault(x => x.Id == item.Id);
                menuData.ParentId = item.ParentId == 0 ? null : item.ParentId;
                menuData.IsActive = item.IsActive;
                //menuData.UpdatedBy = systemSession.UserId;
                menuData.UpdatedNepaliDate = SystemInfo.NepaliDate;
                menuData.UpdatedEnglishDate = SystemInfo.EnglishDate;
                _db.SaveChanges();
            }
        }
    }
}