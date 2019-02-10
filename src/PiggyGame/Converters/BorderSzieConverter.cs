using System;
using System;
using System.Windows.Data;
using System.Globalization;

namespace PiggyGame.Converters
{
    public class BorderSzieConverter : IValueConverter
    {
        //ia ca parametru un obiect care poate fi convertit in double
        //aduna la el parametrul tot de tip obiect care poate fi convertit in double
        //returneaza valoarea + parametru
        //folosit ca sa maresti bordura zarului in functie de grosimea ei
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double borderSize = System.Convert.ToDouble(value);
            double final = borderSize + System.Convert.ToDouble(parameter);

            return final;
        }

        //nu este implementa, nu e nevoie
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


