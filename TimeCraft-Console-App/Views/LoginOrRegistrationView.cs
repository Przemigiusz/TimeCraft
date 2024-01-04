using TimeCraft_Console_App.Interface_Elements;
using TimeCraft_Console_App.Interface_Elements.Forms.FormFields;

namespace TimeCraft_Console_App.Views
{
    internal class LoginOrRegistrationView
    {
        public int displayMenu()
        {
            List<SelectOption> whatToDoMenuOptions = new List<SelectOption>
            {
                new SelectOption("Sign In"),
                new SelectOption("Sign Up"),
                new SelectOption("Exit")
            };
            string whatToDoMenuMenuHeader = "What You want to do?";
            WhatToDoMenuLoR menu = new WhatToDoMenuLoR(whatToDoMenuMenuHeader, whatToDoMenuOptions);
            menu.render(0);

            return menu.navigate();
        }
    }
}
