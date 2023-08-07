using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;

public partial class CalendarDay : ObservableValidator, INotifyCollectionChanged
{
    [ObservableProperty, Required]
    private DateOnly _calendarDate;

    [ObservableProperty]
    private CalendarDay? _previousDay;

    [ObservableProperty, Required]
    private CalendarMonth _currentMonth;

    public CalendarDay(CalendarMonth currentMonth, int dayOfMonth, CalendarDay? previousDay = null)
    {
        CurrentMonth = currentMonth;
        CalendarDate = new(CurrentMonth.Year, CurrentMonth.Month, dayOfMonth);
        PreviousDay = previousDay;

        BudgetItems.CollectionChanged += BudgetItems_CollectionChanged;
    }

    private void BudgetItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(DailyBudget));
        OnPropertyChanged(nameof(EndingBalance));
        OnPropertyChanged(nameof(Label));
    }

    public bool IsBeginningOfMonth => CalendarDate.Day is 1;

    public ObservableCollection<BudgetItem> BudgetItems
    {
        get; set;
    } = new();

    public decimal EndingBalance => (PreviousDay?.EndingBalance ?? 0.0M) + DailyBudget;
        

    public decimal DailyBudget => BudgetItems
            .Where(bi => bi.Category?.IsDeposit ?? false)
            .Sum(bi => bi.Amount) -
        BudgetItems
            .Where(bi => !(bi.Category?.IsDeposit ?? false))
            .Sum(bi => bi.Amount);

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)BudgetItems).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)BudgetItems).CollectionChanged -= value;
    }

    public string Label => $"{CalendarDate:yyyy-MMMM-dd} : Daily Budget {DailyBudget:C0}";

    public override string ToString() => Label;
}
