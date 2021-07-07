
namespace FuzzyLogic
{
	// Class to define an interface for a fuzzy set
	public abstract class FuzzySet
	{
		// This will hold the degree of membership of a given value in this set 
     	protected double dDOM;

		// This is the maximum of the set's membership function. For instance, if
     	// the set is triangular then this will be the peak point of the triangle.
     	// if the set has a plateau then this value will be the mid point of the 
     	// plateau. This value is set in the constructor to avoid run-time
		// calculation of mid-point values.
     	protected double dRepresentativeValue;
		
		public FuzzySet(double repVal) 
		{
			dDOM = 0.0;
			dRepresentativeValue = repVal;
		}
		
		// Returns the degree of membership in this set of the given value. NOTE,
		// this does not set dDOM to the DOM of the value passed as the parameter.
		// This is because the centroid defuzzification method also uses this method
		// to determine the DOMs of the values it uses as its sample points.
     	public abstract double calculateDOM(double val);
		
		// If this fuzzy set is part of a consequent FLV, and it is fired by a rule 
     	// then this method sets the DOM (in this context, the DOM represents a
		// confidence level)to the maximum of the parameter value or the set's 
		// existing dDOM value
		public void ORwithDOM(double val)
		{
			if (val > dDOM) 
			{
				dDOM = val;
			}
		}
		
		public double getRepresentativeVal()
		{
			return dRepresentativeValue;
		}
		
		public void clearDOM() 
		{
			dDOM = 0;
		}
		
		public double getDOM() 
		{
			return dDOM;
		}
		
		public void SetDOM(double val) 
		{
			// No out of range checks for here, TODO : (revisit)
			// val should be between 0 and 1

			dDOM = val;
		}
	}
}