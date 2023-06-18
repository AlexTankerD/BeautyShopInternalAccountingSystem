using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.RegularExpressions
{
    public class EmployeeRegexp
    {
        public static bool IsPasswordValid(string Password)
        {
            if (string.IsNullOrEmpty(Password) || Password.Length < 10)
                return false;
            return true;
        }
        public static bool IsUsernameValid(string Username)
        {
            if (string.IsNullOrEmpty(Username) || Username.Length > 15)
                return false;
            return true;
        }
        public static bool IsNameValid(string Name)
        {
            if (string.IsNullOrEmpty(Name) || Regex.IsMatch(Name, @"\d"))
                return false;
            return true;
        }
        public static bool IsSurnameValid(string Surname)
        {
            if (string.IsNullOrEmpty(Surname) || Regex.IsMatch(Surname, @"\d"))
                return false;
            return true;
        }
        public static bool IsPatronymicValid(string Patronymic)
        {
            if (string.IsNullOrEmpty(Patronymic) || Regex.IsMatch(Patronymic, @"\d"))
                return false;
            return true;
        }
        public static bool IsEmailValid(string Email)
        {
            if (!MailAddress.TryCreate(Email, out _))
                return false;
            return true;
        }
        public static bool IsPhoneNumberValid(string PhoneNumber)
        {
            if (string.IsNullOrEmpty(PhoneNumber) || !Regex.IsMatch(PhoneNumber, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
                return false;
            return true;
        }
        public static bool IsBirthdayValid(string Birthday)
        {
            if (!DateOnly.TryParse(Birthday, out _))
                return false;
            return true;
        }
        public static bool IsSexValid(string Sex)
        {
            if (string.IsNullOrEmpty(Sex) || !Regex.IsMatch(Sex.ToString(), @"(М|Ж)"))
                return false;
            return true;
        }
        public static bool IsSellaryRatioValid(double? sellaryratio)
        {
            if(string.IsNullOrEmpty(sellaryratio.ToString()) || !double.TryParse(sellaryratio.ToString(), out _))
                return false;
            return true;
        }
        public static bool IsPositionValid(string Position)
        {
            if(string.IsNullOrEmpty(Position))
                return false;
            return true;
        }
        public static bool IsPassportDataValid(string PassportData)
        {
            if(string.IsNullOrEmpty(PassportData) || !Regex.IsMatch(PassportData,@"\d{10}"))
                return false;
            return true;
        }
    }
}
