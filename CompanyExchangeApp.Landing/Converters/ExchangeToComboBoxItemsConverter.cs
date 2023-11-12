using CompanyExchangeApp.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CompanyExchangeApp.Landing.Converters
{
    public class ExchangeToComboBoxItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<ExchangeDto> exchanges)
            {
                // Get the names from the Exchange objects
                var exchangeNames = new List<string>(exchanges.Select(exchange => exchange.Name));

                // Add one more name to the list
                exchangeNames.Insert(0, "ALL");

                return exchangeNames;
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
