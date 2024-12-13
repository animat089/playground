using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

internal class TempTracker
{
    public static void Main()
    {
        // Create a temperature sensor that generates random readings every second
        var temperatureSensor = new TemperatureSensor(TimeSpan.FromSeconds(1));

        // Calculate the moving average of the temperature readings over a 5-second window
        var temperatureSubject = new Subject<double>();
        var movingAverage = temperatureSubject.Window(TimeSpan.FromSeconds(5))
            .Select(window => window.Average())
            .Subscribe(temperature => Console.WriteLine($"Moving average temperature: {temperature:F2}"));

        // Subscribe to the temperature readings and publish them to our Rx pipeline
        temperatureSensor.TemperatureStream.Subscribe(temperatureSubject);

        // Wait for user input to exit the program
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        // Cleanup
        movingAverage.Dispose();
        temperatureSubject.Dispose();
        temperatureSensor.Dispose();
    }
}

internal class TemperatureSensor : IDisposable
{
    private readonly Random random = new Random();
    private readonly IDisposable timerSubscription;

    public TemperatureSensor(TimeSpan interval)
    {
        // Set up a timer to generate temperature readings every specified interval
        var timer = Observable.Interval(interval);
        timerSubscription = timer.Subscribe(_ => OnNext());
    }

    public IObservable<double> TemperatureStream { get { return temperatureSubject.AsObservable(); } }

    private readonly Subject<double> temperatureSubject = new Subject<double>();
    private void OnNext()
    {
        // Generate a random temperature reading between 0 and 100 degrees Celsius
        var temperature = random.NextDouble() * 100;
        Console.WriteLine($"New temperature reading: {temperature:F2} C");

        // Publish the temperature reading to our Rx pipeline
        temperatureSubject.OnNext(temperature);
    }

    public void Dispose()
    {
        timerSubscription.Dispose();
        temperatureSubject.OnCompleted();
        temperatureSubject.Dispose();
    }
}
