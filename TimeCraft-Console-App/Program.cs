using System.Text;
using TimeCraft_Console_App.Controllers;

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


