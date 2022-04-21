
using DMS.DAL.DatabaseContext;
using DMS.DAL.Interfaces;
using DMS.DAL.StaticHelper;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.MainRepo
{
    public class BranchesRepo : BaseModel._AbsGeneralRepositories<MainEntities, bra01branches, int>, IBranchesRepo
    {

        private SystemInfoForSession _ActiveSession;

        public BranchesRepo(MainEntities db) : base(db)
        {
            _ActiveSession = SessionHelper.GetSession();
        }

        public override IQueryable<bra01branches> FilterActive()
        {
            _Query = base.FilterActive().Where(x => x.bra01status);
            return _Query;
        }

        public override IQueryable<bra01branches> FilterDeleted()
        {
            _Query = base.FilterDeleted().Where(x => !x.bra01deleted);
            return _Query;
        }

        public override bool AddDefaultsInsert(bra01branches Data)
        {
            Data.bra01created_date_eng = _ActiveSession.SysDateEng;
            Data.bra01created_date_nep = _ActiveSession.SysDateNep;
            Data.bra01created_name = _ActiveSession.UserId;
            Data.bra01updated_date_eng = _ActiveSession.SysDateEng;
            Data.bra01updated_date_nep = _ActiveSession.SysDateNep;
            Data.bra01updated_by = _ActiveSession.UserId;
            return base.AddDefaultsInsert(Data);
        }

        public override bool AddDefaultsUpdate(bra01branches Data)
        {
            Data.bra01updated_date_eng = _ActiveSession.SysDateEng;
            Data.bra01updated_date_nep = _ActiveSession.SysDateNep;
            Data.bra01updated_by = _ActiveSession.UserId;
            return base.AddDefaultsUpdate(Data);
        }
    }
}
