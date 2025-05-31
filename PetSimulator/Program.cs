using System;

namespace VirtualPetSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- Pet Selection ---
            Console.WriteLine("Please choose a type of pet:");
            Console.WriteLine("1. Cat");
            Console.WriteLine("2. Dog");
            Console.WriteLine("3. Rabbit");
            Console.Write("\nUser Input: ");
            string petChoice = Console.ReadLine();

            string petType = petChoice switch
            {
                "1" => "Cat",
                "2" => "Dog",
                "3" => "Rabbit",
                _ => null
            };

            if (petType == null)
            {
                Console.WriteLine("Invalid selection. Exiting...");
                return;
            }

            Console.WriteLine($"\nYou’ve chosen a {petType}. What would you like to name your pet?");
            Console.Write("\nUser Input: ");
            string petName = Console.ReadLine();

            Console.WriteLine($"\nWelcome, {petName}! Let's take good care of {(petType == "Cat" ? "her" : "him")}.");

            // --- Initial Stats ---
            int hunger = 5;
            int happiness = 5;
            int health = 8;

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine($"1. Feed {petName}");
                Console.WriteLine($"2. Play with {petName}");
                Console.WriteLine($"3. Let {petName} rest");
                Console.WriteLine($"4. Check {petName}'s Status");
                Console.WriteLine("5. Exit");
                Console.Write("\nUser Input: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": // Feeding
                        hunger -= 2;
                        if (hunger < 1) hunger = 1;

                        health += 1; // Slight health improvement
                        if (health > 10) health = 10;

                        Console.WriteLine($"\nYou fed {petName}. Hunger decreased and health slightly improved.");
                        SimulateTime(ref hunger, ref happiness, ref health, petName);
                        break;

                    case "2": // Playing
                        if (hunger >= 9)
                        {
                            Console.WriteLine($"\n{petName} is too hungry to play!");
                        }
                        else
                        {
                            happiness += 2;
                            if (happiness > 10) happiness = 10;

                            hunger += 1; // Slight hunger increase
                            if (hunger > 10) hunger = 10;

                            Console.WriteLine($"\nYou played with {petName}. Happiness increased but hunger rose slightly.");
                            SimulateTime(ref hunger, ref happiness, ref health, petName);
                        }
                        break;

                    case "3": // Resting
                        health += 2;
                        if (health > 10) health = 10;

                        happiness -= 1; // Slight happiness decrease
                        if (happiness < 1) happiness = 1;

                        Console.WriteLine($"\n{petName} rested. Health improved but happiness dropped slightly.");
                        SimulateTime(ref hunger, ref happiness, ref health, petName);
                        break;

                    case "4": // Check Status
                        Console.WriteLine($"\n📋 {petName}'s Status:");
                        Console.WriteLine($"- Hunger: {hunger}");
                        Console.WriteLine($"- Happiness: {happiness}");
                        Console.WriteLine($"- Health: {health}");

                        if (hunger >= 9)
                            Console.WriteLine("⚠️  Warning: Hunger is very high!");
                        if (happiness <= 2)
                            Console.WriteLine("⚠️  Warning: Happiness is very low!");
                        if (health <= 2)
                            Console.WriteLine("⚠️  Warning: Health is very low!");
                        break;

                    case "5": // Exit
                        running = false;
                        Console.WriteLine("\nGoodbye! Thanks for playing.");
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
            }
        }

        // --- Time Simulation with Smart Health Decay ---
        static void SimulateTime(ref int hunger, ref int happiness, ref int health, string petName)
        {
            // Save original values BEFORE time effects
            int originalHunger = hunger;
            int originalHappiness = happiness;

            // Simulate time passing
            hunger += 1;
            if (hunger > 10) hunger = 10;

            happiness -= 1;
            if (happiness < 1) happiness = 1;

            // Decay health ONLY if hunger/happiness were critical before action
            if (originalHunger >= 9 || originalHappiness <= 2)
            {
                health -= 1;
                if (health < 1) health = 1;

                Console.WriteLine($"\n  {petName}'s health is deteriorating due to neglect!");
            }
        }
    }
}
