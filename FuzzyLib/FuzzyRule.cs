
namespace FuzzyLogic
{
	// The class implements a fuzzy rule of the form:
	// IF fzVar1 AND fzVar2 AND ... fzVarn THEN fzVar.c
	public class FuzzyRule
	{
		// Antecedent (usually a composite of several fuzzy sets and operators)
     	private FuzzyTerm antecedent;

		// Consequence (usually a single fuzzy set, but can be several ANDed together)
		private FuzzyTerm consequence;

		public FuzzyRule(FuzzyTerm ant, FuzzyTerm con)
		{
			antecedent = ant;

			consequence = con;
		}


		// It doesn't make sense to allow clients to copy rules, TODO : find a way to do it in c#


		public void setConfidenceOfConsequentToZero() 
		{
			consequence.clearDOM();
		}

		// This method updates the DOM (the confidence) of the consequent term with
     	// the DOM of the antecedent term 
     	public void calculate() 
		{
			consequence.ORwithDOM(antecedent.getDOM());
		}
	}
}