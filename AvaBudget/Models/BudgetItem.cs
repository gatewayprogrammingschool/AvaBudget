using System.ComponentModel.DataAnnotations;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;
public partial class BudgetItem : ObservableValidator
{
    [ObservableProperty, Required]
    private string _name;

    [ObservableProperty, Required]
    private IPayee _payTo;

    [ObservableProperty]
    private IPayor? _paidBy;

    [ObservableProperty, NotifyPropertyChangedFor(nameof(DisplayAmount))]
    private decimal _amount = 0m;

    public string DisplayAmount => _category.IsDeposit switch
    {
        true => $"{Amount:C0}",
        _ => $"{-Amount:C0}",
    };

    [ObservableProperty, NotifyPropertyChangedFor(nameof(DisplayAmount))]
    private Category? _category;

    [ObservableProperty]
    private CalendarDay _dateDue;

    public BudgetItem(CalendarDay dateDue, string name, IPayee payTo, IPayor? paidBy, decimal amount, Category? category)
    {
        DateDue = dateDue ?? throw new ArgumentNullException(nameof(dateDue));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        PayTo = payTo ?? throw new ArgumentNullException(nameof(payTo));
        PaidBy = paidBy;
        Category = category;
        Amount = amount;
    }
}
