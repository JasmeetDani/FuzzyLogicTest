
namespace FuzzyLogic
{
	// This is a simple class to define fuzzy sets that have a triangular shape 
	// and can be defined by a mid point, a left displacement and a right displacement. 
	public class FuzzySet_Triangle : FuzzySet
	{
		// The values that define the shape of this FLV
		private double dPeakPoint;
		private double dLeft;
		private double dRight;
		
		public FuzzySet_Triangle(double mid, double left, double right) : base(mid)
		{
			dPeakPoint = mid;
			dLeft = left;
			dRight = right;
		}
		
		// This method calculates the degree of membership for a particular value
		public override double calculateDOM(double val) 
		{
			// Test for the case where the triangle's left or right offsets are zero
			//(to prevent divide by zero errors below)
			if (((dRight == 0.0) && (dPeakPoint == val)) || ((dLeft == 0.0) && (dPeakPoint == val)))
			{
				return 1.0;
			}
			else
			{
				// Find DOM if left of center
				if ((val <= dPeakPoint) && (val >= (dPeakPoint - dLeft))) {

					double grad = 1.0 / dLeft;

					// Below could also have been written as : grad * (val - dPeakPoint) + 1.0
					// TODO : (revisit)
					return grad * (val - (dPeakPoint - dLeft));
				} 
				else
				{
					// Find DOM if right of center
					if ((val > dPeakPoint) && (val < (dPeakPoint + dRight))) 
					{
						double grad = 1.0 / -dRight;

						return grad * (val - dPeakPoint) + 1.0;
					}
					else
					{
						// Out of range of this FLV, return zero
						return 0.0;
					}
				}
			}
		}
	}
}