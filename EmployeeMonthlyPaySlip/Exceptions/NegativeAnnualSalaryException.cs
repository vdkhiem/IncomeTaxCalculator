using System;
using System.Runtime.Serialization;

namespace EmployeeMonthlyPaySlip.Exceptions
{
	public class NegativeAnnualSalaryException : Exception
	{
		public NegativeAnnualSalaryException() { }
		public NegativeAnnualSalaryException(string message) : base(message) { }
		public NegativeAnnualSalaryException(string message, Exception inner) : base(message, inner) { }

		protected NegativeAnnualSalaryException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
