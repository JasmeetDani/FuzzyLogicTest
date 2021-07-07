
namespace FuzzyLogic
{
	// Abstract class to provide an interface for classes to able to be
	// used as terms in a fuzzy if-then rule base
	public abstract class FuzzyTerm
	{
		// Retrieves the degree of membership of the term
     	public abstract double getDOM();
		
		// Clears the degree of membership of the term
     	public abstract void clearDOM();
		
		// Method for updating the DOM of a consequent when a rule fires
     	public abstract void ORwithDOM(double val);
	}
}