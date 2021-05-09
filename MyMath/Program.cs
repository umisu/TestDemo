using System;

namespace MyMath
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Szia, en kepes vagyok oszeadni 2 egesz szamot");
            Console.WriteLine("Irjd be az elso szamot:");
            String number1 = Console.ReadLine()[0].ToString();
            Console.WriteLine("Irjd be a masidik szamot:");
            String number2 = Console.ReadLine()[0].ToString();

            try
            {
                int eredmeny = new MyMathFunctions().sum(number1, number2);
                Console.WriteLine("Az eredmeny:" + eredmeny);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba torttent:" + ex.Message);
            }
        }
    }


    public class MyMathFunctions
    {
        public int sum(String v1, String v2)
        {
            
            return MyParse(v1) + MyParse(v2);
        }

        private int MyParse(string value)
        {
            int number;
            try
            {
                number = int.Parse(value);
            }
            catch (Exception) {
                throw new Exception("The given value is not a number: " + value);
            }
            return number;
        }
    }
}
