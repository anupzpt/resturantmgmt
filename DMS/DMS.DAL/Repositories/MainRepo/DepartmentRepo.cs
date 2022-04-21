using DMS.DAL.DatabaseContext;
using DMS.DAL.Interface;
using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.MainRepo
{
    public class DepartmentRepo : BaseModel._AbsGeneralRepositories<MainEntities, dep01department, int>, IDepartmentRepo
    {

        private SystemInfoForSession _ActiveSession;

        public DepartmentRepo(MainEntities db) : base(db)
        {
            _ActiveSession = SessionHelper.GetSession();
        }

        public override IQueryable<dep01department> FilterActive()
        {
            _Query = base.FilterActive().Where(x => x.dep01status);
            return _Query;
        }

        public override IQueryable<dep01department> FilterDeleted()
        {
            _Query = base.FilterDeleted().Where(x => !x.dep01deleted);
            return _Query;
        }

        public override bool AddDefaultsInsert(dep01department Data)
        {
            Data.dep01created_date_eng = _ActiveSession.SysDateEng;
            Data.dep01created_date_nep = _ActiveSession.SysDateNep;
            Data.dep01created_by = _ActiveSession.UserId;
            Data.dep01updated_date_eng = _ActiveSession.SysDateEng;
            Data.dep01updated_date_nep = _ActiveSession.SysDateNep;
            Data.dep01updated_by = _ActiveSession.UserId;
            Data.dep01deleted = false;
            return base.AddDefaultsInsert(Data);
        }

        public override bool AddDefaultsUpdate(dep01department Data)
        {
            Data.dep01updated_date_eng = _ActiveSession.SysDateEng;
            Data.dep01updated_date_nep = _ActiveSession.SysDateNep;
            Data.dep01updated_by = _ActiveSession.UserId;
            return base.AddDefaultsUpdate(Data);
        }

        public override bool Delete(dep01department Data)
        {
            Data.dep01deleted = true;
            return Update(Data);
        }
    }
}
