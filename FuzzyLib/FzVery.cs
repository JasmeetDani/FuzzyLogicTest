
namespace FuzzyLogic
{
	public class FzVery : FuzzyTerm
	{
		private FuzzySet set;

		public FzVery(FzSet ft) 
		{
			set = ft.set;
		}
		
		public override double getDOM() 
		{
			double DOM = set.getDOM();

			return DOM * DOM;
		}
		
		public override void clearDOM() 
		{
			set.clearDOM();
		}
		
		public override void ORwithDOM(double val) 
		{
			set.ORwithDOM(val * val);
		}
	}
}