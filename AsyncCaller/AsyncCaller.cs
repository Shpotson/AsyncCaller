using System;

namespace AsyncCaller
{
    using System.Threading;
    using System.Threading.Tasks;
    public class AsyncCaller
    {
        protected EventHandler handler;
        EventWaitHandle door = new EventWaitHandle(false, EventResetMode.ManualReset);

        //Shows the result of invoke (true if the delegate's methods was accomlished in the allotted time)
        private bool result;
        public AsyncCaller(EventHandler handler)
        {
            if (handler == null)
            {
                this.handler = handler;
            }
            else
            {
                throw new AsyncCallerException("Delegate cannot be null");
            }
            
        }

        public bool Invoke(int milSec, object obj, EventArgs args)
        {
            //Перевод WaitHandler в несигнальное положение
            door.Reset();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            this.result = false;
            InvokerAsync(obj, args, token);
            WaiterAsync(milSec, token);

            //Ожидание сигнала
            door.WaitOne();
            //Завершение асинхронных процессов. 
            cts.Cancel();
            return result;
        }

        public static AsyncCaller operator +(AsyncCaller asyncCaller_first, AsyncCaller asyncCaller_second)
        {
            if (asyncCaller_first == null | asyncCaller_second == null)
            {
                throw new AsyncCallerException("Imposible to compose AsyncCaller instances if they are null");
            }
            return new AsyncCaller(asyncCaller_first.handler + asyncCaller_second.handler);
        }

        public static AsyncCaller operator +(AsyncCaller asyncCaller, EventHandler handler)
        {
            if (asyncCaller == null | handler == null)
            {
                throw new AsyncCallerException("Imposible to compose AsyncCaller instance and delegate if they are null");
            }
            return new AsyncCaller(asyncCaller.handler + handler);
        }

        //Асинхронный метод ожидания.
        private async void WaiterAsync(int milsec, CancellationToken token)
        {
            await Task.Run(() => Thread.Sleep(milsec));
            if (!token.IsCancellationRequested)
            {
                door.Set();
            }
        }

        //Асинхронный метод выполнения EventHandler.
        private async void InvokerAsync(object obj, EventArgs args, CancellationToken token)
        {
            await Task.Run(() => handler.Invoke(obj, args));
            if (!token.IsCancellationRequested)
            {
                this.result = true;
                door.Set();
            }
        }
    }
}
