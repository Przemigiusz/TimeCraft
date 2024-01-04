using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;
using TimeCraft_Console_App.Messages;

namespace TimeCraft_Console_App.Interface_Elements
{
    internal class WhatToDoMenuLoR : Menu
    {
        public WhatToDoMenuLoR(string menuHeader, List<SelectOption> menuOptions) : base(menuHeader, menuOptions)
        {
            this.topOffset = 0;
        }
        public int navigate()
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
                                    return Codes.LoRLogin;
                                case 1:
                                    return Codes.LoRRegistration;
                                case 2:
                                    Console.Clear();
                                    return Codes.Exit;
                            }
                            break;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Brak elementów w kolekcji 'options' - WhatToDoMenuLoR");
            }
        }
    }
}