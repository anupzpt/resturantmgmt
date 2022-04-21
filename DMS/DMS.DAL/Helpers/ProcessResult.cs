using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Helpers
{
    public class ProcessResult<T> where T : class
    {
        public bool Status { get; set; }
        public string ErrMsg { get; set; }
        public T Data { get; set; }
        public Exception ex { get; set; }
    }
}
