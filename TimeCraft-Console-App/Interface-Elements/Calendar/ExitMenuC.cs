using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;
using TimeCraft_Console_App.Messages;

namespace TimeCraft_Console_App.Interface_Elements.Calendar
{
    internal class ExitMenuC : Menu
    {
        public ExitMenuC(string menuHeader, List<SelectOption> menuOptions) : base(menuHeader, menuOptions)
        {
            this.topOffset = 3;
        }
        public int navigate()
        {
            this.currentOptionId = 0;
            this.options[this.currentOptionId].startBeingFocused();
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        moveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        moveDown();
                        break;
                    case ConsoleKey.Enter:
                        if (currentOptionId == 0)
                        {
                            return Codes.ELogout;
                        }
                        else
                        {
                            return Codes.EBack;
                        }
                }
            }
        }
    }
}
