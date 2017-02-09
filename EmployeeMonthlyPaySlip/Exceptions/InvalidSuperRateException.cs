using System;
using System.Runtime.Serialization;

namespace EmployeeMonthlyPaySlip.Exceptions
{
	public class InvalidSuperRateException: Exception
	{
		public InvalidSuperRateException() { }
		public InvalidSuperRateException(string message) : base(message) { }
		public InvalidSuperRateException(string message, Exception inner) : base(message, inner) { }

		protected InvalidSuperRateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
