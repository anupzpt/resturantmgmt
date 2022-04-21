using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Helpers
{
    public class BatchProcessor<T> where T : class
    {
        public IList<T> DataList { get; set; }
        public IList<ProcessResult<T>> Result { get; set; }
        public int BatchSize { get; set; }

        // Declare a delegate that takes a single string parameter
        // and has no return type.
        public delegate void SaveHandler();

        public BatchProcessor()
        {
            Result = new List<ProcessResult<T>>();
        }
        public void ProcessData(SaveHandler savehandler)
        {
            int c = 1;
            foreach (var item in DataList)
            {
                var procRec = new ProcessResult<T>()
                {
                    Data = item,
                    Status = false
                };
                try
                {
                    procRec.Status = true;
                }
                catch (Exception ex)
                {
                    procRec.ex = ex;
                    throw;
                }
                Result.Add(procRec);
                if (c++ >= BatchSize)
                {
                    c = 1;
                    savehandler?.Invoke();
                }
            }
        }
    }
}
