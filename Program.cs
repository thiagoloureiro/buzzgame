using System;
using System.Linq;
using System.Speech.Synthesis;

namespace ConsoleApp12
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (int i = 1; i < 100; i++)
            {
                var buzzes = Check7and4(i);

                if (i % 4 == 0)
                {
                    if (i / 4 == 4)
                    {
                        buzzes++;
                    }

                    buzzes++;
                }

                if (i % 7 == 0)
                {
                    if (i / 7 == 7)
                    {
                        buzzes++;
                    }
                    buzzes++;
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

        private static int Check7and4(int number)
        {
            int contains = 0;

            contains += number.ToString().Count(x => x == '4');
            contains += number.ToString().Count(x => x == '7');

            return contains;
        }
    }
}