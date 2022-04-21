using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services
{
    public interface IGenericService<VMDataType, PKType>
    {
        Task<List<VMDataType>> ListAll();
        void Create(VMDataType Data);
        Task<VMDataType> Detail(PKType id);
        void Update(VMDataType Data);
    }
}
