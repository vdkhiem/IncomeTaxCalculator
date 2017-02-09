
namespace EmployeeMonthlyPaySlip.Services.Interfaces
{
	public interface ITaxCalculationService
	{
		double CalculateIncomeTax(double annualSalary);
	}
}
