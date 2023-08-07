
using AvaBudget.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty, NotifyPropertyChangedFor(nameof(Count))]
    private Calendar _calendar = new();

    [ObservableProperty]
    private Categories _categories = new();

    public int Count => Calendar.BudgetItems.Count();


    public MainViewModel()
    {
        _calendar.ExtendByOneMonth();
        var august2023 = _calendar.GetMonth(2023, 8);
        august2023?.Days[2].BudgetItems.Add(
            new BudgetItem(
                august2023!.Days[2],
                "Payton Paycheck",
                new Payee<DayOfWeekScheduleTempo>("Payton", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                new Payor<DayOfWeekScheduleTempo>("Kforce", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                40M*80M*0.7M,
                _categories.RootCategories.First()
                ));

        august2023?.Days[9].BudgetItems.Add(
            new BudgetItem(
                august2023!.Days[9],
                "Payton Paycheck",
                new Payee<DayOfWeekScheduleTempo>("Payton", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                new Payor<DayOfWeekScheduleTempo>("Kforce", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                40M * 80M * 0.7M,
                _categories.RootCategories.First()
                ));

        august2023?.Days[16].BudgetItems.Add(
            new BudgetItem(
                august2023!.Days[16],
                "Payton Paycheck",
                new Payee<DayOfWeekScheduleTempo>("Payton", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                new Payor<DayOfWeekScheduleTempo>("Kforce", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                40M * 80M * 0.7M,
                _categories.RootCategories.First()
                ));

        august2023?.Days[23].BudgetItems.Add(
            new BudgetItem(
                august2023!.Days[23],
                "Payton Paycheck",
                new Payee<DayOfWeekScheduleTempo>("Payton", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                new Payor<DayOfWeekScheduleTempo>("Kforce", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                40M * 80M * 0.7M,
                _categories.RootCategories.First()
                ));

        august2023?.Days[30].BudgetItems.Add(
            new BudgetItem(
                august2023!.Days[30],
                "Payton Paycheck",
                new Payee<DayOfWeekScheduleTempo>("Payton", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                new Payor<DayOfWeekScheduleTempo>("Kforce", new DayOfWeekSchedule(DayOfWeekScheduleTempo.Weekly, new DateOnly(2023, 8, 3))),
                40M * 80M * 0.7M,
                _categories.RootCategories.First()
                ));

        august2023?.Days[2].BudgetItems.Add(
            new BudgetItem(
                august2023!.Days[2],
                "Becky Paycheck",
                new Payee<DayOfMonthScheduleTempo>("Becky", new DayOfMonthSchedule(DayOfMonthScheduleTempo.Monthly, new DateOnly(2023, 8, 3))),
                new Payor<DayOfMonthScheduleTempo>("CMCSS", new DayOfMonthSchedule(DayOfMonthScheduleTempo.Monthly, new DateOnly(2023, 8, 3))),
                4300M,
                _categories.RootCategories.First()
                ));

        august2023?.Days[4].BudgetItems.Add(
            new BudgetItem(
                august2023!.Days[4],
                "Rent",
                new Payee<DayOfMonthScheduleTempo>("Bell Crossing", new DayOfMonthSchedule(DayOfMonthScheduleTempo.Monthly, new DateOnly(2023, 8, 5))),
                new Payor<DayOfMonthScheduleTempo>("Becky", new DayOfMonthSchedule(DayOfMonthScheduleTempo.Monthly, new DateOnly(2023, 8, 3))),
                560 + 250,
                _categories.RootCategories[2]
                ));
    }
}
