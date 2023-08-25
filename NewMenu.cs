using ENV;
using ENV.Data;
using Firefly.Box;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Northwind.Demo
{
    public class NewMenu : UIControllerBase
    {

        public NewMenu()
        {

        }

        public void Run()
        {
            Execute();
        }

        protected override void OnLoad()
        {
            View = () => new Views.NewMenuView(this);
        }
    }
}