using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Repositories.GeneralRepo.Interfaces
{
    public interface IConfigValuesByEnumRepo
    {
        string GetString(enumConfigSettingsKeys Key);
        int GetInt(enumConfigSettingsKeys Key);
        bool GetBool(enumConfigSettingsKeys Key);
        long GetLong(enumConfigSettingsKeys Key);
        DateTime GetDatetime(enumConfigSettingsKeys Key);
        decimal GetDecimal(enumConfigSettingsKeys Key);

        bool Update(enumConfigSettingsKeys Key, string Value);
        bool Update(enumConfigSettingsKeys Key, int Value);
        bool Update(enumConfigSettingsKeys Key, bool Value);
        bool Update(enumConfigSettingsKeys Key, long Value);
        bool Update(enumConfigSettingsKeys Key, DateTime Value);
        bool Update(enumConfigSettingsKeys Key, decimal Value);
        Task<bool> SaveAsync();
    }
}
