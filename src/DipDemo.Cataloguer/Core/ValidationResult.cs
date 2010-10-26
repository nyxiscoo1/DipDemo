namespace DipDemo.Cataloguer.Core
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; }

        public string[] Messages { get; private set; }

        public ValidationResult()
        {
            IsValid = true;
            Messages = new string[0];
        }

        public ValidationResult(bool isValid, string[] messages)
        {
            IsValid = isValid;
            Messages = messages;
        }
    }
}