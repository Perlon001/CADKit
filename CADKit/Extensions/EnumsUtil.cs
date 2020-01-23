using System;
using System.Collections.Generic;

namespace CADKit.Extensions
{
    public class EnumsUtil
    {
        public static Dictionary<string, TEnum> GetEnumDictionary<TEnum>()
        {
            Type enumType = typeof(TEnum);
            if (!enumType.IsEnum)
                throw new ArgumentException("Argument nie jest typem wyliczeniowym");
            var values = (TEnum[])Enum.GetValues(enumType);
            var dictionary = new Dictionary<string, TEnum>();
            foreach (var value in values)
            {
                dictionary.Add(Enum.GetName(enumType, value), value);
            }

            return dictionary;
        }

        public static TEnum GetEnum<TEnum>(string enumName, TEnum defaultValue)
        {
            var dictionary = GetEnumDictionary<TEnum>();
            if(!dictionary.ContainsKey(enumName))
            {
                dictionary.Add(enumName, defaultValue);
            }

            return dictionary[enumName];
        }
    }

}
