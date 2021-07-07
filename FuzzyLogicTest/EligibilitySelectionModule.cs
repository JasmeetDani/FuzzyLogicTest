using FuzzyLogic;

namespace FuzzyLogicTest
{
    public class EligibilitySelectionModule : FuzzyModule
    {
		// Fuzzy sets

		// Age bracket
		private FzSet YOUNG = null;
		private FzSet MIDDLE_AGED = null;
		private FzSet OLD = null;

		// Vaccine Desirability
		private FzSet VACCINE_UNDESIRABLE = null;
		private FzSet VACCINE_DESIRABLE = null;
		private FzSet VACCINE_VERY_DESIRABLE = null;


		public EligibilitySelectionModule()
		{
			FuzzyVariable AGE_CATEGORY = createFLV(EligibilitySelectionInputFLVs.MEMBER_AGE);

			FuzzyVariable VACCINE_DESIRABILITY = createFLV(EligibilitySelectionOutputFLVs.VACCINE_DESIRABILITY);


			YOUNG = AGE_CATEGORY.AddLeftShoulderSet(AGE_BRACKET.YOUNG, 0, 25, 40);

			MIDDLE_AGED = AGE_CATEGORY.AddTriangularSet(AGE_BRACKET.MIDDLE_AGED, 30, 40, 55);

			OLD = AGE_CATEGORY.AddRightShoulderSet(AGE_BRACKET.OLD, 40, 50, double.MaxValue);


			VACCINE_UNDESIRABLE = VACCINE_DESIRABILITY.AddLeftShoulderSet(DESIRABILITY.UNDESIRABLE, 0, 25, 50);

			VACCINE_DESIRABLE = VACCINE_DESIRABILITY.AddTriangularSet(DESIRABILITY.DESIRABLE, 25, 50, 75);

			VACCINE_VERY_DESIRABLE = VACCINE_DESIRABILITY.AddRightShoulderSet(DESIRABILITY.VERY_DESIRABLE, 50, 75, 100);


			addRule(OR(MIDDLE_AGED, OLD), VACCINE_VERY_DESIRABLE);

			addRule(YOUNG, VACCINE_DESIRABLE);
		}
	}
}