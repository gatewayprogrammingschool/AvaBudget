using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;

public partial class DayOfWeekSchedule : ObservableValidator, ISchedule<DayOfWeekScheduleTempo>
{
    [CustomValidation(
        typeof(DayOfWeekSchedule), 
        nameof(ValidateAllProperties), 
        ErrorMessage = "Invalid Tempo for Day of Week Schedule.")]
    [ObservableProperty]
    public DayOfWeekScheduleTempo _tempo;

    public DayOfWeekSchedule(DayOfWeekScheduleTempo tempo, DateOnly anchorDate)
    {
        Tempo = tempo;
        AnchorDate = anchorDate;

        base.ErrorsChanged += DayOfMonthSchedule_ErrorsChanged;
    }

    private void DayOfMonthSchedule_ErrorsChanged(object? sender, System.ComponentModel.DataErrorsChangedEventArgs e)
    {
    }
    [ObservableProperty]
    public DateOnly _anchorDate;

    public List<CalendarDay> ProjectedDates(int periods = 1)
    {
        return new();
    }
}
