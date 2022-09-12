﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_design_pattern
{
    internal class Program
    {
        // Facilities communication between components
        // Create the mediator and have each object in the system refer to it
        static void Main(string[] args)
        {
            var room = new ChatRoom();
            var john = new Person("John");
            var jane = new Person("Jane");

            room.Join(john);
            room.Join(jane);

            john.Say("hi");
            jane.Say("oh, hey john");

            var simon = new Person("Simon");
            room.Join(simon);
            simon.Say("hi everyone");
            jane.PrivateMessage("Simon", "glad you could join us!");
            Console.ReadLine();
        }
    }
    public class Person
    {
        public string Name;
        public ChatRoom Room;
        private List<string> chatlog = new List<string>(); // A list that represents all the messages this person has received
        public Person(string name)
        {
            Name = name;
        }
        public void Say(string message)
        {
            Room.Broadcast(Name, message);
        }
        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }
        public void Receive(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            chatlog.Add(s);
            Console.WriteLine($"[{Name}'s chat session] {s}");
        }
    }
    public class ChatRoom
    {
        private List<Person> people = new List<Person>();
        public void Join(Person p)
        {
            string joinMsg = $"{p.Name} joins the chat";
            Broadcast("room", joinMsg);
            p.Room = this;
            people.Add(p);
        }
        public void Broadcast(string source, string message)
        {
            foreach(Person p in people)
            {
                if(p.Name != source)
                {
                    p.Receive(source, message);
                }
            }
        }
        public void Message(string source, string destination, string message)
        {
            people.FirstOrDefault(p => p.Name == destination)
                ?.Receive(source, message);
        }
    }
}
