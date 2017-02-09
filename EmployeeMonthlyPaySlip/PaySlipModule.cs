using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMonthlyPaySlip.Repositories;
using EmployeeMonthlyPaySlip.Repositories.Interfaces;
using EmployeeMonthlyPaySlip.Services;
using EmployeeMonthlyPaySlip.Services.Interfaces;
using Ninject.Modules;

namespace EmployeeMonthlyPaySlip
{
	public class PaySlipModule : NinjectModule
	{

		public override void Load()
		{
			this.Bind<IPaySlipService>().To<PaySlipService>();
			this.Bind<ITaxCalculationService>().To<TaxCalculationService>();
			this.Bind<ITaxableRepository>().To<TaxableRepository>();
			this.Bind<ICsvService>().To<CsvService>();
		}
	}
}
