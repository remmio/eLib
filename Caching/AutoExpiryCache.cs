using System;
using System.Threading;

namespace eLib.Caching
{
    public class AutoExpiryCache<TKey, TValue> : Cache<TKey, TValue>
    {
        private Thread _expiryThread;
        private readonly object _waitLock;
        private volatile bool _quit;
        private volatile bool _restartWait;

        public TimeSpan MaxEntryAge { get; set; }

        private TimeSpan _expiryInterval;
        public TimeSpan ExpiryInterval
        {
            get { return _expiryInterval; }
            set
            {
                _expiryInterval = value;
                RestartWaiting();
            }
        }

        public int NumberOfExpiryRuns { get; private set; }

        public bool ExpiryThreadIsRunning => _expiryThread != null && _expiryThread.IsAlive;

        private void RestartWaiting()
        {
            _restartWait = true;
            WakeUpThread();
        }

        private void WakeUpThread()
        {
            lock (_waitLock)
            {
                Monitor.PulseAll(_waitLock);
            }
        }

        public AutoExpiryCache(string cacheName)
        {
            _quit = false;
            _restartWait = false;
            _waitLock = new object();
            MaxEntryAge = TimeSpan.FromHours(1);
            _expiryInterval = TimeSpan.FromMinutes(30);

            _expiryThread = new Thread(ThreadMain);
            _expiryThread.Name = cacheName + "ExpiryThread";
            _expiryThread.IsBackground = true;
            _expiryThread.Start();
        }

        private void ThreadMain()
        {
            while (!_quit)
            {
                lock (_waitLock)
                {
                    _restartWait = false;
                    Monitor.Wait(_waitLock, ExpiryInterval);
                    if (_quit || _restartWait) continue;
                    Expire(MaxEntryAge);
                    ++NumberOfExpiryRuns;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _quit = true;
                WakeUpThread();
                if (_expiryThread != null)
                {
                    if (!_expiryThread.Join(100))
                    {
                        _expiryThread.Abort();
                    }
                    _expiryThread = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
