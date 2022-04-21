using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL.Helpers
{
    public static class StringHelpers
    {
        public static MemoryStream StringToMemoryStream(Encoding encoding, string source)
        {
            var content = encoding.GetBytes(source);
            return new MemoryStream(content);
        }
    }
}
