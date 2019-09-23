using System;
using System.IO;
using System.Text.RegularExpressions;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose the program:\n[1] Row of numbers\n[2] Fibonacci\n[3] Roman number");
            string input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Incorrect input");
                return;
            }
            switch (input)
                {
                case "1":
                    RowOfInts();
                    break;
                case "2":
                    if (args.Length == 1)
                    {

                        if (!ulong.TryParse(args[0], out ulong n))
                        {
                            Console.WriteLine("Incorrect input");
                        }
                        else
                        {
                            Fibonacci(n);
                        }
                    }
                    
                    break;
                case "3":
                    RomanNumbers();
                    break;
                default:
                    Console.WriteLine("Incorrect input");
                    return;

            }
        }

        public static void RomanNumbers()
        {
            Console.WriteLine("Enter roman number");
            string input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Incorrect input");
                return;
            }

            input = input.ToUpper();

            if (input.Length == 0)
            {
                Console.WriteLine("Nothing was entered");
                return;
            }

            Regex isRoman = new Regex(@"^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$");
            if (!isRoman.IsMatch(input))
            {
                Console.WriteLine("Your number is not Roman");
                return;
            }

            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case 'M':
                        count += 1000;
                        break;
                    case 'D':
                        count += 500;
                        break;
                    case 'C':
                        if (i + 1 < input.Length && (input[i + 1] == 'D' || input[i + 1] == 'M'))
                            count -= 100;
                        else
                            count += 100;
                        break;
                    case 'L':
                        count += 50;
                        break;
                    case 'X':
                        if (i + 1 < input.Length && (input[i + 1] == 'L' || input[i + 1] == 'C' || input[i + 1] == 'M'))
                            count -= 10;
                        else
                            count += 10;

                        break;
                    case 'V':
                        count += 5;
                        break;
                    case 'I':
                        if (i + 1 < input.Length && (input[i + 1] == 'V' || input[i + 1] == 'X' || input[i + 1] == 'C'))
                            count -= 1;
                        else
                            count += 1;

                        break;
                }
            }

            Console.WriteLine(count);
        }

        public static void Fibonacci(ulong n)
        {
            ulong first = 0;
            ulong second = 0;
            for (ulong i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    first = 1;
                    Console.Write(first);
                }
                else if (i == 1)
                {
                    second = 1;
                    Console.Write(" {0}", second);
                }
                else
                {
                    ulong current = first + second;
                    Console.Write(" {0}", current);
                    first = second;
                    second = current;
                }

            }
        }

        public static void RowOfInts()
        {
            try
            {
                string row;
                string pathOfInput = "input.txt";
                string pathOfOutput = "output.txt";
                using (StreamReader file = new StreamReader(pathOfInput))
                {
                    row = file.ReadLine();
                }

                if (row == null)
                {
                    Console.WriteLine("Incorrect input");
                    return;
                }

                string[] words = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //int[] array = Array.ConvertAll(str.Split(), int.Parse);

                long sum = 0;
                foreach (string word in words)
                {
                    if (!long.TryParse(word, out long current))
                    {
                        Console.WriteLine("Incorrect input");
                        return;
                    }

                    sum += current;
                }
                using (StreamWriter file = new StreamWriter(pathOfOutput))
                {
                    file.WriteLine(sum);
                }

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
