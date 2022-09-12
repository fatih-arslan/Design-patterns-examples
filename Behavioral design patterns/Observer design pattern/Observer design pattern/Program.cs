using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_design_pattern
{
    // We need to be informed when certain things happen
    // We want to listen to events and notified when they occur
    // Built into C# with event keyword
    internal class Program
    {
        static void Main(string[] args)
        {

        }        
    }
    public class Event
    {

    }
    public class FallsIllEvent : Event
    {
        public string Address;
    }
    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> subscriptions = new HashSet<Subscription>();
        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            subscriptions.Add(subscription);
            return subscription;
        }
        public void CatchACold()
        {
            foreach (var sub in subscriptions)
                sub.Observer.OnNext(new FallsIllEvent { Address = "123 London Road" });
        }
        private class Subscription : IDisposable
        {
            private Person person;
            public IObserver<Event> Observer;

            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }

            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }
    }


}
