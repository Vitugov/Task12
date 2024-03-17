using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Task12.Converters
{
    public class StringToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Конвертация из decimal в string для отображения
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Конвертация из string в decimal при сохранении значения
            decimal number;
            if (decimal.TryParse(value as string, out number))
            {
                return number;
            }

            // Возвращаем DependencyProperty.UnsetValue, чтобы указать, что ввод недопустим
            return System.Windows.DependencyProperty.UnsetValue;
        }
    }
}
