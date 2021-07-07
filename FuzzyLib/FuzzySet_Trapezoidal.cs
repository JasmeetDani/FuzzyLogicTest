
namespace FuzzyLogic
{
	// Definition of a fuzzy set that has a trapezoidal shape
	// The maximum value this variable can accept is any value (including) between the left and the
	// right peak points
	public class FuzzySet_Trapezoidal : FuzzySet
	{
		// The values that define the shape of this FLV
		private double dLeftPeakPoint;
		private double dRightPeakPoint;
		private double dLeftOffset;
		private double dRightOffset;
		
		public FuzzySet_Trapezoidal(double peakLeft, double peakRight, 
		                              double leftOffset, double rightOffset) : 
			base((peakLeft + peakRight) / 2)
		{
			dLeftPeakPoint = peakLeft;
			dRightPeakPoint = peakRight;
			dLeftOffset = leftOffset;
			dRightOffset = rightOffset;
		}
		
		// This method calculates the degree of membership for a particular value
		public override double calculateDOM(double val) 
		{
			// Test for the case where the left or right offsets are zero
			// (to prevent divide by zero errors below)
			if (((dRightOffset == 0.0) && (dRightPeakPoint == val))
			    || ((dLeftOffset == 0.0) && (dLeftPeakPoint == val)))
			{
				return 1.0;
			}
			else 
			{
				// Find DOM if left of left peak
				if ((val <= dLeftPeakPoint) && (val > (dLeftPeakPoint - dLeftOffset)))
				{
					double grad = 1.0 / dLeftOffset;
					
					// Below could also have been written as : grad * (val - dLeftPeakPoint) + 1.0
					// TODO : (revisit)
					return grad * (val - (dLeftPeakPoint - dLeftOffset));
				}
				else
				{
					// Find DOM if right of right peak and less than right peak + right offset
					if ((val >= dRightPeakPoint) && (val < (dRightPeakPoint + dRightOffset)))
					{
						double grad = 1.0 / -dRightOffset;
						
						return grad * (val - dRightPeakPoint) + 1.0;
					}
					else
					{
						// Find DOM if right of left peak and left of right peak
						if((val < dRightPeakPoint) && (val > dLeftPeakPoint))
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
}