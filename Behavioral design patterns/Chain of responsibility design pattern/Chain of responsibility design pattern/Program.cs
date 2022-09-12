using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain_of_responsibility_design_pattern
{
    // Sequence of handlers processing an event one after another
    // A chain of components who all get a chance to process a command or a query, optionally having default processing implementation
    // and ability to terminate the processing chain
    internal class Program
    {
        static void Main(string[] args)
        {
            var goblin = new Creature("goblin", 2, 2);
            var root = new CreatureModifier(goblin);
            root.Add(new NoBonusesModifier(goblin));
            root.Add(new DoubleAttackModifier(goblin));
            root.Add(new IncreaseDefenseModifier(goblin));
            root.Handle();
            Console.WriteLine(goblin);
            Console.ReadLine();
        }
    }
    // Method chain
    public class Creature
    {
        public string Name;
        public int Attack, Defense;
        public Creature(string name, int attack, int defense)
        {
            this.Name = name;
            this.Attack = attack;
            this.Defense = defense;
        }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;
        protected CreatureModifier next; //linked list
        public CreatureModifier(Creature creature)
        {
            this.creature = creature ?? throw new ArgumentNullException(paramName: nameof(creature));
        }
        public void Add(CreatureModifier cm) // adding new modifiers to the linked list
        {
            if(next != null)
            {
                next.Add(cm);
            }
            else
            {
                next = cm;
            }
        }
        public virtual void Handle() => next?.Handle(); // base handle method will traverse the linked list of modifiers and all of them will be applied 
    }   
    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {

        }
        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle(); // We call the base handler to move on the next modifier
        }
    }
    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Creature creature) : base(creature)
        {

        }
        public override void Handle()
        {
            Console.WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense += 3;
            base.Handle();
        }
    }
    public class NoBonusesModifier : CreatureModifier
    {
        public NoBonusesModifier(Creature creature) : base(creature)
        {

        }
        public override void Handle()
        {
            // We do nothing here because nobody will  be traversing the linked list from this point onward
        }
    }
}
