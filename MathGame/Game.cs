using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    internal class Game
    {
        Random rand;
        public int Rounds { get; private set; }
        public Level Difficulty { get; private set; }
        public Type Operation { get; private set; }
        public int Results { get; private set; }
        public enum Level
        {
            Easy = 1, // max value 10**1
            Medium, // max value 10**2
            Hard // max value 10**3
        };

        public enum Type
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Random
        }


        public Game(int rounds, Level difficulty, Type operation)
        {
            rand = new Random();
            this.Rounds = rounds;
            this.Difficulty = difficulty;
            this.Operation = operation;
        }

        private void gamePrompt()
        {
            Console.WriteLine("Welcome to the Math Quiz game.");
            if (Rounds > 1)
            {
                Console.WriteLine($"You will be given {Rounds} problems to solve.");
            } 
            else
            {
                Console.WriteLine($"You will be given {Rounds} problem to solve.");
            }
            Console.WriteLine("All answers will be integers (no decimals answers).");
        }

        private List<int> getProblem()
        {
            // Randomly generate math problem
            List<int> prob = new List<int>();


            int op;
            if (Operation == Type.Random)
            {
                op = rand.Next(0, 4);
            }
            else
            {
                op = (int)Operation;
            }

            prob.Add(op);
            int max = (int) Math.Pow(10.0, (double)Difficulty);
            int a = rand.Next(0, 11);
            int b = rand.Next(1, 11);
            prob.Add(a);

            if (op == 0)
            {
                prob.Add(b);
                prob.Add(a + b);
            }
            else if (op == 1)
            {
                prob.Add(b);
                prob.Add(a - b);

            }
            else if (op == 2)
            {
                prob.Add(b);
                prob.Add(a * b);

            }
            else if (op == 3)
            {
                while (a % b != 0)
                {
                    b = rand.Next(1, 11);
                }
                prob.Add(b);
                prob.Add(a / b);
            }


            return prob;
        }
        
        public void Run()
        {
            gamePrompt();
            for (int i = 0; i < Rounds; i++)
            {
                List<int> prob = getProblem();

                String op = prob[0] switch
                {
                    0 => "+",
                    1 => "-",
                    2 => "*",
                    3 => "/",
                    _ => "?"
                };

                Console.Write($"{prob[1]} {op} {prob[2]} = ");

                try
                {
                    String? input = Console.ReadLine();
                    if (input != null) 
                    { 
                        if (input == "q")
                            break;

                        int user_answer = int.Parse(input);
                        if (user_answer == prob[3])
                        {
                            Console.WriteLine("\n\tCorrect!");
                            Results++;
                        } else
                        {
                            Console.WriteLine($"\n\tIncorrect! The answer is {prob[3]}");
                        }
                    }
                }
                catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
                {
                    Console.WriteLine("\nError in reading input\n Exiting game.");
                    break;
                }
            }
        }

        public String ResultsToString()
        {
            if (Rounds > 1)
            {
                return $"Won {Results} out of {Rounds} rounds";
            }
            else
            {
                return $"Won {Results} out of {Rounds} round.";
            }
        }

    }
}
