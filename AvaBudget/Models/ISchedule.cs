namespace AvaBudget.Models;

public interface ISchedule<TScheduleTempo> where TScheduleTempo : Enum
{
    TScheduleTempo Tempo
    {
        get;
        set;
    }

    DateOnly AnchorDate
    {
        get;
        set;
    }

    List<CalendarDay> ProjectedDates(int periods=1);
}
