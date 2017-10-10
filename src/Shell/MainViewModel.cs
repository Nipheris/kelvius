using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI;

namespace Kelvius.Shell
{
	using static TemperatureConversions;

	class MainViewModel : ReactiveObject
	{
		private BehaviorSubject<decimal> subjNewCelsius;
		private BehaviorSubject<decimal> subjNewKelvin;
		private BehaviorSubject<decimal> subjNewFahrenheit;
		private BehaviorSubject<TemperatureUnit> subjMasterUnit;

		private ObservableAsPropertyHelper<decimal> celsius;
		private ObservableAsPropertyHelper<decimal> kelvin;
		private ObservableAsPropertyHelper<decimal> fahrenheit;

		public MainViewModel()
		{
			this.subjNewCelsius = new BehaviorSubject<decimal>(0);
			this.subjNewKelvin = new BehaviorSubject<decimal>(0);
			this.subjNewFahrenheit = new BehaviorSubject<decimal>(0);
			this.subjMasterUnit = new BehaviorSubject<TemperatureUnit>(TemperatureUnit.Celsius);

			this.celsius = this.CreateCelsiusObservable().ToProperty(this, x => x.Celsius);
			this.kelvin = this.CreateKelvinObservable().ToProperty(this, x => x.Kelvin);
			this.fahrenheit = this.CreateFahrenheitObservable().ToProperty(this, x => x.Fahrenheit);
		}

		public decimal Celsius
		{
			get { return this.celsius.Value; }
			set { this.subjNewCelsius.OnNext(value); }
		}

		public decimal Kelvin
		{
			get { return this.kelvin.Value; }
			set { this.subjNewKelvin.OnNext(value); }
		}

		public decimal Fahrenheit
		{
			get { return this.fahrenheit.Value; }
			set { this.subjNewFahrenheit.OnNext(value); }
		}

		public TemperatureUnit MasterUnit
		{
			get { return this.subjMasterUnit.Value; }
			set
			{
				// store the last calculated value in the subject (use it as initial value when start editing)
				this.StoreLastCalculatedValueAsInput(value);
				// switch the master unit
				this.subjMasterUnit.OnNext(value);
			}
		}

		private IObservable<decimal> CreateCelsiusObservable()
		{
			return Observable.CombineLatest(
				this.subjMasterUnit, this.subjNewCelsius, this.subjNewKelvin, this.subjNewFahrenheit,
				(masterUnit, celsius, kelvin, fahrenheit) =>
				{
					switch (masterUnit)
					{
						case TemperatureUnit.Celsius:
							return celsius;
						case TemperatureUnit.Kelvin:
							return KelvinToCelsius(kelvin);
						case TemperatureUnit.Fahrenheit:
							return FahrenheitToCelsius(fahrenheit);
						default:
							throw new Exception();
					}
				}
			);
		}

		private IObservable<decimal> CreateKelvinObservable()
		{
			return Observable.CombineLatest(
				this.subjMasterUnit, this.subjNewCelsius, this.subjNewKelvin, this.subjNewFahrenheit,
				(masterUnit, celsius, kelvin, fahrenheit) =>
				{
					switch (masterUnit)
					{
						case TemperatureUnit.Celsius:
							return CelsiusToKelvin(celsius);
						case TemperatureUnit.Kelvin:
							return kelvin;
						case TemperatureUnit.Fahrenheit:
							return FahrenheitToKelvin(fahrenheit);
						default:
							throw new Exception();
					}
				}
			);
		}

		private IObservable<decimal> CreateFahrenheitObservable()
		{
			return Observable.CombineLatest(
				this.subjMasterUnit, this.subjNewCelsius, this.subjNewKelvin, this.subjNewFahrenheit,
				(masterUnit, celsius, kelvin, fahrenheit) =>
				{
					switch (masterUnit)
					{
						case TemperatureUnit.Celsius:
							return CelsiusToFahrenheit(celsius);
						case TemperatureUnit.Kelvin:
							return KelvinToFahrenheit(kelvin);
						case TemperatureUnit.Fahrenheit:
							return fahrenheit;
						default:
							throw new Exception();
					}
				}
			);
		}

		private void StoreLastCalculatedValueAsInput(TemperatureUnit unit)
		{
			switch (unit)
			{
				case TemperatureUnit.Celsius:
					this.subjNewCelsius.OnNext(this.Celsius);
					break;
				case TemperatureUnit.Kelvin:
					this.subjNewKelvin.OnNext(this.Kelvin);
					break;
				case TemperatureUnit.Fahrenheit:
					this.subjNewFahrenheit.OnNext(this.Fahrenheit);
					break;
			}
		}
	}
}
