namespace DipDemo.Cataloguer.Infrastructure.Presentation
{
    public interface IMessageBoxCreator
    {
        bool AskUser(string message);
        bool AskUser(string message, string caption);
        bool? AskUserWithCancellation(string question);
        bool? AskUserWithCancellation(string message, string caption);
        void ShowInfo(string message);
        void ShowInfo(string message, string caption);
        void ShowError(string message);
        void ShowError(string message, string caption);
    }
}