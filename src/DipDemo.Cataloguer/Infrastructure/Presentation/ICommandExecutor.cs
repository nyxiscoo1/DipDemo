using System;

namespace DipDemo.Cataloguer.Infrastructure.Presentation
{
    public interface ICommandExecutor
    {
        bool IsInProgress { get; }

        void Execute(Func<Action> action, Action onAbort, Action<Exception> onException);
        void AbortOperation();
    }
}