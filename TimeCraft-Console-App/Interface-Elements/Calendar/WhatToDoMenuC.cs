using Spectre.Console;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;
using TimeCraft_Console_App.Messages;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal class WhatToDoMenuC : Menu
    {
        public WhatToDoMenuC(string menuHeader, List<SelectOption> menuOptions) : base(menuHeader, menuOptions)
        {
            this.topOffset = 3;
        }
        public CNavigationResult navigate()
        {
            if (this.options.Count > 0)
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
                            switch (this.currentOptionId)
                            {
                                case 0:
                                    return new CNavigationResult(Codes.WTDMenuCTDAddPlans);
                                case 1:
                                    return new CNavigationResult(Codes.WTDMenuCTDShowPlans);
                                case 2:
                                    return new CNavigationResult(Codes.WTDMenuCTDCancel);
                            }
                            break;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Brak elementów w kolekcji 'options' - WhatToDoMenuC");
            }
            
        }
    }
}
