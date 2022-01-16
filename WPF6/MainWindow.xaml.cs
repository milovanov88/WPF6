using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public class WeatherControl : DependencyObject
        {
            public static readonly DependencyProperty temperatureProperty;
            public int temperature
            {
                get => (int)GetValue(temperatureProperty);
                set => SetValue(temperatureProperty, value);
            }
            static string[] WindDirection =
            {
            "северный", "северо-восточный", "восточный", "юго-восточный", "южный", "юго-западный", "западный", "северо-западный",
        };
            string direction;
            public string windDirection
            {
                get
                {
                    return direction;
                }
                set
                {
                    for (int i = 0; i < WindDirection.Length; ++i)
                        if (value.Equals(WindDirection[i], StringComparison.CurrentCultureIgnoreCase))
                        {
                            direction = value;
                            return;
                        }
                }
            }
            public int windSpeed { get; set; }
            static string[] WeatherType =
            {
            "солнечно", "облачно" , "дождь" , "снег"
        };
            string weather;
            public enum Precipitation : int
            {
                солнечно, облачно, дождь, снег
            }
            public string precipitation { get; set; }
            public WeatherControl(int temperature, string windDirection, int windSpeed, string precipitation)
            {
                this.temperature = temperature;
                this.windDirection = windDirection;
                this.windSpeed = windSpeed;
                this.precipitation = Enum.GetName(typeof(Precipitation), precipitation);
            }
            static WeatherControl()
            {
                temperatureProperty = DependencyProperty.Register(
                        nameof(temperature),
                        typeof(int),
                        typeof(WeatherControl),
                        new FrameworkPropertyMetadata(
                            0,
                            FrameworkPropertyMetadataOptions.AffectsMeasure |
                            FrameworkPropertyMetadataOptions.AffectsRender,
                            null,
                            new CoerceValueCallback(CoerceTemperatyre)),
                        new ValidateValueCallback(ValidateTemperature));
            }
            private static bool ValidateTemperature(object value)
            {
                int v = (int)value;
                if (v > -51 && v < 51)
                    return true;
                else
                    return false;
            }
            private static object CoerceTemperatyre(DependencyObject d, object baseValue)
            {
                int v = (int)baseValue;
                if (v > -51 && v < 51)
                    return v;
                else
                    return 0;
            }
        }
    }
}
