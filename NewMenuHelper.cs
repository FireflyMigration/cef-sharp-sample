using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.DevTools.Debugger;
using ENV.Data;
using Firefly.Box;

namespace Northwind.Demo
{
    internal class NewMenuHelper
    {
        public string Hello(string text)
        {
            return "Hello " + text;
        }

        Context _current = Context.Current;

        private T Run<T>(Func<T> what)
        {
            var result = default(T);
            var w = new System.Threading.ManualResetEvent(false);
            _current.BeginInvoke(() =>
            {
                result = what();
                w.Set();
            });
            w.WaitOne();
            return result;
        }

        public class Statistics
        {
            public List<int> Years = new List<int>();
            public List<int> Values = new List<int>();
        }
        public Statistics getStatistics()
        {
            var result = new Statistics();
            var count = 0;
            var previousYear = 0;
            var orders = new Models.Orders();
            var bp = new BusinessProcess { From = orders };
            bp.OrderBy.Add(orders.OrderDate);
            bp.ForEachRow(() =>
            {
                if (orders.OrderDate.Year != previousYear)
                {
                    if (previousYear != 0)
                    {
                        result.Years.Add(previousYear);
                        result.Values.Add(count);
                    }
                  
                }
                previousYear = orders.OrderDate.Year;
                count++;
            });
            result.Years.Add(previousYear);
            result.Values.Add(count );

            return result;


        }

        public string GetCustomerName(string customerId)
        {
            return Run(() =>
            {
                var c = new Models.Customers();
                return c.GetValue(c.CompanyName, c.CustomerID.IsEqualTo(customerId));

            });
        }
        public string selectCustomer(string customerId)
        {
            return Run(() =>
            {
                var tc = new TextColumn { Value = customerId };
                new Customers.ShowCustomers().Run(tc);
                return tc.Value.ToString().Trim();
            });
        }

        public void AButtonWasClicked(string text)
        {
            Run(() =>
            {
                switch (text)
                {


                    case "Orders":
                        new Orders.ShowOrders().Run();
                        break;
                    case "Customers":
                        new Customers.ShowCustomers().Run();
                        break;
                    case "Products":
                        new Products.ShowProducts().Run();
                        break;
                    case "Order Report":
                        new Orders.Print_Orders().Run();
                        break;

                }

                return false;
            });



        }
    }
}

