using DMS.DAL.DatabaseContext;
using DMS.DAL.Interface;
using DMS.DAL.StaticHelper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.MainRepo
{
    public class DesignationRepo : BaseModel._AbsGeneralRepositories<MainEntities, des01designations, int>, IDesignationRepo
    {

        private SystemInfoForSession _ActiveSession;

        public DesignationRepo(MainEntities db) : base(db)
        {
            _ActiveSession = SessionHelper.GetSession();
        }

        public override IQueryable<des01designations> FilterActive()
        {
            _Query = base.FilterActive().Where(x => x.des01status);
            return _Query;
        }

        public override IQueryable<des01designations> FilterDeleted()
        {
            _Query = base.FilterDeleted().Where(x => !x.des01deleted);
            return _Query;
        }

        public override bool AddDefaultsInsert(des01designations Data)
        {
            Data.des01created_date_eng = _ActiveSession.SysDateEng;
            Data.des01created_date_nep = _ActiveSession.SysDateNep;
            Data.des01created_by = _ActiveSession.UserId;
            Data.des01updated_date_eng = _ActiveSession.SysDateEng;
            Data.des01updated_date_nep = _ActiveSession.SysDateNep;
            Data.des01updated_by = _ActiveSession.UserId;
            Data.des01deleted = false;
            return base.AddDefaultsInsert(Data);
        }

        public override bool AddDefaultsUpdate(des01designations Data)
        {
            Data.des01updated_date_eng = _ActiveSession.SysDateEng;
            Data.des01updated_date_nep = _ActiveSession.SysDateNep;
            Data.des01updated_by = _ActiveSession.UserId;
            return base.AddDefaultsUpdate(Data);
        }

        public override bool Delete(des01designations Data)
        {
            Data.des01deleted = true;
            return Update(Data);
        }
    }
}
