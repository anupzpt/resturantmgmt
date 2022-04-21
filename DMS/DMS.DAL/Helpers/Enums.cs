using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace DMS.DAL.Helpers
{
    public static class PriorityEnum
    {

        public enum PriorityType
        {
            Urgent = 1,
            High = 2,
            Normal = 3,
            Low = 4,
        }
        public enum CAD_Status_Type
        {
            CADInitiateReady = 0,
            CADInitiated = 1,
            CADSupervised = 2,
            CADLegalHeadAssigned = 3,
            CADLegalAdvisorAssigned = 4,
            CADLegalAdvisorApproved = 5,
            CADLegalHeadCompleted = 6,
            CADCompleted = 7,
        }

        public static string GetPriorityType(string priorityType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(PriorityType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var PriorityType = enumList.Single(x => x.Id.ToString() == priorityType).Name;
            return PriorityType.ToString();
        }
    }
    public static class CustomerTypeEnum
    {

        public enum CustomerType
        {
            Person = 1,
            Company = 2,

        }

        public static string GetCustomerType(string customerType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(CustomerType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var CustomerType = enumList.Single(x => x.Id.ToString() == customerType).Name;
            return CustomerType.ToString();
        }
    }
    public static class GenderEnum
    {
        public enum GenderType
        {
            [Display(Name = "Male")]
            Male = 1,
            [Display(Name = "Female")]
            Female = 2,
            [Display(Name = "Others")]
            Others = 3,
        }
    }



    public static class FileEnum
    {
        public enum FileType
        {
            xlsx = 1,

            docx = 2,

            pdf = 3,
        }
    }



    public static class StatusEnum
    {

        public enum StatusType
        {
            Pending = 1,
            Return = 2,
            Reject = 3,
            Proceed = 4,
            Approved = 5,
            Skipped = 6
        }
        public enum RecommendationStatusType
        {
            Supported = 1,
            Noted = 2,
            Recommended = 3,
            Returned = 4,
            Approved = 5,
            Rejected = 6
        }



        public static int GetStatusType(string statusType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(StatusType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var StatusType = enumList.Single(x => x.Name.ToString() == statusType).Id;
            return StatusType;
        }




    }


    public static class EmpTypeEnum
    {

        public enum EmpType
        {
            MisAdmin = 1,
            Initiator = 2,
            Recommender = 3,
            Approver = 4,
            CADInitiator = 5,
            CADRecommender = 6,
            LegalApprover = 7,
            FinalApproval = 8
        }



        public static int GetEmpType(string empType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(EmpType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var StatusType = enumList.Single(x => x.Name.ToString() == empType).Id;
            return StatusType;
        }






    }

    public static class TaskTypeEnum
    {

        public enum TaskType
        {
            Open = 1,
            Pending = 2,
            Processing = 3,
            Closed = 4
        }


        public static int GetTaskType(string taskType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(TaskType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var StatusType = enumList.Single(x => x.Name.ToString() == taskType).Id;
            return StatusType;
        }




    }


    public static class MenuEnum
    {
        public enum MenuSection
        {
            [Display(Name = "Shared")]
            Shared = 1,
            [Display(Name = "Admin")]
            Admin = 2,
            [Display(Name = "Head")]
            Head = 3,
            [Display(Name = "Branch")]
            Branch = 4,
        }
    }

    public static class MonthEnum
    {
        public enum Month
        {
            बैशाख = 1,
            ज्येष्ठ = 2,
            आषाढ = 3,
            श्रावण = 4,
            भाद्र = 5,
            आश्विन = 6,
            कार्तिक = 7,
            मंगसिर = 8,
            पौष = 9,
            माघ = 10,
            फाल्गुण = 11,
            चैत्र = 12
        }

        public enum MonthInEng
        {
            Baisakh = 1,
            Jestha = 2,
            Asadh = 3,
            Srawan = 4,
            Bhadra = 5,
            Aswin = 6,
            kartik = 7,
            Mangsir = 8,
            Poush = 9,
            Magh = 10,
            Falgun = 11,
            Chaitra = 12
        }



        public static string GetMonthInEngWords(string monthval)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(MonthInEng))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var MonthName = enumList.Single(x => x.Id.ToString() == monthval).Name;
            return MonthName.ToString();
        }

        public static string GetMonthInNepWords(string monthval)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(Month))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var MonthName = enumList.Single(x => x.Id.ToString() == monthval).Name;
            return MonthName.ToString();
        }




    }
    public static class CustomerBlackListStatusEnum
    {

        public enum CustomerBlackListType
        {
            [Display(Name = "Black Listed")]
            BlackListed = 1,
            [Display(Name = "Non Black Listed")]
            NonBlackListed = 2,

        }



        public static int GetBlackListStatus(string status)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(CustomerBlackListType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var StatusType = enumList.Single(x => x.Name.ToString() == status).Id;
            return StatusType;
        }






    }

    #region OnlineForm
    public static class MaritalStatusEnum
    {
        public enum MaritalStatusType
        {
            [Display(Name = "Single")]
            Single = 1,
            [Display(Name = "Married")]
            Married = 2,
            [Display(Name = "Others")]
            Others = 3,
        }
        public static string GetMaritalStatusType(string maritalStatusType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(MaritalStatusType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var MaritalStatusType = enumList.Single(x => x.Id.ToString() == maritalStatusType).Name;
            return MaritalStatusType.ToString();
        }
    }
    public static class OccupationEnum
    {
        public enum OccupationType
        {
            [Display(Name = "Salaried")]
            Salaried = 1,
            [Display(Name = "Self Employeed")]
            SelfEmployeed = 2,
            [Display(Name = "Professional")]
            Professional = 3,
            [Display(Name = "Other")]
            Other = 4,
        }
        public static string GetOccupaationType(string occupationType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(OccupationType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var OccupationType = enumList.Single(x => x.Id.ToString() == occupationType).Name;
            return OccupationType.ToString();
        }
    }
    public static class SourceOfFundEnum
    {
        public enum SourceOfFundType
        {
            [Display(Name = "Salaried")]
            Saving = 1,
            [Display(Name = "Self Employeed")]
            Salary = 2,
            [Display(Name = "Professional")]
            Investment = 3,
            [Display(Name = "Self Employeed")]
            BusinessIncome = 4,
            [Display(Name = "Professional")]
            Remittance = 5,
            [Display(Name = "Other")]
            Other = 6,
        }
        public static string GetSourceOfFundType(string sourceOfFund)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(SourceOfFundType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var sourceOfFundType = enumList.Single(x => x.Id.ToString() == sourceOfFund).Name;
            return sourceOfFundType.ToString();
        }
    }
    public static class NatureOfTransactionEnum
    {
        public enum NatureOfTransactionType
        {
            [Display(Name = "Cash")]
            Cash = 1,
            [Display(Name = "Cheque")]
            Cheque = 2,
            [Display(Name = "Remittance")]
            Remittance = 3,

        }
        public static string GetNatureOFTransaction(string natureOfTransactionType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(NatureOfTransactionType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var natureOfTransaction = enumList.Single(x => x.Id.ToString() == natureOfTransactionType).Name;
            return natureOfTransaction.ToString();
        }
    }
    public static class CurrencyEnum
    {
        public enum CurrencyType
        {
            [Display(Name = "NPR")]
            NPR = 1,
            [Display(Name = "$")]
            Dollar = 2,
            [Display(Name = "INR")]
            INR = 3,

        }
        public static string GetCurrencyTypeString(string currencyType)
        {
            var enumList = (from Enum e in Enum.GetValues(typeof(CurrencyType))
                            select new
                            {
                                Name = e,
                                Id = Convert.ToInt32(e)
                            }).ToList();

            var currencyTypeString = enumList.Single(x => x.Id.ToString() == currencyType).Name;
            return currencyTypeString.ToString();
        }
    }


    #endregion



}