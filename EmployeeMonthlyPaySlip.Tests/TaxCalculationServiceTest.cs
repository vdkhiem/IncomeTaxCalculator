
using System.Collections.Generic;
using EmployeeMonthlyPaySlip.Entities;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using EmployeeMonthlyPaySlip.Repositories.Interfaces;
using EmployeeMonthlyPaySlip.Services;

namespace EmployeeMonthlyPaySlip.Tests
{
	[TestFixture]
	public class TaxCalculationServiceTest
	{
		private Mock<ITaxableRepository> _taxableRepositoryMock;
		private TaxCalculationService _taxCalculationService;

		[SetUp]
		public void Setup()
		{
			var incomeTaxes = new List<IncomeTax>();
			incomeTaxes.Add(new IncomeTax() { MinRange = 0, MaxRange = 18200, MinPayableTax = 0, ExtraTax = 0 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 18201, MaxRange = 37000, MinPayableTax = 0, ExtraTax = 0.19 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 37001, MaxRange = 80000, MinPayableTax = 3572, ExtraTax = 0.325 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 80001, MaxRange = 180000, MinPayableTax = 17547, ExtraTax = 0.37 });
			incomeTaxes.Add(new IncomeTax() { MinRange = 180001, MaxRange = int.MaxValue, MinPayableTax = 54547, ExtraTax = 0.45 });
			_taxableRepositoryMock = new Mock<ITaxableRepository>();
			_taxableRepositoryMock.Setup(x => x.GetIncomeTaxList()).Returns(incomeTaxes);
			_taxCalculationService = new TaxCalculationService(_taxableRepositoryMock.Object);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_Negative()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(-1);
			Assert.AreEqual(incomeTax, 0);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_Zero()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(0);
			Assert.AreEqual(incomeTax, 0);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_18200()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(18200);
			Assert.AreEqual(incomeTax, 0);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_18350()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(18350);
			Assert.AreEqual(incomeTax, 2);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_37000()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(37000);
			Assert.AreEqual(incomeTax, 298);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_37001()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(37001);
			Assert.AreEqual(incomeTax, 298);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_80000()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(80000);
			Assert.AreEqual(incomeTax, 1462);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_80001()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(80001);
			Assert.AreEqual(incomeTax, 1462);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_180000()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(180000);
			Assert.AreEqual(incomeTax, 4546);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_180001()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(180001);
			Assert.AreEqual(incomeTax, 4546);
		}

		[Test]
		public void TaxCalculationService_CalculateIncomeTax_AnnualSalary_200000()
		{
			var incomeTax = _taxCalculationService.CalculateIncomeTax(200000);
			Assert.AreEqual(incomeTax, 5296);
		}
	}
}
