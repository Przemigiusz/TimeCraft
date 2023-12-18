using Spectre.Console;
using TimeCraft_Console_App.Messages;

internal class StartingMenuView
{
    private string introductoryText = "Welcome, press the Enter button to continue...";
    private string appDescription = "The efficient way to track your time.\nSchedule tasks, plan your day, and manage your life.";
    private static string appLabel = "___________.__                 _________                  _____   __   \r\n\\__    ___/|__|  _____    ____ \\_   ___ \\_______ _____  _/ ____\\_/  |_ \r\n  |    |   |  | /     \\ _/ __ \\/    \\  \\/\\_  __ \\\\__  \\ \\   __\\ \\   __\\\r\n  |    |   |  ||  Y Y  \\\\  ___/\\     \\____|  | \\/ / __ \\_|  |    |  |  \r\n  |____|   |__||__|_|  / \\___  >\\______  /|__|   (____  /|__|    |__|  \r\n                     \\/      \\/        \\/             \\/               ";

    private int delay = 100;

    private readonly object consoleLock = new object();
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    private List<Color> colorPool = new List<Color>
    {
        Color.Cyan2,
        Color.MediumSpringGreen,
        Color.SpringGreen1,
        Color.SpringGreen2_1,
        Color.Green1,
    };

    public async Task<int> DisplayStartingMenu()
    {
        DisplayAppLabel();

        DisplayAppDescription();

        Task.Run(() => AnimateIntroText(cancellationTokenSource.Token));
        Task.Run(() => AnimateColorWave(cancellationTokenSource.Token));

        Task<int> listenerTask = WaitForEnterOrEscape();

        await Task.WhenAll(listenerTask);

        return listenerTask.Result;
    }

    private void DisplayAppLabel()
    {
        AnsiConsole.MarkupLine($"[bold mediumspringgreen]{appLabel}[/]");
        Console.WriteLine();
    }

    private async Task AnimateColorWave(CancellationToken cancellationToken)
    {
        int colorIndex = 0;

        while (!cancellationToken.IsCancellationRequested)
        {
            Color currentColor = colorPool[colorIndex];

            await ChangeColorWave(currentColor, cancellationToken);

            colorIndex = (colorIndex + 1) % colorPool.Count;

            await Task.Delay(1);
        }
    }

    private async Task ChangeColorWave(Color color, CancellationToken cancellationToken)
    {
        string[] lines = appLabel.Split('\n');

        int width = lines[0].Length;
        int height = lines.Length;

        for (int i = 0; i < width && !cancellationToken.IsCancellationRequested; i++)
        {
            for (int j = 0; j < height && !cancellationToken.IsCancellationRequested; j++)
            {
                if (i < lines[j].Length)
                {
                    char currentChar = lines[j][i];

                    if (IsValidCharacter(currentChar))
                    {
                        lock (consoleLock)
                        {
                            Console.SetCursorPosition(i, j);
                            AnsiConsole.Markup($"[{color}]{currentChar}[/]");
                        }
                        await Task.Delay(1);
                    }
                }
            }
        }
    }

    private void DisplayAppDescription()
    {
        AnsiConsole.MarkupLine($"[bold aqua]{this.appDescription}[/]");
        Console.WriteLine();
    }

    private async Task<int> WaitForEnterOrEscape()
    {
        ConsoleKeyInfo keyInfo;
        while (true)
        {
            keyInfo = await Task.Run(() => Console.ReadKey(true));
            if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Escape)
            {
                break;
            }
        }
        this.cancellationTokenSource.Cancel();
        return keyInfo.Key == ConsoleKey.Enter ? Codes.SMContinue : Codes.SMExit;
    }

    private async void AnimateIntroText(CancellationToken cancellationToken)
    {
        int consoleTop = Console.CursorTop;

        while (!cancellationToken.IsCancellationRequested)
        {

            for (int i = 0; i <= introductoryText.Length; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                string visiblePart = introductoryText.Substring(0, i);

                lock (this.consoleLock)
                {
                    Console.SetCursorPosition(0, consoleTop);
                    AnsiConsole.Markup($"[bold yellow]{visiblePart.PadRight(introductoryText.Length, ' ')}[/]");
                }
                await Task.Delay(delay);
            }

            await Task.Delay(1000);

            for (int i = introductoryText.Length; i >= 0; i--)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                string visiblePart = introductoryText.Substring(0, i);

                lock (this.consoleLock)
                {
                    Console.SetCursorPosition(0, consoleTop);
                    AnsiConsole.Markup($"[bold yellow]{visiblePart.PadRight(introductoryText.Length, ' ')}[/]");
                }
                await Task.Delay(delay);
            }
        }
    }

    private bool IsValidCharacter(char character)
    {
        return character != ' ' && character != '\r' && character != '\n';
    }
}