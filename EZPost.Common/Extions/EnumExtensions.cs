using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace EZPost.Common.Extions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取指定枚举成员的描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Enum obj)
        {
            var attribs = (DescriptionAttribute[])obj.GetType().GetField(obj.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attribs.Length > 0 ? attribs[0].Description : obj.ToString();
        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj,
       bool markCurrentAsSelected = true, int[] valuesToExclude = null) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumObj");

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue)) 
                         select new { ID = Convert.ToInt32(enumValue), Name = ((DescriptionAttribute[])enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)).Length > 0 ? ((DescriptionAttribute[])enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false))[0].Description : enumValue.ToString() };
            object selectedValue = null;
            if (markCurrentAsSelected)
                selectedValue = Convert.ToInt32(enumObj);
            return new SelectList(values, "ID", "Name", selectedValue);
        }

        /// <summary>
        /// 获取枚举成员的值(this是扩展方法的标志)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }

        public static T ToEnum<T>(this string obj) where T : struct
        {
            if (string.IsNullOrEmpty(obj))
            {
                return default(T);
            }
            try
            {
                return (T)Enum.Parse(typeof(T), obj, true);
            }
            catch (Exception)
            {
                return default(T);
            }
        }


        /// <summary>
        /// 根据枚举值，获取指定枚举类的成员描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetDescriptionString(this Type type, int? id)
        {
            var values = from Enum e in Enum.GetValues(type)
                         select new { id = e.ToInt(), name = e.ToDescriptionString() };

            if (!id.HasValue) id = 0;

            try
            {
                return values.ToList().Find(c => c.id == id).name;
            }
            catch
            {
                return "";
            }
        }
    }
}