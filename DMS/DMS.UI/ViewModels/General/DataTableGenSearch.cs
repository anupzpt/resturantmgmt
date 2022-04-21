using BaseModel.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels
{
    public class DataTableModelGenSearch<ChildType> : DataTableModel where ChildType : class
    {
        public ChildType ChildData { get; set; }
    }
}