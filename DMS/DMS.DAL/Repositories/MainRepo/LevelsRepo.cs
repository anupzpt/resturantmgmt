using DMS.DAL.DatabaseContext;
using DMS.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DMS.DAL.StaticHelper;

namespace DMS.DAL.Repositories.MainRepo
{
    public class LevelsRepo : BaseModel._AbsGeneralRepositories<MainEntities, lvl01employee_levels, int>, ILevelsRepo
    {

        private SystemInfoForSession _ActiveSession;

        public LevelsRepo(MainEntities db) : base(db)
        {
            _ActiveSession = SessionHelper.GetSession();
        }

        public override IQueryable<lvl01employee_levels> FilterActive()
        {
            _Query = base.FilterActive().Where(x => x.lvl01status);
            return _Query;
        }

        public override IQueryable<lvl01employee_levels> FilterDeleted()
        {
            _Query = base.FilterDeleted().Where(x => !x.lvl01deleted);
            return _Query;
        }

        public override bool AddDefaultsInsert(lvl01employee_levels Data)
        {
            Data.lvl01created_date_eng = _ActiveSession.SysDateEng;
            Data.lvl01created_date_nep = _ActiveSession.SysDateNep;
            Data.lvl01created_by = _ActiveSession.UserId;
            Data.lvl01updated_date_eng = _ActiveSession.SysDateEng;
            Data.lvl01updated_date_nep = _ActiveSession.SysDateNep;
            Data.lvl01updated_by = _ActiveSession.UserId;
            Data.lvl01deleted = false;
            return base.AddDefaultsInsert(Data);
        }

        public override bool AddDefaultsUpdate(lvl01employee_levels Data)
        {
            Data.lvl01updated_date_eng = _ActiveSession.SysDateEng;
            Data.lvl01updated_date_nep = _ActiveSession.SysDateNep;
            Data.lvl01updated_by = _ActiveSession.UserId;
            return base.AddDefaultsUpdate(Data);
        }

        public override bool Delete(lvl01employee_levels Data)
        {
            Data.lvl01deleted = true;
            return Update(Data);
        }
    }
}
