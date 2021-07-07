using System.Collections.Generic;

namespace FuzzyLogic
{
	// Fuzzy OR operator class
	public class FzOR : FuzzyTerm
	{
		private List<FuzzyTerm> terms = new List<FuzzyTerm>();

		public FzOR(params FuzzyTerm[] terms)
		{
			for (int i = 0; i < terms.Length; i++)
			{
				this.terms.Add(terms[i]);
			}    
		}
		
		// The OR operator returns the maximum DOM of the sets it is operating on
		public override double getDOM() 
		{
			double largest = double.MinValue;
			
			foreach(FuzzyTerm term in terms)
			{
				double tempDOM = term.getDOM();
				
				if(tempDOM > largest)
				{
					largest = tempDOM;
				}
			}
			
			return largest;
		}
		
		public override void clearDOM() 
		{
			// Unused
		}
		
		public override void ORwithDOM(double val) 
		{
			// Unused
		}
	}
}