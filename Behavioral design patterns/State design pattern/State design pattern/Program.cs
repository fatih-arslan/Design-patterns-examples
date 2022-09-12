﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_design_pattern
{
    // A pattern in which the objects beaviour is determined by its state
    // An objects transitions from one state to another (something triggers the transition)
    // A formalized construct which manages state and transitions is called a state machine
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Switch
    {
        public State State = new OffState();
        public void On() { State.On(this); }
        public void Off() { State.Off(this); }
    }

    public abstract class State
    {
        public virtual void On(Switch sw)
        {
            Console.WriteLine("Light is already on.");
        }

        public virtual void Off(Switch sw)
        {
            Console.WriteLine("Light is already off.");
        }
    }

    public class OnState : State
    {
        public OnState()
        {
            Console.WriteLine("Light turned on.");
        }
        public override void Off(Switch sw)
        {
            Console.WriteLine("Turning light off...");
            sw.State = new OffState();
        }
    }

    public class OffState : State
    {
        public OffState()
        {
            Console.WriteLine("Light turned off.");
        }

        public override void On(Switch sw)
        {
            Console.WriteLine("Turning light on...");
            sw.State = new OnState();
        }
    }

}
