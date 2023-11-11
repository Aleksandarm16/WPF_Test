using CompanyExchangeApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Type = CompanyExchangeApp.Business.Models.Type;

namespace CompanyExchangeApp.Landing.Converters
{
    public class TypeToComboBoxItemsConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Type> types)
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

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}