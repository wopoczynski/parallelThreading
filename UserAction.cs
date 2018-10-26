using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultipleThreading
{
    class UserAction
    {
        public const string MEAN = "mean";
        public const string MEDIAN = "median";

        private int[] generated;
        private int inputThreads;
        private string elapsedTime;
        private int windowLength;
        private string action;

        public void setInputThreads(int threads)
        {
            this.inputThreads = threads;
        }

        public void setAction(string action)
        {
            this.action = action;
        }

        public void setWindowLength(int windowLenght)
        {
            this.windowLength = windowLenght;
        }

        public void setGenerated(int range)
        {
            int[] generated = new int[range];
            Random RandomNumber = new Random();

            for (int i = 0; i < range; i++)
            {
                generated[i] = RandomNumber.Next(0, 10);
            }

            this.generated = generated;
        }

        public string getElapsedTime()
        {
            return this.elapsedTime;
        }

        private void calculateWindow()
        {
            this.windowLength = this.inputThreads != 0 ? (int)decimal.Floor(this.windowLength / this.inputThreads) : this.windowLength;
        }

        public string makeAction()
        {
            if (this.action == MEAN)
            {
                this.calculateMeanParallel();
                return this.elapsedTime;
            }
            else if (this.action == MEDIAN)
            {
                this.calculateMedianParallel();
                return this.elapsedTime;
            }
            else
            {
                return "WYBIERZ METODE OBLICZEŃ";
            }
        }

        private void calculateMean(IEnumerable<int> dataPart)
        {
                double result = dataPart.Sum() / dataPart.Count();
        }

        private void calculateMeanParallel()
        {
            Stopwatch timer = new Stopwatch();
            IList<int> generated = this.generated.ToList();
            timer.Start();
            if (this.inputThreads == 0)
            {
            int step = (int)decimal.Floor(generated.Count() / this.windowLength);

                for (int i = 0; i < step; i++)
                {
                    IEnumerable<int> dataPart = generated.Skip(i * windowLength).Take(windowLength);
                    this.calculateMean(dataPart);
                }
            }
            else
            {
                List<Thread> threads = new List<Thread>();
                for (int i = 0; i < this.inputThreads; i++)
                {                 
                    var dataPart = generated.Skip(i * windowLength).Take(windowLength);
                    Thread thread = new Thread(
                        () => calculateMean(dataPart)
                        );
                    thread.Start();
                    threads.Add(thread);
                }

                foreach(Thread thread in threads)
                {
                    thread.Join();
                }
            }
            
            timer.Stop();
            this.elapsedTime = timer.Elapsed.TotalSeconds.ToString();
        }

        private void calculateMedian(IEnumerable<int> dataPart)
        {
                int elements = dataPart.Count();
                int center = elements / 2;
                List<int> data = dataPart.ToList();
                int median = center % 2 == 0 ? data[center] : (data[center] + data[center + 1]) / 2;
                double result = dataPart.Sum() / dataPart.Count();
        }

        private void calculateMedianParallel()
        {
            Stopwatch timer = new Stopwatch();
            IList<int> generated = this.generated.ToList();
            timer.Start();
            if (this.inputThreads == 0)
            {
                int step = (int)decimal.Floor(generated.Count() / this.windowLength);

                for (int i = 0; i < step; i++)
                {
                    IEnumerable<int> dataPart = generated.Skip(i * windowLength).Take(windowLength).ToArray();

                    this.calculateMedian(dataPart);
                }
            }
            else
            {
                IList<Thread> threads = new List<Thread>();
                for (int i = 0; i < this.inputThreads; i++)
                {
                    IEnumerable<int> dataPart = generated.Skip(i * windowLength).Take(windowLength);
                    Thread thread = new Thread(
                        () => calculateMedian(dataPart)
                        );
                    thread.Start();
                    threads.Add(thread);
                }

                foreach (Thread thread in threads)
                {
                    thread.Join();
                }
            }

            timer.Stop();
            this.elapsedTime = timer.Elapsed.TotalSeconds.ToString();
        }
     
    }


}
