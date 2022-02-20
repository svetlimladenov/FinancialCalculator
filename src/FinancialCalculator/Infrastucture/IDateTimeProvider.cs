using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastucture
{
    public interface IDateTimeProvider
    {
        DateTime GetDate();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDate()
        {
            return DateTime.UtcNow;
        }
    }
}
