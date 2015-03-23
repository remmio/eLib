using System;
using System.ComponentModel;
using System.Timers;

namespace CLib {
    /// <summary>
    /// 
    /// </summary>
    public class Ticker:INotifyPropertyChanged {
        /// <summary>
        /// 
        /// </summary>
        public Ticker () {
            var timer = new Timer {Interval = 1000};
            timer.Elapsed+=timer_Elapsed;
            timer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Now => DateTime.Now;

        void timer_Elapsed (object sender, ElapsedEventArgs e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Now"));

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
