using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageCheckerAPI.Services.DateTimeService
{
    public class DateTimeService: IDateTimeService
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
