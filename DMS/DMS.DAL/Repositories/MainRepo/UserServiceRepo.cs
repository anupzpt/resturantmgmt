using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.DAL.DatabaseContext;
using DMS.DAL.Interfaces;
using DMS.DAL.StaticHelper;

namespace DMS.DAL.Repositories.MainRepo
{
    public class UserServiceRepo : BaseModel._AbsGeneralRepositories<MainEntities, usr05users, string>, IUserRepo
    {

        private SystemInfoForSession _ActiveSession;

        public UserServiceRepo(MainEntities db) : base(db)
        {
            _ActiveSession = SessionHelper.GetSession();
        }

        public override IQueryable<usr05users> FilterActive()
        {
            _Query = base.FilterActive().Where(x => x.usr05status);
            return _Query;
        }

        public override IQueryable<usr05users> FilterDeleted()
        {
            _Query = base.FilterDeleted().Where(x => !x.usr05deleted);
            return _Query;
        }

        public override bool AddDefaultsInsert(usr05users Data)
        {
            Data.usr05created_date = _ActiveSession.SysDateEng;
            Data.usr05created_by = _ActiveSession.UserId;
            Data.usr05updated_date = _ActiveSession.SysDateEng;
            Data.usr05updated_by = _ActiveSession.UserId;
            Data.usr05deleted = false;
            return base.AddDefaultsInsert(Data);
        }

        public override bool AddDefaultsUpdate(usr05users Data)
        {
            Data.usr05updated_date = _ActiveSession.SysDateEng;
            Data.usr05updated_by = _ActiveSession.UserId;
            return base.AddDefaultsUpdate(Data);
        }

        public override bool Delete(usr05users Data)
        {
            Data.usr05deleted = true;
            Data.usr05status = false;
            return Update(Data);
        }
    }
}
