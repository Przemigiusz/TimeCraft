using System.Text;
using TimeCraft_Console_App.Controllers;
using TimeCraft_Console_App.Interface_Elements;
using TimeCraft_Console_App.Messages;
using TimeCraft_WPF_App;
class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Console.WriteLine("Choose user's interface:");
        Console.WriteLine("1. Console Interface");
        Console.WriteLine("2. Graphical interface");
        Console.WriteLine("3. Exit");


        ConsoleKeyInfo keyInfo;
        while (true)
        {
            keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    Console.CursorVisible = false;
                    Console.OutputEncoding = Encoding.UTF8;
                    PlansController plansController = new PlansController();
                    plansController.takeControl();
                    Environment.Exit(0);
                    break;
                case ConsoleKey.D2:
                    App app = new App();
                    MainWindow mainWindow = new MainWindow();
                    app.Run(mainWindow);
                    Environment.Exit(0);
                    break;
                case ConsoleKey.D3:
                    Environment.Exit(0);
                    break;

            }

        }


    }
}

