using System.Text;
using TimeCraft_Console_App.Controllers;
using TimeCraft_Console_App.Interface_Elements;
class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = Encoding.UTF8;
        PlansController plansController = new PlansController();
        plansController.takeControl();
    }
}

