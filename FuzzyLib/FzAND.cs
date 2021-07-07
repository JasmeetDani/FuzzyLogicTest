using System.Collections.Generic;

namespace FuzzyLogic
{
	// Fuzzy AND operator class
	public class FzAND : FuzzyTerm
	{
		private List<FuzzyTerm> terms = new List<FuzzyTerm>();

		public FzAND(params FuzzyTerm[] terms)
		{
			for (int i = 0; i < terms.Length; i++)
			{
				this.terms.Add(terms[i]);
			}    
		}

		// The AND operator returns the minimum DOM of the sets it is operating on
     	public override double getDOM() 
		{
			double smallest = double.MaxValue;

			foreach(FuzzyTerm term in terms)
			{
				double tempDOM = term.getDOM();

				if(tempDOM < smallest)
				{
					smallest = tempDOM;
				}
			}

			return smallest;
		}
		
		public override void clearDOM() 
		{
			foreach(FuzzyTerm term in terms)
			{
				term.clearDOM();
			}
		}
		
		public override void ORwithDOM(double val) 
		{
			foreach(FuzzyTerm term in terms)
			{
				term.ORwithDOM(val);
			}
		}
	}
}