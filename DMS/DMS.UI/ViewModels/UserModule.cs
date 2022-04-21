using DMS.DAL.DatabaseContext;
using DMS.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels
{
    public class UserModule
    {
        public ApplicationUserVM ApplicationUser { get; set; }
      public usr05users UserAgent { get; set; }
    }
}