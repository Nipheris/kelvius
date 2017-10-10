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

		public static decimal CelsiusToKelvin(decimal celsius) =>
			celsius - absoluteZeroC;

		public static decimal CelsiusToFahrenheit(decimal celsius) =>
			celsius * 9M / 5M + 32;

		public static decimal FahrenheitToCelsius(decimal fahrenheit) =>
			(fahrenheit - 32) * 5M / 9M;

		public static decimal FahrenheitToKelvin(decimal fahrenheit) =>
			(fahrenheit - 32) * 5M / 9M - absoluteZeroC;

		public static decimal KelvinToCelsius(decimal kelvin) =>
			kelvin + absoluteZeroC;

		public static decimal KelvinToFahrenheit(decimal kelvin) =>
			(kelvin + absoluteZeroC) * 9M / 5M + 32;
	}
}
