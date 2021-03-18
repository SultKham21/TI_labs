using System;

namespace TI
{
    class Program
    {
        static void Main(string[] args)
        {
            int num;
            string userChoice;

            do
            {
                Console.WriteLine("\nВыберите метод шифрования: ");
                Console.WriteLine("1: Поворачивающаяся решетка");
                Console.WriteLine("2: Железнодорожная изгородь");
                Console.WriteLine("3: Столбцовый метод");
                Console.WriteLine("4: Шифр Виженера");
                Console.Write("\nНомер пункта: ");
                userChoice = Console.ReadLine();

                if(!Int32.TryParse(userChoice, out num)) continue;

                if (userChoice == "1")
                {
                    RotatingGrillCipher.CallRotateGrill();
                }

                if (userChoice == "2")
                {
                    RailFenceCipher.CallRail();
                }

                if(userChoice == "3")
                {
                    ColumnarTranspositionCipher.CallColumnar();
                }

                if(userChoice == "4")
                {
                    VigenereCipher.CallVigenere();
                }

            } while (true);
        }
    }
}
