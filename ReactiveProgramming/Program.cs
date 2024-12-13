using ReactiveProgramming;

var quit = false;

while (!quit)
{
    Console.Clear();
    Console.WriteLine("1. Range Sorter");
    Console.WriteLine("2. Folder Watcher");
    Console.WriteLine("3. Stock Tracker");
    Console.WriteLine("4. Quit");
    var key = Console.ReadKey();
    Console.WriteLine();

    switch (key.KeyChar)
    {
        case '1':
            RangeSorter.StartSorting();
            break;

        case '2':
            FolderWatcher.StartWatch();
            break;

        case '3':
            TempTracker.Main();//StockPriceTracker.StartTracking();
            break;

        case '4':
            quit = true;
            break;

        default:
            break;
    }
}