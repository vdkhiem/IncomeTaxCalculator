using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EmployeeMonthlyPaySlip.Entities;
using EmployeeMonthlyPaySlip.Services;
using EmployeeMonthlyPaySlip.Services.Interfaces;
using Ninject;
using System.IO;

namespace EmployeeMonthlyPaySlip
{
	class Program
	{
		private static IPaySlipService _paySlipService;
		private static ICsvService _csvService;

		static void Main(string[] args)
		{
			try
			{
				RegisterServices();
				var filePath = AppDomain.CurrentDomain.BaseDirectory + "test.csv";
				var employeeInfos = _csvService.GetEmployeeInfoList(filePath);
				if (employeeInfos.Count <= 0)
					return;

				var paySlips = new List<PaySlip>();

				foreach (var item in employeeInfos)
				{
					var paySlip = _paySlipService.GetPaySlip(item);
					paySlips.Add(paySlip);
				}
				filePath = AppDomain.CurrentDomain.BaseDirectory + "testResult.csv";
				_csvService.ExportToCsv(paySlips, filePath);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to generate payslip");
				Console.ReadLine();
			}
		}

		private static void RegisterServices()
		{
			StandardKernel kernel = new StandardKernel();
			kernel.Load(new PaySlipModule());

			_paySlipService = kernel.Get<IPaySlipService>();
			_csvService = kernel.Get<ICsvService>();
		}
	}
}
