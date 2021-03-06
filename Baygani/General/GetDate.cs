using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Baygani.General
{
    public class GetDate
    {
        public static string Today()
        {
            PersianCalendar pc = new PersianCalendar();
            string PDate= pc.GetYear(DateTime.Now).ToString() + "/" + pc.GetMonth(DateTime.Now).ToString() + "/" + pc.GetDayOfMonth(DateTime.Now).ToString();
            return PDate;
        }
    }
}
