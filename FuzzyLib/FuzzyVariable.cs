using System;
using System.Collections.Generic;

namespace FuzzyLogic
{
	// The class to define a fuzzy linguistic variable (FLV)
	// An FLV comprises of a number of fuzzy sets  
	public class FuzzyVariable
	{
		// A map of the fuzzy sets that comprise this variable
		private Dictionary<Enum, FuzzySet> memberSets = new Dictionary<Enum, FuzzySet>();

		// The minimum and maximum value of the range of this variable
		private double dMinRange;
		private double dMaxRange;

		public FuzzyVariable()
		{
			dMinRange = 0;
			dMaxRange = 0;
		}

		// This method is called with the upper and lower bound of a set each time a
		// new set is added to adjust the upper and lower range values accordingly
		private void adjustRangeToFit(double minBound, double maxBound) 
		{
			if (minBound < dMinRange) 
			{
				dMinRange = minBound;
			}

			if (maxBound > dMaxRange) 
			{
				dMaxRange = maxBound;
			}
		}

		// The following methods create instances of the sets named in the method
		// name and add them to the member set map. Each time a set of any type is
		// added the dMinRange and dMaxRange are adjusted accordingly. All of the
		// methods return a proxy class representing the newly created instance. This
		// proxy set can be used as an operand when creating the rule base.

		// Adds a trapeziodal type set
		public FzSet AddTrapezoidalSet(Enum name, double minBound, double leftPeak, double rightPeak,
		                               double maxBound) 
		{
			memberSets.Add(name, new FuzzySet_Trapezoidal(leftPeak, rightPeak, 
			                                              leftPeak - minBound, maxBound - rightPeak));
			
			// Adjust range if necessary
			adjustRangeToFit(minBound, maxBound);
			
			return new FzSet(memberSets[name]);
		}

		// Adds a left shoulder type set
		public FzSet AddLeftShoulderSet(Enum name, double minBound, double peak, double maxBound) 
		{
			memberSets.Add(name, new FuzzySet_LeftShoulder(peak, peak - minBound, maxBound - peak));
			
			// Adjust range if necessary
			adjustRangeToFit(minBound, maxBound);
			
			return new FzSet(memberSets[name]);
		}

		// Adds a left shoulder type set
		public FzSet AddRightShoulderSet(Enum name, double minBound, double peak, double maxBound) 
		{
			memberSets.Add(name, new FuzzySet_RightShoulder(peak, peak - minBound, maxBound - peak));
			
			// Adjust range if necessary
			adjustRangeToFit(minBound, maxBound);
			
			return new FzSet(memberSets[name]);
		}

		// Adds a triangular shaped fuzzy set to the variable
		public FzSet AddTriangularSet(Enum name, double minBound, double peak, double maxBound) 
		{
			memberSets.Add(name, new FuzzySet_Triangle(peak, peak - minBound, maxBound - peak));

			// Adjust range if necessary
			adjustRangeToFit(minBound, maxBound);
			
			return new FzSet(memberSets[name]);
		}

		// Adds a singleton to the variable
		public FzSet AddSingletonSet(Enum name, double minBound, double peak, double maxBound) 
		{
			memberSets.Add(name, new FuzzySet_Singleton(peak, peak - minBound, maxBound - peak));
			
			adjustRangeToFit(minBound, maxBound);
			
			return new FzSet(memberSets[name]);
		}

		// Fuzzify a value by calculating its DOM in each of this variable's subsets
		// takes a crisp value and calculates its degree of membership for each set
		// in the variable
		public void fuzzify(double val) 
		{
			// No range checks here, TODO : (revisit)
			
			// For each set in the flv calculate the DOM for the given value
			// TODO (revisit) : Any more efficient way of traversing a dictionary values
			foreach (FuzzySet entry in memberSets.Values) 
			{
				entry.SetDOM(entry.calculateDOM(val));
			}
		}

		// Defuzzifies the value by averaging the maxima of the sets that have fired
		// OUTPUT = sum (maxima * DOM) / sum (DOMs) 
		public double deFuzzifyMaxAv() 
		{
			double bottom = 0.0;
			double top = 0.0;

			// TODO (revisit) : Any more efficient way of traversing a dictionary values
			foreach (FuzzySet entry in memberSets.Values) 
			{
				bottom += entry.getDOM();
				top += entry.getRepresentativeVal() * entry.getDOM();
			}

			// Make sure bottom is not equal to zero
			if(bottom == 0.0) 
			{
				return 0.0;
			}

			return top / bottom;
		}
		
		
		// Defuzzify the variable using the centroid method
		public double deFuzzifyCentroid(int numSamples) 
		{
			// Calculate the step size
			double stepSize = (dMaxRange - dMinRange) / numSamples;

			double totalArea = 0.0;
			double sumOfMoments = 0.0;

			// Step through the range of this variable in increments equal to stepSize
			// adding up the contribution (lower of calculateDOM or the actual DOM of this
			// variable's fuzzified value) for each subset. This gives an approximation of
			// the total area of the fuzzy manifold.(This is similar to how the area under
			// a curve is calculated using calculus... the heights of lots of 'slices' are
			// summed to give the total area.)
			// In addition the moment of each slice is calculated and summed. Dividing
			// the total area by the sum of the moments gives the centroid. (Just like
			// calculating the center of mass of an object)
			for (int samp = 1; samp <= numSamples; ++samp) 
			{
				// For each set get the contribution to the area. This is the lower of the 
				// value returned from calculateDOM or the actual DOM of the fuzzified 
				// value itself
				// TODO (revisit) : Any more efficient way of traversing a dictionary values
				foreach (FuzzySet entry in memberSets.Values) 
				{
					double contribution = Math.Min(entry.calculateDOM(dMinRange + samp * stepSize),
					                               entry.getDOM());
					
					totalArea += contribution;
					
					sumOfMoments += (dMinRange + samp * stepSize) * contribution;
				}
			}

			// Make sure total area is not equal to zero
			if(totalArea == 0.0)
			{
				return 0.0;
			}

			return sumOfMoments / totalArea;
		}
	}
}