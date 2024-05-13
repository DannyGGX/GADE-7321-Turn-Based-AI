

using System;

namespace DannyG
{
    public static class EnumExtensions
    {
        public static int Previous(this Enum enumValue)
        {
            return (enumValue.GetHashCode() - 1 + enumValue.GetType().GetEnumNames().Length) % enumValue.GetType().GetEnumNames().Length;
        }
        public static int Next(this Enum enumValue)
        {
            return (enumValue.GetHashCode() + 1) % enumValue.GetType().GetEnumNames().Length;
            //return Enum.ToObject(enumValue.GetType(), (enumValue.GetHashCode() + 1) % enumValue.GetType().GetEnumNames().Length) as Enum;
        }
        
        public static string ReplaceUnderscoreWithSpace(this Enum value)
        {
            // put spaces between words
            string text = value.ToString();
            text = text.Replace("_", " ");
            return text;
        }
    
        public static string InsertSpacesBeforeCapitals(this Enum value)
        {
            // TODO: don't separate letters of acronyms
            string text = value.ToString();
            text = System.Text.RegularExpressions.Regex.Replace(text, "([A-Z])", " $1");
            return text;
        }

        
    }
}