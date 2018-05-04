using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RNDSystems.Common
{
    public static class Validation
    {
        private static readonly List<IValidate> validation;
        static Validation()
        {
            validation = new List<IValidate>();
            validation.Add(new StringValidation());
            validation.Add(new DecimalValidation());
            validation.Add(new Int32Validation());
            validation.Add(new DateTimeValidation());
        }
        public static T Validate<T>(object obj)
        {
            return validation.FirstOrDefault(o => o.isMatch<T>()).Validate<T>(obj);
        }
        public static bool IsNotValidAsciiChars(object obj)
        {
            return !new Regex("[^ -~]+").IsMatch(obj.ToString().Trim());
        }
    }

    public interface IValidate
    {
        bool isMatch<T>();
        T Validate<T>(object data);
    }
    public class StringValidation : IValidate
    {
        public bool isMatch<T>()
        {
            return typeof(T) == typeof(string);
        }
        public T Validate<T>(object data)
        {
            string outData = "";
            if (data != null && data != DBNull.Value && Validation.IsNotValidAsciiChars(data))
            {
                return (T)Convert.ChangeType(data, typeof(T));
            }
            return (T)Convert.ChangeType(outData, typeof(T));
        }
    }
    public class DecimalValidation : IValidate
    {

        public bool isMatch<T>()
        {
            return typeof(T) == typeof(decimal);
        }
        public T Validate<T>(object data)
        {
            decimal outData = 0;
            if (data != null && data != DBNull.Value && Validation.IsNotValidAsciiChars(data) && decimal.TryParse(data.ToString(), out outData))
            {
                return (T)Convert.ChangeType(data, typeof(T));
            }
            return (T)Convert.ChangeType(outData, typeof(T));
        }
    }
    public class Int32Validation : IValidate
    {

        public bool isMatch<T>()
        {
            return typeof(T) == typeof(Int32);
        }
        public T Validate<T>(object data)
        {
            Int32 outData = 0;
            if (data != null && data != DBNull.Value && Validation.IsNotValidAsciiChars(data) && Int32.TryParse(data.ToString(), out outData))
            {
                return (T)Convert.ChangeType(data, typeof(T));
            }
            return (T)Convert.ChangeType(outData, typeof(T));
        }
    }
    public class DateTimeValidation : IValidate
    {

        public bool isMatch<T>()
        {
            return typeof(T) == typeof(DateTime);
        }
        public T Validate<T>(object data)
        {
            DateTime outData = DateTime.MinValue;
            if (data != null && data != DBNull.Value && Validation.IsNotValidAsciiChars(data) && DateTime.TryParse(data.ToString(), out outData))
            {
                return (T)Convert.ChangeType(data, typeof(T));
            }
            return (T)Convert.ChangeType(outData, typeof(T));
        }
    }
}