
namespace FuzzyLogic
{
	// Definition of a fuzzy set that has a left shoulder shape
	// The minimum value this variable can accept is any value less than the midpoint : TODO (revisit)
	public class FuzzySet_LeftShoulder : FuzzySet
	{
		// The values that define the shape of this FLV
		private double dPeakPoint;
		private double dRightOffset;
		private double dLeftOffset;
		
		public FuzzySet_LeftShoulder(double peak, double leftOffset, double rightOffset) : 
			base(((peak - leftOffset) + peak) / 2)
		{
			dPeakPoint = peak;
			dLeftOffset = leftOffset;
			dRightOffset = rightOffset;
		}

		// This method calculates the degree of membership for a particular value
     	public override double calculateDOM(double val) 
		{
			// Test for the case where the left or right offsets are zero
			// (to prevent divide by zero errors below)
			if (((dRightOffset == 0.0) && (dPeakPoint == val))
				|| ((dLeftOffset == 0.0) && (dPeakPoint == val)))
			{
				return 1.0;
			}
			else 
			{
				// Find DOM if right of center
				if ((val >= dPeakPoint) && (val < (dPeakPoint + dRightOffset)))
				{
					double grad = 1.0 / -dRightOffset;

					return grad * (val - dPeakPoint) + 1.0;
				}
				else
				{
					// Find DOM if left of center
					if ((val < dPeakPoint) && (val >= dPeakPoint - dLeftOffset)) 
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
	}
}