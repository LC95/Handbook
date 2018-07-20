using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Helloworld
{

    class Program
    {
        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            Subscriber sub1 = new Subscriber("sub1");
            Subscriber sub2 = new Subscriber("sub2");

            sub1.Subscribe(pub);
            sub2.Subscribe(pub);

            // 调用事件方法
            pub.DoSomething();

            Console.ReadLine();
        }
    }
    //事件数据
    public class CustomEventArgs : EventArgs
    {
        private string msg;

        public CustomEventArgs(string s)
        {
            msg = s;
        }
        public string Message
        {
            get=>msg;set { msg = value; }
        }
    }

    public class Publisher
    {
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;
        public void DoSomething()
        {
            while(true)
            {
                OnRaiseCustomEvent(new CustomEventArgs("Did something"));
                Thread.Sleep(1000);
            }
            
        }
        protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> handler = RaiseCustomEvent;

            if (handler != null)
            {
                e.Message += String.Format(" at {0}",DateTime.Now.ToString());
                handler(this, e);
            }
        }
        public void GetSubscribe(EventHandler<CustomEventArgs> eventHandler)
        {
            this.RaiseCustomEvent += eventHandler;
        }
    }
    class Subscriber
    {
        private string id;
        public Subscriber(string id)
        {
            this.id = id;
        }
        public void Subscribe(Publisher pub)
        {
            pub.GetSubscribe(HandleCustomEvent);
        }
        private void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            Console.WriteLine(id + " received this message: {0}", e.Message);
        }
    }
}
