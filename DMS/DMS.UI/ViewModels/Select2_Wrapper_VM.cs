using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.ViewModels
{
  
        public class Select2Format<PKType>
        {
            public PKType id { get; set; }

            public string text { get; set; }

            public bool selected { get; set; }

            public bool disabled { get; set; }
        }

        public class Select2Group

        {

            public string text { get; set; }

            public IEnumerable<Select2Format<string>> children { get; set; }

        }

        public class Select2FormatWrapper

        {
            public IEnumerable<Select2Format<string>> results { get; set; }

            public bool paginated { get; set; }

            public string error { get; set; }


            public Select2FormatWrapper()

            {

                results = new List<Select2Format<string>>();

                paginated = true;
            }
        }

}