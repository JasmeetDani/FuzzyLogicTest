
namespace FuzzyLogic
{
	// Definition of a fuzzy set that has a right shoulder shape
	// The maximum value this variable can accept is any value greater than the midpoint : TODO (revisit)
	public class FuzzySet_RightShoulder : FuzzySet
	{
		// The values that define the shape of this FLV
		private double dPeakPoint;
		private double dLeftOffset;
		private double dRightOffset;
				
		public FuzzySet_RightShoulder(double peak, double leftOffset, double rightOffset) : 
			base(((peak + rightOffset) + peak) / 2)
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
				// Find DOM if left of center
				if ((val <= dPeakPoint) && (val > (dPeakPoint - dLeftOffset)))
				{
					double grad = 1.0 / dLeftOffset;

					// Below could also have been written as : grad * (val - dPeakPoint) + 1.0
					// TODO : (revisit)
					return grad * (val - (dPeakPoint - dLeftOffset));
				}
				else
				{
					// Find DOM if right of center and less than center + right offset
					if ((val > dPeakPoint) && (val <= dPeakPoint + dRightOffset)) 
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