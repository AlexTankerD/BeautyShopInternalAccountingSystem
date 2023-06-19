using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeautyShopInternalAccountingSystem.Models.RegularExpressions
{
    public static class ClientRegexp
    {
        public static bool IsPasswordValid(string Password)
        {
            if (string.IsNullOrEmpty(Password) || Password.Length < 10)
                return false;
            return true;
        }
        public static bool IsNewPasswordValid(string NewPassword, string ConfirmPassword)
        {
            if (ConfirmPassword != NewPassword)
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
        public static bool IsClientTagValid(string ClientTag)
        {
            if(string.IsNullOrEmpty(ClientTag))
                return false;
            return true;
        }
        public static bool IsClientTagColorValid(string ClientTagColor)
        {
            if (string.IsNullOrEmpty(ClientTagColor))
                return false;
            return true;
        }
    }
}
