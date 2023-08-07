using System.Collections.ObjectModel;
using System.Collections.Specialized;

using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;

public partial class CalendarMonth : ObservableValidator, INotifyCollectionChanged
{
    [ObservableProperty]
    private ObservableCollection<CalendarDay> _days = new()
    {
    };

    [ObservableProperty]
    private CalendarMonth? _previousMonth = null;

    public int DaysInMonth => DateTime.DaysInMonth(Year, Month);

    public CalendarDay LastDay => Days.OrderByDescending(d => d.CalendarDate).First();
    public CalendarDay FirstDay => Days.OrderBy(d => d.CalendarDate).First();

    public decimal BeginningBalance => PreviousMonth?.LastDay.EndingBalance ?? 0M;
    public decimal EndingBalance => LastDay.EndingBalance;

    public decimal MonthlyBudget => EndingBalance - BeginningBalance;

    public int Year
    {
        get;
    }
    public int Month
    {
        get;
    }

    public CalendarMonth(int monthOfYear, int year, CalendarMonth? previousMonth = null)
    {
        Year = year;
        Month = monthOfYear;
        PreviousMonth = previousMonth;

        Enumerable.Range(1, DaysInMonth).ToList().ForEach(dayOfMonth
            => Days.Add(new(this, dayOfMonth)
                {
                    PreviousDay = dayOfMonth switch
                    {
                        1 => previousMonth?.LastDay,
                        _ => LastDay
                    }
                }));

        Days.ToList().ForEach(day => day.CollectionChanged += Day_CollectionChanged);
        Days.ToList().ForEach(day => day.PropertyChanged += Day_PropertyChanged);

        PropertyChanged += CalendarMonth_PropertyChanged;
    }

    private void CalendarMonth_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    private void Day_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Days));
        OnPropertyChanged(nameof(BudgetItems));
        OnPropertyChanged(nameof(WorkingBudgetItems));
    }

    private void Day_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Days));
        OnPropertyChanged(nameof(BudgetItems));
        OnPropertyChanged(nameof(WorkingBudgetItems));
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)Days).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)Days).CollectionChanged -= value;
    }

    public ObservableCollection<BudgetItem> BudgetItems 
        => new(Days
            .SelectMany(d => d.BudgetItems)
            .OrderBy(t => t.DateDue.CalendarDate));

    public string Label => $"{FirstDay.CalendarDate:yyyy-MMMM} : Monthly Budget {MonthlyBudget:C0}";

    [ObservableProperty, NotifyPropertyChangedFor(nameof(WorkingBudgetItems))]
    private DateTime? _workingDate;

    [ObservableProperty]
    private bool _isWorkingMonth;

    public bool IsCurrentMonth => Year == DateTime.Today.Year && Month == DateTime.Today.Month;

    public ObservableCollection<BudgetItem> WorkingBudgetItems
        => new(Days.FirstOrDefault(d 
            => d.CalendarDate.Year == WorkingDate?.Year &&
                d.CalendarDate.Month == WorkingDate?.Month &&
                d.CalendarDate.Day == WorkingDate?.Day)?
                .BudgetItems.ToArray() ?? Array.Empty<BudgetItem>());

    public override string ToString() => Label;
}
