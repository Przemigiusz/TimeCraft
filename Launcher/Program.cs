using System.Diagnostics;

class Program
{
    static void Main()
    {
        string? choice = "-1";
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose user's interface:");
            Console.WriteLine("1. Console Interface");
            Console.WriteLine("2. Graphics interface");
            Console.WriteLine("3. Exit");

            Console.WriteLine();

            Console.Write("Your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RunConsoleVersion();
                    break;
                case "2":
                    RunGraphicsVersion();
                    break;
                case "3":
                    System.Environment.Exit(0);
                    break;
            }
        }
    }

    static void RunConsoleVersion()
    {
        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "C:\\Users\\rutko\\Desktop\\Studia\\KCK\\Projekt\\TimeCraft\\TimeCraft-Console-App\\bin\\Release\\net8.0-windows\\TimeCraft-Console-App.exe",
                UseShellExecute = false
            };

            Process? process = Process.Start(startInfo);

            if (process != null)
            {
                process.WaitForExit();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running console version: {ex.Message}");
        }
    }

    static void RunGraphicsVersion()
    {
        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "C:\\Users\\rutko\\Desktop\\Studia\\KCK\\Projekt\\TimeCraft\\TimeCraft-WPF-App\\bin\\Release\\net8.0-windows\\TimeCraft-WPF-App.exe",
                UseShellExecute = false
            };

            Process? process = Process.Start(startInfo);

            if (process != null)
            {
                process.WaitForExit();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running graphics version: {ex.Message}");
        }
    }
}
