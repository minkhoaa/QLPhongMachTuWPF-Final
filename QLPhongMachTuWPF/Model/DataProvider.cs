using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace QLPhongMachTuWPF.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider Ins { get { if (_ins == null) { _ins = new DataProvider(); } return _ins; } }
        public QLPMTEntities db { get; set; }
        private DataProvider()
        {
            db = new QLPMTEntities();
        }
    }
}
