using System.Collections.ObjectModel;
using System.Collections.Specialized;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;

public partial class Calendar : ObservableValidator, INotifyCollectionChanged
{
    [ObservableProperty]
    private ObservableCollection<CalendarMonth> _calendarMonths = new();

    public Calendar() : this(DateOnly.FromDateTime(DateTime.Today)) { }

    public Calendar(DateOnly startingDate)
    {
        CalendarMonth startMonth = new(startingDate.Month, startingDate.Year);
        AddMonth(startMonth);
    }

    public Calendar(DateOnly startingDate, DateOnly endingDate) : this(startingDate) 
        => ExtendTo(endingDate);

    public Calendar(DateOnly startingDate, int length) : this(startingDate) 
        => ExtendByMonths(length);

    public CalendarMonth? GetMonth(int year, int monthOfYear) 
        => CalendarMonths.FirstOrDefault(cm => (cm.Year, cm.Month) == (year, monthOfYear));

    public decimal CurrentBalance => CalendarMonths.Last().EndingBalance;

    public CalendarDay FirstDay => CalendarMonths.First().FirstDay;

    public CalendarDay LastDay => CalendarMonths.Last().LastDay;

    public int TotalDays => (LastDay.CalendarDate.ToDateTime(TimeOnly.MaxValue) - FirstDay.CalendarDate.ToDateTime(TimeOnly.MinValue)).Days + 1;

    public decimal GetBalance(int year, int month, int day)
    {
        CalendarMonth? currentMonth = GetMonth(year, month);

        return currentMonth?.Days.Count > day - 1 
            ? currentMonth.Days[day - 1].EndingBalance 
            : 0M;
    }

    public void ExtendByOneMonth() 
        => ExtendByMonths(1);

    private void AddMonth(CalendarMonth newMonth)
    {
        CalendarMonths.Add(newMonth);

        newMonth.CollectionChanged += NewMonth_CollectionChanged;
        newMonth.PropertyChanged += NewMonth_PropertyChanged;
    }

    private void NewMonth_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        OnPropertyChanged(e.PropertyName);

        if (e.PropertyName is nameof(CalendarMonth.WorkingDate) && sender is CalendarMonth month)
        {
            if (WorkingMonth is not null)
            {
                WorkingMonth.IsWorkingMonth = false;
            }
            WorkingMonth = month;
            WorkingMonth.IsWorkingMonth = true;
        }
    }

    private void NewMonth_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CalendarMonths));
        OnPropertyChanged(nameof(BudgetItems));
    }

    public void ExtendByMonths(int monthsToExtendBy)
    {
        CalendarMonth currentMonth = CalendarMonths.Last();
        for (int i = 0; i < monthsToExtendBy; ++i)
        {
            CalendarMonth newMonth = new(
                currentMonth.Month switch
                {
                    12 => 1,
                    _ => currentMonth.Month + 1
                },
                currentMonth.Month switch
                {
                    12 => currentMonth.Year + 1,
                    _ => currentMonth.Year
                },
                currentMonth);
            AddMonth(newMonth);
            currentMonth = newMonth;
        }
    }

    public void ExtendTo(DateOnly endingDate)
    {
        CalendarMonth startMonth = CalendarMonths.First();
        TimeSpan diff = endingDate.ToDateTime(TimeOnly.MaxValue) - startMonth.FirstDay.CalendarDate.ToDateTime(TimeOnly.MinValue);
        int remainingDays = diff.Days;
        CalendarMonth currentMonth = startMonth;
        while (remainingDays > startMonth.DaysInMonth)
        {
            CalendarMonth newMonth = new(
                currentMonth.Month switch
                {
                    12 => 1,
                    _ => currentMonth.Month + 1
                },
                currentMonth.Month switch
                {
                    12 => currentMonth.Year + 1,
                    _ => currentMonth.Year
                },
                currentMonth);
            CalendarMonths.Add(newMonth);
            currentMonth = newMonth;
            remainingDays -= currentMonth.DaysInMonth;
        }
    }

    public ObservableCollection<BudgetItem> BudgetItems
        => new(CalendarMonths
            .SelectMany(cm => cm.BudgetItems));

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)CalendarMonths).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)CalendarMonths).CollectionChanged -= value;
    }

    public CalendarMonth? CurrentMonth => GetMonth(DateTime.Today.Year, DateTime.Today.Month);

    [ObservableProperty]
    private CalendarMonth? _workingMonth;
}