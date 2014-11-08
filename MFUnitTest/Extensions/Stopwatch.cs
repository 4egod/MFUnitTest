
namespace System.Diagnostics
{
    using System;
    using Microsoft.SPOT;
    using Microsoft.SPOT.Hardware;

    public class Stopwatch
    {
        private long m_startTicks = 0;
        private long m_stopTicks = 0;
        private bool m_isRunning = false;

        private const long m_ticksPerMillisecond = TimeSpan.TicksPerMillisecond;

        public static Stopwatch StartNew()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }

        private Stopwatch()
        {
        }

        public void Reset()
        {
            m_startTicks = 0;
            m_stopTicks = 0;
            m_isRunning = false;
        }

        public void Restart()
        {
            m_startTicks = Utility.GetMachineTime().Ticks;
            m_stopTicks = 0; 
            m_isRunning = true;
        }

        public void Start()
        {
            if (m_startTicks != 0 && m_stopTicks != 0)
                m_startTicks = Utility.GetMachineTime().Ticks - (m_stopTicks - m_startTicks); 
            else
                m_startTicks = Utility.GetMachineTime().Ticks; 

            m_isRunning = true;
        }

        public void Stop()
        {
            m_stopTicks = Utility.GetMachineTime().Ticks;
            m_isRunning = false;
        }

        public long ElapsedMilliseconds
        {
            get
            {
                if (m_startTicks != 0 && m_isRunning)
                    return (Utility.GetMachineTime().Ticks - m_startTicks) / m_ticksPerMillisecond;
                else 
                    if (m_startTicks != 0 && !m_isRunning)
                        return (m_stopTicks - m_startTicks) / m_ticksPerMillisecond;
                    else
                        throw new InvalidOperationException();
            }
        }
    }
}