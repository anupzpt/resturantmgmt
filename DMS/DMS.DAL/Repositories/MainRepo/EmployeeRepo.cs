using DMS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.DAL.DatabaseContext;
using System.Data.Entity;
using DMS.DAL.StaticHelper;

namespace DMS.DAL.Repositories.MainRepo
{
    public class EmployeeRepo : BaseModel._AbsGeneralRepositories<MainEntities, emp01employee, int>, IEmployeeRepo
    {

        private SystemInfoForSession _ActiveSession;

        public EmployeeRepo(MainEntities db) : base(db)
        {
            _ActiveSession = SessionHelper.GetSession();
        }

        public override IQueryable<emp01employee> FilterActive()
        {
            _Query = base.FilterActive().Where(x => x.emp01status);
            return _Query;
        }

        public override IQueryable<emp01employee> FilterDeleted()
        {
            _Query = base.FilterDeleted().Where(x => !x.emp01deleted);
            return _Query;
        }

        public override bool AddDefaultsInsert(emp01employee Data)
        {
            Data.emp01created_date_eng = _ActiveSession.SysDateEng;
            Data.emp01created_date_nep = _ActiveSession.SysDateNep;
            Data.emp01created_by = _ActiveSession.UserId;
            Data.emp01updated_date_nep = _ActiveSession.SysDateNep;
            Data.emp01updated_by = _ActiveSession.UserId;
            Data.emp01deleted = false;
            return base.AddDefaultsInsert(Data);
        }

        public override bool AddDefaultsUpdate(emp01employee Data)
        {
            Data.emp01updated_date_nep = _ActiveSession.SysDateNep;
            Data.emp01updated_by = _ActiveSession.UserId;
            return base.AddDefaultsUpdate(Data);
        }

        public override bool Delete(emp01employee Data)
        {
            Data.emp01deleted = true;
            return Update(Data);
        }
    }
}
