using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace MathGame
{
    internal class Program
    {
        /** 
         * Math Quiz game 
         */
        static void Main(string[] args)
        {
            List<Game> games = new List<Game>();

            while (true)
            {
                int? diffInput = null;
                int? typeInput = null;
                int? rounds = null;
                Console.WriteLine("Enter the game Settings (q to Quit): ");
                while (diffInput == null)
                {
                    diffInput = getDifficulty();
                    if (diffInput == -1) return;
                }
                while (typeInput == null)
                {

                    typeInput = getType();
                    if (typeInput == -1) return;
                }
                while (rounds == null)
                {
                    rounds = getRounds();
                    if (rounds == -1) return;
                }

                Game game = new Game( (10 + (rounds.Value * 5)), (Game.Level)diffInput, (Game.Type)typeInput);
                game.Run();
                games.Add(game);
                Console.WriteLine(game.ResultsToString());
                Console.WriteLine("Would you like to play again? ('p' to see previous games history");
                int? input = getInput(['y', 'n', 'p']);
                if (input != null && input.Value == -3) {
                    break;
                } 
                if (input != null && input.Value == -4)
                {
                    Console.WriteLine("Previous Games:");
                    foreach (Game g in games)
                    {
                        Console.WriteLine(g.ResultsToString());
                    }
                }
            }

        }


        static int? getInput(List<char> validInputs)
        {
            int? input = null;
            String? inputRaw = Console.ReadLine();
            if (inputRaw != null && inputRaw.Length == 1)
            {
                char inputChar = (char)inputRaw[0];
                if (validInputs.Contains(inputChar))
                {
                    if (inputChar == 'q')
                        return -1;
                    if (Char.ToLower(inputChar) == 'y')
                        return -2;
                    if (Char.ToLower(inputChar) == 'n')
                        return -3;
                    if (char.ToLower(inputChar) == 'p')
                        return -4;

                    if (Char.IsNumber(inputChar))
                        input = int.Parse(inputChar.ToString());
                }
                else
                {
                    Console.WriteLine("Not a valid option.");
                    return null;
                }
            }

            return input;

        }
        static int? getDifficulty()
        {

            Console.WriteLine("Difficulty:");
            Console.WriteLine("\t1. Easy: Max value of operand is 10.");
            Console.WriteLine("\t2. Medium: Max value of operand is 100.");
            Console.WriteLine("\t3. Hard: Max value of operand is 1000.");
            return getInput(['1', '2', '3', 'q']);

        }

        static int? getType()
        {
            Console.WriteLine("Type:");
            Console.WriteLine("\t0. Addition");
            Console.WriteLine("\t1. Subtraction");
            Console.WriteLine("\t2. Muliplication");
            Console.WriteLine("\t3. Division");
            Console.WriteLine("\t4. Random");
            return getInput(['0', '1', '2', '3', '4', 'q']);
        }

        static int? getRounds()
        {
            Console.WriteLine("Rounds: ");
            Console.WriteLine("\t0. 10 Rounds");
            Console.WriteLine("\t1. 15 Rounds");
            Console.WriteLine("\t2. 20 Rounds");
            Console.WriteLine("\t3. 25 Rounds");
            return getInput(['0', '1', '2', '3', 'q']);
        }

    }
}
