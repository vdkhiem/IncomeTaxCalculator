using System;

namespace EmployeeMonthlyPaySlip.Entities
{
	public class PaySlip : EmployeeInfo
	{
		public int GrossIncome
		{
			get
			{
				return (int)Math.Round(AnnualSalary / 12);
			}
		}

		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}

		public int IncomeTax { get; set; }
		public int NetIncome { get; set; }
		public int Super { get; set; }
	}
}
