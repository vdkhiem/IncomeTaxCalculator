using System.Collections.Generic;
using EmployeeMonthlyPaySlip.Entities;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using EmployeeMonthlyPaySlip.Repositories.Interfaces;
using EmployeeMonthlyPaySlip.Services;
using EmployeeMonthlyPaySlip.Services.Interfaces;
using EmployeeMonthlyPaySlip.Exceptions;

namespace EmployeeMonthlyPaySlip.Tests
{
	[TestFixture]
	public class PaySlipServiceTest
	{
		private Mock<ITaxCalculationService> _taxCalculationServiceMock;
		private PaySlipService _paySlipService;
		private EmployeeInfo _employeeInfo;

		[SetUp]
		public void Setup()
		{
			_taxCalculationServiceMock = new Mock<ITaxCalculationService>();
			_paySlipService = new PaySlipService(_taxCalculationServiceMock.Object);
			_employeeInfo = new EmployeeInfo()
			{
				FirstName = "Ryan",
				LastName = "Chen",
				AnnualSalary = 120000,
				SuperRate = "10%",
				PayPeriod = "01 March – 31 March"
			};
		}

		[Test]
		public void PaySlipService_Validate_NegativeAnnualSalary()
		{
			Assert.Throws<NegativeAnnualSalaryException>(
				new TestDelegate(ThrowNegativeAnnualSalaryException));
		}

		[Test]
		public void PaySlipService_Validate_InvalidSuperRate_Greater50()
		{
			Assert.Throws(typeof(InvalidSuperRateException),
				new TestDelegate(ThrowInvalidSuperRateException_Greater50));
		}

		[Test]
		public void PaySlipService_Validate_InvalidSuperRate_NegativeValue()
		{
			Assert.Throws(typeof(InvalidSuperRateException),
				new TestDelegate(ThrowInvalidSuperRateException_Negative));
		}

		[Test]
		public void PaySlipService_GetFullName()
		{
			var paySlip = _paySlipService.GetPaySlip(_employeeInfo);
			Assert.AreEqual(paySlip.FullName, "Ryan Chen");
		}

		[Test]
		public void PaySlipService_GetGrossIncome()
		{
			var paySlip = _paySlipService.GetPaySlip(_employeeInfo);
			Assert.AreEqual(paySlip.GrossIncome, 10000);
		}

		[Test]
		public void PaySlipService_GetIncomeTax()
		{
			_taxCalculationServiceMock.Setup(x => x.CalculateIncomeTax(It.IsAny<double>())).Returns(123);
			var paySlip = _paySlipService.GetPaySlip(_employeeInfo);
			Assert.AreEqual(paySlip.IncomeTax, 123);
		}

		[Test]
		public void PaySlipService_GetNetIncome()
		{
			_taxCalculationServiceMock.Setup(x => x.CalculateIncomeTax(It.IsAny<double>())).Returns(123);
			var paySlip = _paySlipService.GetPaySlip(_employeeInfo);
			Assert.AreEqual(paySlip.NetIncome, paySlip.GrossIncome - 123);
		}

		[Test]
		public void PaySlipService_GetSuper()
		{
			_taxCalculationServiceMock.Setup(x => x.CalculateIncomeTax(It.IsAny<double>())).Returns(123);
			var paySlip = _paySlipService.GetPaySlip(_employeeInfo);
			Assert.AreEqual(paySlip.Super, (int)(paySlip.GrossIncome * 0.1));
		}

		#region Helper

		private void ThrowNegativeAnnualSalaryException()
		{
			_employeeInfo.AnnualSalary = -1;
			_paySlipService.GetPaySlip(_employeeInfo);
		}

		private void ThrowInvalidSuperRateException_Greater50()
		{
			_employeeInfo.SuperRate = "51%";
			var paySlip = _paySlipService.GetPaySlip(_employeeInfo);
		}

		private void ThrowInvalidSuperRateException_Negative()
		{
			_employeeInfo.SuperRate = "-1%";
			var paySlip = _paySlipService.GetPaySlip(_employeeInfo);
		}

		#endregion
	}
}
