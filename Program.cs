using System;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;

namespace BuzzGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var specials = new int[] { 4, 7 };
            Debug.Assert(!specials.Contains(1));

            for (int i = 1; i < 100; i++)
            {
                var buzzes = CheckLiteralSpecials(specials, i);

                foreach (var s in specials)
                {
                    buzzes += NumBuzzesFromMultiples(s, i);
                }

                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child);
                synthesizer.Volume = 100;
                synthesizer.Rate = 7;

                if (buzzes > 0)
                {
                    Console.WriteLine(CreateBuzzString(buzzes));
                    synthesizer.Speak(CreateBuzzString(buzzes));
                }
                else
                {
                    Console.WriteLine(i.ToString());
                    synthesizer.Speak(i.ToString());
                }
            }
        }

        private static string CreateBuzzString(int buzzes)
        {
            string str = "";
            for (int i = 0; i < buzzes; i++)
            {
                str += "buzz ";
            }
            return str;
        }

        private static int NumBuzzesFromMultiples(int special, int number)
        {
            int buzzes = 0;
            int pow = 1;
            int n = number;
            while (n % special == 0)
            {
                ++buzzes;
                n /= special;
            }
            for (int i = 0; i < buzzes; ++i)
            {
                pow *= special;
            }
            if (pow == number) { return buzzes; }
            else
            {
                return (number % special == 0) ? 1 : 0;
            }
        }

        private static int CheckLiteralSpecials(int[] specials, int number)
        {
            int contains = 0;

            foreach (var s in specials)
            {
                contains += new Regex(s.ToString()).Matches(number.ToString()).Count;
            }

            return contains;
        }
    }
}