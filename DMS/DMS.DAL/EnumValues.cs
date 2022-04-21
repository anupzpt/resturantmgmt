using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.DAL
{
    public enum EnumUserRoles
    {
        SuperAdmin = 1,
        Admin = 2,
        OrganizationAdmin = 3,
        OrganizationUser = 4,
        //for agent

        BranchAdmin = 5,
        BranchUser = 6

    }

    public enum EnumMenuTypes
    {
        Primary,
        Secondary
    }

    public enum EnumEnglishMonth
    {
        January = 1,
        Febraury = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum EnumConfigValues
    {
        seq_NIBL_FILE = 1,
        CardNoInitialDigits = 3,
        TotalCardNoLength = 4,
        REPLACEMENT_SEQ_NIBL_FILE = 5,
        CBSCodeCompareLength = 6,
        AccountCBSCompareAfterLength = 7,
        DebitTransIDMinLength = 8,
        DebitTransIDMaxLength = 9
    }

    public enum EnumGenders
    {
        Male = 1,
        Female = 2
    }

    public enum EnumStatus
    {
        Yes = 1,
        No = 2
    }

    public enum EnumCustomerInitial
    {
        MR = 1,
        MS = 2,
        MRS = 3,
        MISS = 4
    }

    public enum EnumMachineType
    {
        Diebold = 1,
        Wincor = 2,
        NCR = 3
    }

    public enum EnumCardServiceRequestType
    {
        ReplaceCard = 0,
        PinRegeneration = 1,
    }

    public enum EnumCardReplacementStatus
    {
        STOLEN = 1,
        LOST = 2,
        DAMAGE = 3,
        OtherReason = 4
    }

    public enum EnumCardRequestType
    {
        RequestCard = 0,
        LinkToNewAccount = 1,
    }

    public enum EnumFrequency
    {
        Daily = 1,
        Weekly = 2,
        Fortnight = 3,
        Monthly = 4
    }

    public enum EnumMaritalStatus
    {
        Married = 1,
        Unmarried = 2,
        Divorce = 3,
        Widow = 4
    }

    public enum EnumCustomerType
    {
        Person = 1,
        Company = 2
    }

    public enum EnumRequestType
    {
        Activated = 1,
        Block = 2,
        Destory = 3,
        eComRegistration = 4,
        NewCard = 5,
        Renew = 6,
        RePin = 7,
        UnBlock = 8,
        PinRefreshed = 9,
        Grievance = 10,
        RePrint = 11,
        Supplementary = 12,
        LinkCard = 13,
        AnnualRenew = 14
    }

    public enum EnumCardStatus
    {
        Generated = 1,
        InPrinting = 2,
        PrintingReceived = 3,
        DispatchToBranch = 4,
        BranchReceived = 5,
        CustomerDelivered = 6,
        RequestActivation = 7,
        Activated = 8,
        Blocked = 9,
        Destroyed = 10,
        PinReceived = 11,
        Approved = 12,
        NewRequest = 13,
        Rejected = 14,
        CustomerNotified = 15,
        RequestBlock = 16,
        RequestUnblock = 17,
        RequestDestroy = 18,
        CardReceived = 19,
        Renew = 20,
        EComRegistrationRequest = 21,
        RePin = 22,
        EComRegistered = 23,
        RepinApproved = 24,
        BranchCardReceived = 25,
        BranchPinReceived = 26,
        BranchCardDispatched = 27,
        BranchPinDispatched = 28,
        RequestReturnedToBranch = 29,
        RepinCustomerNotified = 30,
        RepinCustomerDelivered = 31,
        BranchRepinPinReceived = 32,
        BranchRepinPinDispatched = 33,
        RequestPinRefreshed = 34,
        PinRefreshed = 35,
        InstantUploadedCard = 36,
        AssignedToBranch = 37,
        ReturnActivationRequest = 38,
        Deleted = 39,
        RequestDelete = 40,
        NewLinkRequest = 41,
        LinkRequestApproved = 42,
        LinkRequestReturned = 43,
        LinkRequestCompleted = 44,
        PinSorted = 45,
        CardSorted = 46,
        AnnualRenewFailed = 47,
        BlockRequestReturned = 48,
    }

    public enum EnumDestroyReason
    {
        Uncollected = 1,
        Unclear = 2,
        Damaged = 3,
        AccountClosed = 4
    }

    public enum EnumBatch_status
    {

        BranchReceived = 1,
        CentralReceived = 2,
        DispatchToBranch = 3,
        Generated = 4,
        PinReceived = 5,
        RepinGenerated = 6
    }

    public enum EnumPin_status
    {
        Activated = 1,
        CardRecived = 2,
        Delivered2Customer = 3,
        Generated = 4,
        Notified2Customer = 5
    }

    public enum EnumFieldDataType
    {
        String = 1,
        Number = 2,
        Date = 3
    }
    public enum EnumFieldSetting
    {
        ActionCode = 1,
        CardNumber = 2,
        ClientCode = 3,
        InstitutionCode = 4,
        BranchCode = 5,
        VIPFlag = 6,
        OwnerCode = 7,
        BasicCardFlag = 8,
        BasicCardNumber = 9,
        Title = 10,
        FamilyName = 11,
        FirstName = 12,
        EmbossedName = 13,
        EncodedName = 14,
        MaritalStatus = 15,
        Gender = 16,
        LegalID = 17,
        NationalityCode = 18,
        NoOfChildren = 19,
        CreditLimit = 20,
        IssuersClient = 21,
        LodgingPeriod = 22,
        ResidenceStatus = 23,
        NetYearlyIncome = 24,
        NoOfDependents = 25,
        BirthDate = 26,
        BirthCity = 27,
        BirthCountry = 28,
        Address1 = 29,
        Address2 = 30,
        Address3 = 31,
        Address4 = 32,
        CityCodes = 33,
        ZipCode = 34,
        CountryCode = 35,
        PhoneNo1 = 36,
        PhoneNo2 = 37,
        MobileNo = 38,
        EmailID = 39,
        Employer = 40,
        EmpAddress1 = 41,
        EmpAddress2 = 42,
        EmpAddress3 = 43,
        EmpAddress4 = 44,
        EmpCityCode = 45,
        EmpZipCode = 46,
        EMPCOUNTRYCODE = 47,
        CONTRACTSTARTDATE = 48,
        EmployeeStatus = 49,
        OPENINGDATE = 50,
        STARTVALDATE = 51,
        PRODUCTCODE = 52,
        TARIFFCODE = 53,
        DELIVERYMODE = 54,
        ACCOUNT1 = 55,
        ACCOUNT1CURRENCY = 56,
        ACCOUNT1TYPE = 57,
        LIMITCASHDOM = 58,
        LIMITPURCHDOM = 59,
        LIMITTEDOM = 60,
        RESERVED1 = 61,
        LIMITCASHINT = 62,
        LIMITPURCHINT = 63,
        LIMITTEINT = 64,
        RESERVED2 = 65,
        AUTHOLIMITDOM = 66,
        AUTHOLIMITINT = 67,
        RESERVED3 = 68,
        ACTIVITYCODE = 69,
        SOCIOPROFCODE = 70,
        STATUSCODE = 71,
        STAFFID = 72,
        DELIVERYFLAG = 73,
        DELIVERYDATE = 74,
        BANK_DSAREF = 75,
        USERDEFINEDFIELD1 = 76,
        USERDEFINEDFIELD2 = 77,
        USERDEFINEDFIELD3 = 78,
        USERDEFINEDFIELD4 = 79,
        USERDEFINEDFIELD5 = 80,
        EMBOSSLINE3 = 81,
        MAILINGADDRESS1 = 82,
        MAILINGADDRESS2 = 83,
        MAILINGADDRESS3 = 84,
        MAILINGADDRESS4 = 85,
        MAILINGZIPCODE = 86,
        MAILINGCITYCODE = 87,
        MAILINGCOUNTRYCODE = 88,
        PHONEHOME = 89,
        PHONEALTERNATE = 90,
        PHONEMOBILE = 91,
        PHOTOINDICATOR = 92,
        LANGUAGEIND = 93,
        MAIDENNAME = 94,
        SECONDARYACCT1 = 95,
        ACCTTYPE1 = 96,
        SECONDARYACCT2 = 97,
        ACCTTYPE2 = 98,
        APPLICATIONDATE = 99,
        FOURTHLINEPRINITINGDATA = 100,
        CHECKSUM = 101
    }

    public enum EnumCardTypes
    {
        DebitCardRegular = 1,
        CardInstant = 2,
        ODCard = 3,
        Credit = 4,
        ATM = 5,
        ContactLessCard = 6,
        LinkCard = 7,
        Grievance = 8,
        ECommerce = 9,
        ContactLessInstantCard = 10
    }

    public enum DashBoardTabCategory
    {
        CARD_HISTORY = 1,
        BRANCH_TRAN = 2,
        AMENDMENT = 3,
        GRIEVANCE = 4,
    }

    public enum EnumReturnStatus
    {
        AccountAlready = 1,
        DBError = 2,
        DateError = 3,
        Success = 4,
        CardAlreadyExist = 5
    }

    public enum EnumTemplateList
    {
        NewUserRegister = 1,
        PasswordReset = 2,
        SingleCardDispatched = 3,
        SinglePinDispatched = 4,
        AllCardDispatched = 5,
        AllPinDispatched = 6,
        ForgetPasswordTemplate = 7,
        ThankYouLetter = 8
    }

    public enum EnumTemplateFields
    {
        UserName = 1,
        UserEmail = 2,
        CustomerName = 3,
        AccountNumber = 4,
        CardNumber = 5,
        BatchNo = 6,
        BatchTotalCard = 7,
        RefNo = 8,
        Password = 9,
        LogedInUserName = 10,
        PassCode = 11,
        EmailId = 12,
        EmployeeName = 13,
        ThankYouLetterDate = 14
    }

    public enum enumConfigSettingsKeys
    {
        SMTPServer,
        SMTPUser,
        SMTPPassword,
        SMTPPort,
        SMTPSSL,
        SMTPFrom,
        EmailNotification,
    }

    public static class ConfigSettingsKeyValues
    {
        public static IDictionary<enumConfigSettingsKeys, KeyValuePair<string, string>> _AllKeys = new Dictionary<enumConfigSettingsKeys, KeyValuePair<string, string>>();
        static ConfigSettingsKeyValues()
        {
            _AllKeys.Add(enumConfigSettingsKeys.SMTPServer, new KeyValuePair<string, string>("Mail", "SMTPServer"));
            _AllKeys.Add(enumConfigSettingsKeys.SMTPUser, new KeyValuePair<string, string>("Mail", "SMTPUser"));
            _AllKeys.Add(enumConfigSettingsKeys.SMTPPassword, new KeyValuePair<string, string>("Mail", "SMTPPassword"));
            _AllKeys.Add(enumConfigSettingsKeys.SMTPPort, new KeyValuePair<string, string>("Mail", "SMTPPort"));
            _AllKeys.Add(enumConfigSettingsKeys.SMTPSSL, new KeyValuePair<string, string>("Mail", "SMTPSSL"));
            _AllKeys.Add(enumConfigSettingsKeys.SMTPFrom, new KeyValuePair<string, string>("Mail", "SMTPFrom"));
            _AllKeys.Add(enumConfigSettingsKeys.EmailNotification, new KeyValuePair<string, string>("Mail", "EmailNotification"));

        }
    }
}
