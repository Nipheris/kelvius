namespace Kelvius
{
	enum TemperatureUnit
	{
		Celsius,
		Kelvin,
		Fahrenheit,
	}

	static class TemperatureConversions
	{
		private static decimal absoluteZeroC = -273.15M;

		// celsius <-> kelvin
		public static decimal CelsiusToKelvin(decimal celsius) =>
			celsius - absoluteZeroC;

		public static decimal KelvinToCelsius(decimal kelvin) =>
			kelvin + absoluteZeroC;

		// celsius <-> fahrenheit
		public static decimal CelsiusToFahrenheit(decimal celsius) =>
			celsius * 9M / 5M + 32;

		public static decimal FahrenheitToCelsius(decimal fahrenheit) =>
			(fahrenheit - 32) * 5M / 9M;

		// fahrenheit <-> kelvin
		public static decimal FahrenheitToKelvin(decimal fahrenheit) =>
			CelsiusToKelvin(FahrenheitToCelsius(fahrenheit));

		public static decimal KelvinToFahrenheit(decimal kelvin) =>
			CelsiusToFahrenheit(KelvinToCelsius(kelvin));
	}
}
