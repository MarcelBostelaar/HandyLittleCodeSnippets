using System.Threading;

namespace AbstractAsynchronousUpdate
{
	/// <summary>
	/// Subclass this abstract class and implement the update function. Then start and stop as you see fit.
	/// </summary>
    abstract class AbstractAsynchronousUpdate
    {
        Thread UpdateThread;
        /// <summary>
        /// Boolean showing if there is currently an update thread running or not.
        /// </summary>
        public bool ThreadIsRunning { get; private set; }

        public AbstractAsynchronousUpdate()
        {
            ThreadIsRunning = false;
            UpdateThread = new Thread(new ThreadStart(LoopUpdate));
        }

        /// <summary>
        /// Method that can be invoked every monogame update. Will excecute one update if no thread is running, will do nothing if a thread is already doing updates.
        /// </summary>
        public void Invoke()
        {
            if (!ThreadIsRunning)
            {
                Update();
            }
        }

        /// <summary>
        /// Starts the maximum speed update thread if none is already running.
        /// </summary>
        public void StartThread()
        {
            if (!ThreadIsRunning)
            {
                UpdateThread.Start();
                ThreadIsRunning = true;
            }
        }
        /// <summary>
        /// Stops the maximum speed update thread if one is currently running. Thread will stop at the end of a full update call.
        /// </summary>
        public void StopThread()
        {
            if (ThreadIsRunning)
            {
                DoLoop = false;
                UpdateThread.Join();
                ThreadIsRunning = false;
            }
        }

        bool DoLoop;
        private void LoopUpdate()
        {
            DoLoop = true;
            while (DoLoop)
            {
                Update();
            }
        }
        protected abstract void Update();
    }
}
