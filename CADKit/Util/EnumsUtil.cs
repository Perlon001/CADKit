using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitCore.Util
{
    public class EnumsUtil 
    {
        public static TEnum GetEnum<TEnum>(string enumName, TEnum defaultValue)
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
            if(!dictionary.ContainsKey(enumName))
            {
                dictionary.Add(enumName, defaultValue);
            }

            return dictionary[enumName];
        }
    }
}
