using AutoMapper;
using DMS.DAL.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DMS.Services
{
    public abstract class _AbsGeneralService<MainRepo, VMDataType, MainDataType, PKType> : IGenericService<VMDataType, PKType>
        where VMDataType : class
        where MainDataType : class
        where MainRepo : IGenericRepo<MainDataType, PKType>
    {
        public readonly MainRepo _MainRepo;
        public _AbsGeneralService(MainRepo MainRepo)
        {
            _MainRepo = MainRepo;
        }

        public virtual async Task<List<VMDataType>> ListAll()
        {
            List<MainDataType> BranchList = await _MainRepo.GetAll();
            List<VMDataType> Branch_VMList = Mapper.Map<List<VMDataType>>(BranchList);
            return Branch_VMList;
        }
        public virtual void Create(VMDataType Data)
        {
            MainDataType MainData = Mapper.Map<MainDataType>(Data);
            _MainRepo.AddSync(MainData);
        }
        public virtual async Task<VMDataType> Detail(PKType id)
        {
            MainDataType Data = await _MainRepo.GetById(id);
            return Mapper.Map<VMDataType>(Data);
        }
        public virtual void Update(VMDataType Data)
        {
            MainDataType MainData = Mapper.Map<MainDataType>(Data);
            _MainRepo.UpdateSync(MainData);
        }
    }
}