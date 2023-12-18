using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;
using TimeCraft_Console_App.Messages;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal class WhatToDoMenuDS : Menu
    {
        public WhatToDoMenuDS(string menuHeader, List<SelectOption> menuOptions) : base(menuHeader, menuOptions)
        {
            this.topOffset = 1;
        }
        public DSNavigationResult navigate()
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
                                    return new DSNavigationResult(Codes.WTDMenuDSDetails);
                                case 1:
                                    return new DSNavigationResult(Codes.WTDMenuDSEdit);
                                case 2:
                                    return new DSNavigationResult(Codes.WTDMenuDSDelete);
                                case 3:
                                    return new DSNavigationResult(Codes.WTDMenuDSCancel);
                            }
                            break;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Brak elementów w kolekcji 'options' - WhatToDoMenuDS");
            }
        }
    }
}
