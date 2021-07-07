
namespace FuzzyLogic
{
	// Class to provide a proxy for a fuzzy set. The proxy inherits from
	// FuzzyTerm and therefore can be used to create fuzzy rules
	public class FzSet : FuzzyTerm
	{
		public readonly FuzzySet set;
		
		public FzSet(FuzzySet fs) 
		{
			set = fs;
		}
	
		public override double getDOM() 
		{
			return set.getDOM();
		}
		
		public override void clearDOM() 
		{
			set.clearDOM();
		}
		
		public override void ORwithDOM(double val)
		{
			set.ORwithDOM(val);
		}
	}
}