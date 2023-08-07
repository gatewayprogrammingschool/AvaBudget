using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;

public partial class DayOfMonthSchedule : ObservableValidator, ISchedule<DayOfMonthScheduleTempo>
{
    [CustomValidation(
        typeof(DayOfWeekSchedule),
        nameof(ValidateAllProperties),
        ErrorMessage = "Invalid Tempo for Day of Week Schedule.")]
    [ObservableProperty]
    public DayOfMonthScheduleTempo _tempo;

    [ObservableProperty]
    public DateOnly _anchorDate;

    public DayOfMonthSchedule(DayOfMonthScheduleTempo tempo, DateOnly anchorDate)
    {
        Tempo = tempo;
        AnchorDate = anchorDate;

        base.ErrorsChanged += DayOfMonthSchedule_ErrorsChanged;
    }

    private void DayOfMonthSchedule_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
    }

    public List<CalendarDay> ProjectedDates(int periods = 1)
    {
        return new();
    }
}
