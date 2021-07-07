using System;

namespace FuzzyLogic
{
	public class FzFairly : FuzzyTerm
	{
		private FuzzySet set;

		public FzFairly(FzSet ft) 
		{
			set = ft.set;
		}

		public override double getDOM() 
		{
			return Math.Sqrt(set.getDOM());
		}

		public override void clearDOM() 
		{
			set.clearDOM();
		}

		public override void ORwithDOM(double val) 
		{
			set.ORwithDOM(Math.Sqrt(val));
		}
	}
}