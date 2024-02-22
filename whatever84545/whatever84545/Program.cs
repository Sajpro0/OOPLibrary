using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace whatever84545
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(uhmmm(new string[] { "slovo", "vetsi nez 5", "neco" }, 5));
			Console.ReadKey(true);
		}

		static int uhmmm(string[] strArray, int minLength) => (from x in strArray where x.Length > minLength select x).Count();
	}
}
