
namespace FuzzyLogic
{
	// This defines a fuzzy set that is a singleton (a range over which the DOM is always 1.0)
	public class FuzzySet_Singleton : FuzzySet
	{
		// The values that define the shape of this FLV
		private double dPeakPoint;
		private double dLeft;
		private double dRight;
		
		public FuzzySet_Singleton(double mid, double left, double right) : base(mid)
		{
			dPeakPoint = mid;
			dLeft = left;
			dRight = right;
		}
		
		// This method calculates the degree of membership for a particular value
		public override double calculateDOM(double val) 
		{ 
			if ((val >= dPeakPoint - dLeft) && (val <= dPeakPoint + dRight))
			{
				return 1.0;
			}
			else
			{
				// Out of range of this FLV, return zero
				return 0.0;
			}
		}
	}
}