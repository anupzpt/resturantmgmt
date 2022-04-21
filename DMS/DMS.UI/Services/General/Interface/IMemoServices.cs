using DMS.DAL.DatabaseContext;
using DMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services.General.Interface
{
    public interface IMemoServices
    {
        Task<List<Memo_VM>> GetAllMemo();

        Task<List<Memo>> GetAllValidMemo();

        Task<List<Memo_VM>> GetAllValidMemoByBranch();

        void AddMemo(Memo_VM memo_VM);

        void DeleteMemo(int id);

        void EditMemo(Memo_VM memo_VM);

        Task<Memo_VM> GetMemobyId(int id);
    }
}