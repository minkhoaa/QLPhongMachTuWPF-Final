using QLPhongMachTuWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace QLPhongMachTuWPF.Model
{
    public class DBSupport
    {
        private static DBSupport _ins;
        public static DBSupport Ins { get { if (_ins == null) { _ins = new DBSupport(); } return _ins; } }
        public QLPMTEntities db { get; set; }
        private DBSupport()
        {
            db = new QLPMTEntities();
        }
    }
}
