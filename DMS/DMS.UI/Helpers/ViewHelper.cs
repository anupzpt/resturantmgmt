using DMS.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMS.Services;
using DMS.DAL.DatabaseContext;
using DMS.DAL;

namespace DMS
{
    public static class ViewHelper
    {

        public static string CurrentActionName => HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();


        public static string DisplayCardType(byte? card00uin)
        {
            string ret = "";
            switch (card00uin)
            {
                case (int)EnumCardTypes.DebitCardRegular:
                    ret = "<span class='col-sm-12 label label-sm label-info'>Embossed</span>";
                    break;
                case (int)EnumCardTypes.CardInstant:
                    ret = "<span class='col-sm-12 label label-sm label-success'>Embossed Instant</span>";
                    break;
                case (int)EnumCardTypes.ODCard:
                    ret = "<span class='col-sm-12 label label-sm label-primary'>OD Card</span>";
                    break;
                case (int)EnumCardTypes.Credit:
                    ret = "<span class='col-sm-12 label label-sm label-primary'>Credit</span>";
                    break;
                case (int)EnumCardTypes.ATM:
                    ret = "<span class='col-sm-12 label label-sm label-primary'>ATM</span>";
                    break;
                case (int)EnumCardTypes.ContactLessCard:
                    ret = "<span class='col-sm-12 label label-sm label-success'>Contact Less Card</span>";
                    break;
                case (int)EnumCardTypes.LinkCard:
                    ret = "<span class='col-sm-12 label label-sm label-info'>Link Card</span>";
                    break;
                case (int)EnumCardTypes.Grievance:
                    ret = "<span class='col-sm-12 label label-sm label-success'>Grievance</span>";
                    break;
                case (int)EnumCardTypes.ECommerce:
                    ret = "<span class='col-sm-12 label label-sm label-success'>E-Commerce</span>";
                    break;
                case (int)EnumCardTypes.ContactLessInstantCard:
                    ret = "<span class='col-sm-12 label label-sm label-info'>Contact Less Instant</span>";
                    break;
            }
            return ret;
        }

        private static string CurSymbol = "NRs. ";
        private static string curFormat = "#,##0.00";
        private static string NumFormat = "#,##";

        public static string formaNumber(int No)
        {
            return formaNumber((long)No);
        }
        public static string formaNumber(long No)
        {
            string strFormat = "{1}{0}";
            if (No < 0) { strFormat = "({1}{0})"; }
            return (No == 0) ? "0" : string.Format(strFormat, Math.Abs(No).ToString(NumFormat), "");
        }

        public static string formatCur(double amt)
        {
            return formatCur(amt, false);
        }

        public static string formatCur(long amt)
        {
            return formatCur(amt, false);
        }

        public static string formatCur(double amt, bool addSymbol)
        {
            string strFormat = "{1}{0}";
            if (amt < 0) { strFormat = "({1}{0})"; }
            return (amt == 0) ? "" : string.Format(strFormat, Math.Abs(amt).ToString(curFormat), (addSymbol ? CurSymbol : ""));
        }



        public static string formatCur(decimal amt)
        {
            return formatCur((double)amt);
        }
        public static string formatCur(decimal? amt)
        {
            return (amt.HasValue && amt.Value != 0) ? formatCur(amt.Value) : "";
        }
        public static string formatCur(int amt)
        {
            return formatCur((double)amt);
        }
        public static string formatCur(float amt)
        {
            return formatCur((double)amt);
        }
        public static string InWords(decimal inputNumber)
        {
            return InWords((double)inputNumber);
        }
        public static string InWords(double inputNumber)
        {
            double inputNo = inputNumber;
            if (inputNo == 0)
                return "Zero";
            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }
            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            numbers[0] = (int)inputNo % 1000; // units
            numbers[1] = (int)inputNo / 1000;
            numbers[2] = (int)inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = (int)inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            sb.Append(" only");
            return sb.ToString().TrimEnd();
        }
        public static string GetCopyName(byte i)
        {
            string[] names = { "Customer Copy", "Office Copy", "Bank Copy" };
            if (i >= names.Count()) { return ""; }
            return names[i];
        }

        public static string TenantFile(string File)
        {
            return $"Tenant/default/{File}";
        }

        public static string GetDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string GetTime(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return "";
            }
            return dateTime.Value.ToShortTimeString();
        }

        public static string GetDate(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return "";
            }
            return dateTime.Value.ToString("yyyy-MM-dd");
        }

        public static string GetDate(string date)
        {
            try
            {
                DateTime dateTime = DateTime.Parse(date);
                return dateTime.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                return date;
            }
        }

        //public static DateTime ConvertDate(string date)
        //{
        //    try
        //    {
        //        DateTime dateTime = DateTime.Parse(date);
        //        return dateTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ErrorHelper.GetMsg(ex));
        //    }
        //}

        
        public static string RelatedCompanyName()
        {
            return Properties.Settings.Default.ClientName;
        }

        public static string LoginCompanyName => Properties.Settings.Default.LoginCompanyName;


        public static string EncryptText(long plainText)
        {
            return EncryptText(plainText.ToString());
        }

        public static string EncryptText(string plainText)
        {
            return BasicEncryption.EncryptString(plainText, "ants@101");
        }

        public static long DecryptText(string cipherText)
        {
            var decrypted_text = BasicEncryption.DecryptString(cipherText, "ants@101");
            long value = Convert.ToInt64(decrypted_text);
            return value;
        }

        public static string DecryptedText(string cipherText)
        {
            var decrypted_text = BasicEncryption.DecryptString(cipherText, "ants@101");
            string value = decrypted_text;
            return value;
        }

        public static List<cfg01configurations> cfg01configurations { get; set; }

        public static bool GetConfigurationBooleanValue(enumConfigSettingsKeys Key)
        {
            var value = GetConfigurationValue(Key);
            bool s = false;
            bool.TryParse(value, out s);
            return s;
        }

        public static string GetConfigurationValue(enumConfigSettingsKeys Key)
        {
            var Set1 = ConfigSettingsKeyValues._AllKeys[Key];
            if (ViewHelper.cfg01configurations == null)
            {
                return "";
            }
            cfg01configurations cfg01configurations = ViewHelper.cfg01configurations.FirstOrDefault(x => x.cfg01module == Set1.Key && x.cfg01key == Set1.Value);
            if (cfg01configurations == null)
            {
                return "";
            }
            if (cfg01configurations.cfg01value == null) { cfg01configurations.cfg01value = ""; }
            return cfg01configurations.cfg01value;
        }

    }
}