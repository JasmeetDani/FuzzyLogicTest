using System;
using System.Collections.Generic;

namespace FuzzyLogic
{
	// REF : Programming Game AI by example : Matt Buckland
	// The class describes a fuzzy module: a collection of fuzzy variables
	// and the rules that operate on them
	public class FuzzyModule
	{
		// A map of all the fuzzy variables this module uses
		private Dictionary<Enum, FuzzyVariable> vars = new Dictionary<Enum, FuzzyVariable>();

		// A list containing all the fuzzy rules
		private List<FuzzyRule> rules = new List<FuzzyRule>();

		// When calculating the centroid of the fuzzy manifold this value is used
		// to determine how many cross sections should be sampled
		private const int numSamplesToUseForCentroid = 15;


		// Zeros the DOMs of the consequents of each rule. Used by Defuzzify()
     	private void setConfidencesOfConsequentsToZero() 
		{
			foreach (FuzzyRule rule in rules)
			{
				rule.setConfidenceOfConsequentToZero();
			}
		}

		// Creates a new 'empty' fuzzy variable and returns a reference to it.
     	public FuzzyVariable createFLV(Enum varName) 
		{
			// No checks for duplication here, TODO : (revisit)

			FuzzyVariable var = new FuzzyVariable ();

			vars.Add (varName, var);

			return var;
		}

		// Adds a rule to the module
		public void addRule(FuzzyTerm antecedent, FuzzyTerm consequence) 
		{
			rules.Add(new FuzzyRule(antecedent, consequence));
		}

		// This method calls the Fuzzify method of the variable with the same name as the key 
		public void fuzzify(Enum nameOfFLV, double val) 
		{
			FuzzyVariable var;

			if(vars.TryGetValue(nameOfFLV, out var))
			{
				var.fuzzify(val);
			}
		}

		// Given a fuzzy variable and a deffuzification method this returns a crisp value
		public double deFuzzify(Enum nameOfFLV, DefuzzifyMethod method) 
		{
			FuzzyVariable var;
			
			if(vars.TryGetValue(nameOfFLV, out var))
			{
				// Clear the DOMs of all the consequents of all the rules
				setConfidencesOfConsequentsToZero();
				
				// Process the rules
				foreach (FuzzyRule rule in rules)
				{
					rule.calculate();
				}

				// Now defuzzify the resultant conclusion using the specified method
				switch (method)
				{
					case DefuzzifyMethod.CENTROID:
						return var.deFuzzifyCentroid(numSamplesToUseForCentroid);
						
					case DefuzzifyMethod.MAX_AV:
						return var.deFuzzifyMaxAv();
				}
			}
			
			return 0;
		}


		// Helper functions to enhance readability of rule definitions

		public FzAND AND(params FuzzyTerm[] terms)
		{
			return new FzAND(terms);
		}

		public FzOR OR(params FuzzyTerm[] terms)
		{
			return new FzOR(terms);    
		}

		public FzFairly Fairly(FzSet set)
		{
			return new FzFairly(set);    
		}

		public FzVery Very(FzSet set)
		{
			return new FzVery(set);    
		}
	}
}