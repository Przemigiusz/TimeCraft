using Spectre.Console;

namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields.TimeSelect
{
    internal class MinutesSwitch : TimeSwitch
    {
        public MinutesSwitch(int currentOptionId) : base()
        {
            this.currentOptionId = currentOptionId;
            this.initializeSwitchOptions();
        }
        protected override void initializeSwitchOptions()
        {
            for (int minute = 0; minute < 60; ++minute)
            {
                string formattedMinute = minute.ToString("D2");
                this.switchOptions.Add(formattedMinute);
            }
        }
    }
    
}
