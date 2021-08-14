using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerPartsTracker.Shared.Models.Attributes
{
    public class ReceivedShippedDateAttribute : RangeAttribute
    {
        public ReceivedShippedDateAttribute()
          : base(typeof(DateTime),
                  DateTime.Today.AddYears(-1).ToShortDateString(),
                  DateTime.Today.AddDays(1).ToShortDateString())
        { }

    }
}