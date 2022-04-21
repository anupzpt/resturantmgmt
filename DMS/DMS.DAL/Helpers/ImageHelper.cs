using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace DMS.DAL.Helpers
{
    public static class ImageHelper
    {

        /// <summary>
        /// Determines whether [is image extension] [the specified ext].
        /// Comparision agains supported image types from properties ImageTypes eg: .jpg, .png, .gif
        /// input variable ext should be without ., as . will be added by function itself
        /// </summary>
        /// <param name="ext">The ext.</param>
        /// <returns>
        ///   <c>true</c> if [is image extension] [the specified ext]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsImageExtension(string file_name)
        {
            string ext = System.IO.Path.GetExtension(file_name != null ? file_name : "");
            ext = "." + ext.Replace(".", "");
            string[] _validExtensions = (Properties.Settings.Default.ImageTypes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.Trim().ToLower()).ToArray();
            return _validExtensions.Contains(ext.ToLower());
        }

        public static string getExtensionName(string file_name)
        {
            string extName = System.IO.Path.GetExtension(file_name);
            var charsToRemove = new string[] { "." };
            foreach (var c in charsToRemove)
            {
                extName = extName.Replace(c, string.Empty);
            }
            return extName;
        }

    }

}
