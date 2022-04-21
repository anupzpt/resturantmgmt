using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace DMS.DAL.StaticHelper
{
    public class UploadResponse
    {
        public bool Status { get; set; }
        public string FileName { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public static class UploadHelper
    {
        public static string GetNewFileName()
        {
            Guid newGUID = Guid.NewGuid();
            return newGUID.ToString();
        }
        public static UploadResponse UploadFile(HttpPostedFileBase MainPhoto, string Destination)
        {
            var allowedExtensions = new[] { ".jpg", ".gif", ".png" };
            return UploadFile(MainPhoto, Destination, allowedExtensions, "");
        }
        public static UploadResponse UploadFile(HttpPostedFileBase MainPhoto, string Destination, string OldFileName)
        {
            var allowedExtensions = new[] { ".jpg", ".gif", ".png" };
            return UploadFile(MainPhoto, Destination, allowedExtensions, OldFileName);
        }
        public static UploadResponse UploadFile(HttpPostedFileBase MainPhoto, string Destination, string[] allowedExtensions)
        {
            return UploadFile(MainPhoto, Destination, allowedExtensions, "");
        }
        public static UploadResponse UploadFile(HttpPostedFileBase MainPhoto, string Destination, string[] allowedExtensions, string OldFileName)
        {
            var imageExt = new[] { ".jpg", ".gif", ".png" };
            UploadResponse NewResponse = new UploadResponse() { Status = false, ErrorCode = 999, ErrorMessage = "Unknown" };

            string newName = "";

            if (MainPhoto != null && MainPhoto.ContentLength > 0)
            {
                var newExt = Path.GetExtension(MainPhoto.FileName).ToLower();
                newName = OldFileName != "" ? OldFileName : GetNewFileName() + newExt;
                if (allowedExtensions.Contains(newExt))
                {
                    var dirPath = System.Web.HttpContext.Current.Server.MapPath(Destination);
                    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Destination + "/" + newName));

                    try
                    {
                        if (!System.IO.Directory.Exists(dirPath))
                        {
                            System.IO.Directory.CreateDirectory(dirPath);
                        }
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        MainPhoto.SaveAs(path);

                        //resize code
                        //resize
                        if (imageExt.Contains(newExt))
                        {
                            ImageResizeHelper.ResizeImage(path, Properties.Settings.Default.UploadImageW, Properties.Settings.Default.UploadImageH);
                        }
                        NewResponse.Status = true;
                        NewResponse.FileName = newName;
                    }
                    catch (Exception ex)
                    {
                        NewResponse.ErrorCode = 501;
                        NewResponse.ErrorMessage = ex.Message;
                    }
                }
                else
                {
                    NewResponse.ErrorCode = 102;
                    NewResponse.ErrorMessage = "Invalid File Type";
                }
            }
            else
            {
                NewResponse.ErrorCode = 101;
                NewResponse.ErrorMessage = "No File uploaded";
            }
            return NewResponse;
        }
        //public static string SignaturePath()
        //{
        //    return "~/private/" + "org_" + SessionData.My.UserModel.RelatedOrgId + "/" + Properties.Settings.Default.;
        //}
    }
}

