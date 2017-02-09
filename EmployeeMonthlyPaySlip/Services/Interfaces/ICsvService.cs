using EmployeeMonthlyPaySlip.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeMonthlyPaySlip.Services.Interfaces
{
	public interface ICsvService
	{
		List<EmployeeInfo> GetEmployeeInfoList(string filePath);
		void ExportToCsv(List<PaySlip> paySlips, string filePath);
	}
}
