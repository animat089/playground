using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace ReactiveProgramming;

internal class FolderWatcher
{
    public static void StartWatch()
    {
        string directoryToWatch = @"D:\Temp";

        using (var watcher = new FileSystemWatcher(directoryToWatch))
        {
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.IncludeSubdirectories = false;

            var fileCreated = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                handler => watcher.Created += handler,
                handler => watcher.Created -= handler
            );

            var dataStream = fileCreated
                .SelectMany(evt => ReadFileData(evt.EventArgs.FullPath))
                .Where(data => !string.IsNullOrEmpty(data))
                .Select(data => ProcessData(data));

            dataStream.Subscribe(data => Console.WriteLine($"Processed data: {data}"));

            watcher.EnableRaisingEvents = true;
            Console.WriteLine($"Monitoring directory '{directoryToWatch}' for new files...");
            Console.ReadKey();
        }
    }

    private static IObservable<string> ReadFileData(string filePath)
    {
        return Observable.Start(() =>
        {
            Console.WriteLine($"Reading data from file '{filePath}'");
            return File.ReadAllText(filePath);
        });
    }

    private static string ProcessData(string data)
    {
        Console.WriteLine($"Original data: {data}");

        return data.ToUpper();
    }
}