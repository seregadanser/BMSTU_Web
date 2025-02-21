﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_course.Models
{
    public class ValidationException : Exception
    {
        public ValidationException(string value) : base(value)
        { }
    }

    public class NoSuchObjectException : Exception
    {
        public NoSuchObjectException(string value) : base(value)
        { }
    }
    public class IdException : Exception
    {
        public IdException(string value) : base(value)
        { }
    }

    public class ExistException : Exception
    {
        public ExistException(string value) : base(value)
        { }
    }

    public class DataValidateModel
    {
        public void Validate(object model)
        {
            string errorMessage = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, results, true);
            if (isValid == false)
            {
                foreach (var item in results)
                    errorMessage += "- " + item.ErrorMessage + "\n";
                throw new ValidationException(errorMessage);
            }
        }

    }
    public class DateLessThanOrEqualToToday : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Date value should not be a future date";
        }
        DateTime date;
        public DateLessThanOrEqualToToday(string date)
        {
            this.date = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }
        protected override ValidationResult IsValid(object objValue,
                                                       ValidationContext validationContext)
        {
            var dateValue = objValue as DateTime? ?? new DateTime();

            //alter this as needed. I am doing the date comparison if the value is not null

            if(dateValue.Date > DateTime.Now.Date || dateValue.Date < date)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }

    public static class Hash
    {

        public static string HashFunc(string password)
        {

            return password;
        }
        public static string HashFunc1(string password)
        {
            char[] charArray = password.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
