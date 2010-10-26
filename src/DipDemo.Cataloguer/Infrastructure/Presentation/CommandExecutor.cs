using System;
using System.ComponentModel;
using System.Threading;

namespace DipDemo.Cataloguer.Infrastructure.Presentation
{
    public class CommandExecutor : ICommandExecutor
    {
        private Thread thread;

        public bool IsInProgress
        {
            get { return thread == null ? false : thread.IsAlive; }
        }

        public void Execute(Func<Action> action, Action onAbort, Action<Exception> onException)
        {
            if(IsInProgress)
                throw new InvalidOperationException("Command executor is already in progress");
                
            var ctx = AsyncOperationManager.SynchronizationContext;

            thread = new Thread(o =>
                                    {
                                        try
                                        {
                                            var result = action();
                                            ctx.Send(x => result(), null);
                                        }
                                        catch (ThreadAbortException)
                                        {
                                            ctx.Send(x => onAbort(), null);
                                        }
                                        catch (Exception exc)
                                        {
                                            ctx.Send(x => onException(exc), null);
                                        }
                                    }) { IsBackground = true };

            thread.Start();
        }

        public void AbortOperation()
        {
            thread.Abort();
            thread = null;
        }
    }
}