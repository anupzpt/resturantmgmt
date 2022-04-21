using System;
using DMS.DAL.Helpers;

namespace DMS.DAL.StaticHelper
{
    public static class SystemInfo
    {
        public static string NepaliDate /*{ get; set; }*/= "";// NepCalender.ConvertToNepali(DateTime.Now);

        public static string EnglishDate /*{ get; set; }*/ = DateTime.Now.ToString("yyyy-MM-dd");
        public static DateTime SystemDate /*{ get; set; }*/ = DateTime.Now;
        public static bool holidayTransection;
        public static string CoopNepDate { get; set; }
        public static bool IsBeginOFDay { get; set; }
        public static string RedirectAction { get; set; }
        public static bool IsThirdPartyIntegration { get; set; }
        public static string Time = DateTime.Now.ToString("HH: mm tt");

        public static dynamic UpdateDates(dynamic data)
        {
            data.UpdatedNepaliDate = NepaliDate;
            data.UpdatedEnglishDate = EnglishDate;
            data.UpdatedBy = "User";
            return data;
        }

        public static dynamic CreateDates(dynamic data)
        {
            if (data.Id == null)
            {
                data.Id = Guid.NewGuid().ToString();
            }
            data.CreatedNepaliDate = NepaliDate;
            data.CreatedEnglishDate = EnglishDate;
            data.UpdatedNepaliDate = NepaliDate;
            data.UpdatedEnglishDate = EnglishDate;
            data.UpdatedBy = "User";
            return data;
        }
    }
}