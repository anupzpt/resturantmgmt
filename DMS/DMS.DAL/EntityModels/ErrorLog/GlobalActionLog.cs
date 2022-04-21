using System;

namespace DMS.DAL.EntityModels.ErrorLog
{
    public class GlobalActionLog
    {
        public string Id { get; set; }
        public string Log { get; set; }
        public string ControllerName { get; set; }
        public string ActionPerformed { get; set; }
        public string Reason { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime Date { get; set; }
    }
}
