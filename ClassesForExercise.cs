using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCollectionsAsync
{
    public class Battery
    {
        const int MAX_CAPACITY = 1000;
        private static Random r = new Random();
        //Add events to the class to notify upon threshhold reached and shut down!
        #region events
        #endregion
        
        public event Action ReachedThreshhold;
        public event Action Shutdown;

        public int Threshold { get; }
        public int Capacity { get; set; }
        public int Percent
        {
            get
            {
                return 100 * Capacity / MAX_CAPACITY;
            }
        }
        public Battery()
        {
            Capacity = MAX_CAPACITY;
            Threshold = 400;
        }

        public void Usage()
        {
            Capacity -= r.Next(50, 150);
            if(ReachedThreshhold != null)
            {
                ReachedThreshhold();
            }
            if(Shutdown != null)
            {
                Shutdown();
            }
            //Add calls to the events based on the capacity and threshhold
            #region Fire Events
            #endregion
        }

    }

    class ElectricCar
    {
        public Battery Bat { get; set; }
        public int id;

        //Add event to notify when the car is shut down
        public event Action OnCarShutDown;

        public ElectricCar(int id)
        {
            this.id = id;
            Bat = new Battery();
            Bat.ReachedThreshhold += BatTheshhold;
            Bat.Shutdown += BatShutdown;
            #region Register to battery events
            #endregion
        }
        public void StartEngine()
        {
            while (Bat.Capacity > 0)
            {
                Console.WriteLine($"{this} {Bat.Percent}% Thread: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Bat.Usage();
            }
        }

        //Add code to Define and implement the battery event implementations
        public void BatShutdown()
        {
          if(Bat.Capacity < Bat.Threshold)
            {
                Console.WriteLine("It is reached threshhold");
            }
        }
        public void BatTheshhold()
        {
            Console.WriteLine("You have low present Battry, the car is going to shut down!");
            Console.WriteLine("You are on "+ Bat.Percent + "%");
        }
        #region events implementation
        #endregion

        public override string ToString()
        {
            return $"Car: {id}";
        }

    }

}
