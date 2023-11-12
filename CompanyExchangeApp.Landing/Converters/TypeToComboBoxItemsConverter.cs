using CompanyExchangeApp.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CompanyExchangeApp.Landing.Converters
{
    public class TypeToComboBoxItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<TypeDto> types)
            {
                // Get the names from the Type objects
                var typeNames = new List<string>(types.Select(type => type.Name));

                // Add one more name to the list
                typeNames.Insert(0, "ALL");

                return typeNames;
            }

            // Default to an empty list if the conversion cannot be performed
            return new List<string>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}