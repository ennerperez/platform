//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Platform.Support
//{

//    public class Timer
//    {

//        private int _interval;

//        public int Interval
//        {
//            get { return _interval; }
//            set { _interval = value; }
//        }

//        private bool _isRunning;

//        public bool IsRunning
//        {
//            get { return _isRunning; }
//            set { _isRunning = value; }
//        }

//        public event ElapsedEventHandler Elapsed;

//        protected virtual void OnTimerElapsed()
//        {
//            if (Elapsed != null)
//            {
//                Elapsed(this, new ElapsedEventArgs(DateTime.Now));
//            }
//        }

//        public Timer()
//        {
//        }

//        public Timer(int interval) : this()
//        {
//            Interval = interval;
//        }

//        public async Task Start()
//        {
//            int seconds = 0;
//            IsRunning = true;
//            while (IsRunning)
//            {
//                if (seconds != 0 && seconds % Interval == 0)
//                {
//                    OnTimerElapsed();
//                }
//#if NETFX_45
//                await Task.Delay(1000);
//#else
//                await TaskEx.Delay(1000);
//#endif
//                seconds++;
//            }
//        }

//        public void Stop()
//        {
//            IsRunning = false;
//        }


//        public bool AutoReset
//        {
//            get;
//            set;
//        }



//    }

//    public delegate void ElapsedEventHandler(object sender, ElapsedEventArgs e);

//    public class ElapsedEventArgs : EventArgs
//    {
//        //
//        // Properties
//        //
//        public DateTime SignalTime
//        {
//            get;
//        }

//        //
//        // Constructors
//        //
//        internal ElapsedEventArgs(DateTime time)
//        {
//            this.SignalTime = time;
//        }
//    }

//}
