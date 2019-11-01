using System;
using System.Collections.Generic;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            if (weekEnds == null)
            {
                return startDate.AddDays(dayCount - 1);
            }

            List<DateTime> weekEndsBuffer = new List<DateTime>();

            foreach (var vacationDay in weekEnds)
            {
                if (vacationDay.StartDate > startDate.AddDays(dayCount - 1) || vacationDay.EndDate < startDate)
                {
                    continue;
                }

                for (DateTime date = vacationDay.StartDate; date <= vacationDay.EndDate; date = date.AddDays(1))
                {
                    weekEndsBuffer.Add(date);
                }
            }

            int counter = 0;
            DateTime currentDate = startDate;
            while (counter < dayCount)
            {
                if (weekEndsBuffer.IndexOf(currentDate) == -1)
                {
                    counter++;
                }
                currentDate = currentDate.AddDays(1);
            }

            return currentDate.AddDays(-1);
        }
    }
}
