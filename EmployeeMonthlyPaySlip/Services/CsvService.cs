using EmployeeMonthlyPaySlip.Entities;
using EmployeeMonthlyPaySlip.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmployeeMonthlyPaySlip.Services
{
	public class CsvService : ICsvService
	{
		public List<EmployeeInfo> GetEmployeeInfoList(string filePath)
		{
			List<EmployeeInfo> results = new List<EmployeeInfo>();
			try
			{
				using (var fs = File.OpenRead(filePath))
				using (var reader = new StreamReader(fs))
				{
					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var values = line.Split(',');
						results.Add(new EmployeeInfo()
						{
							FirstName = values[0],
							LastName = values[1],
							AnnualSalary = double.Parse(values[2]),
							SuperRate = values[3],
							PayPeriod = values[4]
						});
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			return results;
		}

		public void ExportToCsv(List<PaySlip> paySlips, string filePath)
		{
			var csv = new StringBuilder();
			foreach (var item in paySlips)
			{
				csv.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", item.FullName, item.PayPeriod, item.GrossIncome, item.IncomeTax, item.NetIncome, item.Super));
			}
			File.WriteAllText(filePath, csv.ToString());
		}
	}
}
