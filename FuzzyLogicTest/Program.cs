using System;

namespace FuzzyLogicTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EligibilitySelectionModule module = new EligibilitySelectionModule();

            while(true)
            {
                try
                {
                    Console.Write("Enter Age : ");

                    int inputAge = int.Parse(Console.ReadLine());

                    module.fuzzify(EligibilitySelectionInputFLVs.MEMBER_AGE, inputAge);

                    Console.WriteLine("Desirability Score : " + module.deFuzzify(EligibilitySelectionOutputFLVs.VACCINE_DESIRABILITY, FuzzyLogic.DefuzzifyMethod.CENTROID));
                }
                catch(Exception)
                {
                    break;
                }
            }
        }
    }
}