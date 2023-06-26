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
            if (string.IsNullOrEmpty(Password) || Password.Length < 10 || Password.Length > 50)
                return false;
            return true;
        }
        public static bool IsUsernameValid(string Username)
        {
            if (string.IsNullOrEmpty(Username) || Username.Length > 50)
                return false;
            return true;
        }
        public static bool IsNameValid(string Name)
        {
            if (string.IsNullOrEmpty(Name) || !Regex.IsMatch(Name, @"^[A-Za-zА-Яа-я]+$") || Name.Length > 50)
                return false;
            return true;
        }
        public static bool IsSurnameValid(string Surname)
        {
            if (string.IsNullOrEmpty(Surname) || !Regex.IsMatch(Surname, @"^[A-Za-zА-Яа-я]+$") || Surname.Length > 50)
                return false;
            return true;
        }
        public static bool IsPatronymicValid(string Patronymic)
        {
            if (string.IsNullOrEmpty(Patronymic) || !Regex.IsMatch(Patronymic, @"^[A-Za-zА-Яа-я]+$") || Patronymic.Length > 50)
                return false;
            return true;
        }
        public static bool IsEmailValid(string Email)
        {
            if (!MailAddress.TryCreate(Email, out _) || Email.Length > 50)
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
            if (!DateOnly.TryParse(Birthday, out _) || DateTime.Parse(Birthday) > DateTime.Now)
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
            if(string.IsNullOrEmpty(sellaryratio.ToString()) || !double.TryParse(sellaryratio.ToString(), out _) || sellaryratio > 100)
                return false;
            return true;
        }
        public static bool IsPositionValid(string Position)
        {
            if(string.IsNullOrEmpty(Position) || Position.Length > 50)
                return false;
            return true;
        }
        public static bool IsPassportNumberValid(string PassportNumber)
        {
            if(string.IsNullOrEmpty(PassportNumber) || !Regex.IsMatch(PassportNumber,@"^\d{6}$"))
                return false;
            return true;
        }
        public static bool IsPassportSeriesValid(string PassportSeries)
        {
            if (string.IsNullOrEmpty(PassportSeries) || !Regex.IsMatch(PassportSeries, @"^\d{4}$"))
                return false;
            return true;
        }
    }
}
