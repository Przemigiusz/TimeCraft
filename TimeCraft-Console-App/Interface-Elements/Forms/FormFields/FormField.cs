namespace TimeCraft_Console_App.Interface_Elements.Forms.FormFields
{
    internal abstract class FormField
    {
        protected string fieldName;
        public string FieldName { get { return this.fieldName; } }
        public FormField(string fieldName) {
            this.fieldName = fieldName;
        }
        public abstract void render();
        public abstract string getAnswer();
    }
}
