using System.Collections.ObjectModel;
using System.Collections.Specialized;

using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;

public partial class Categories : ObservableObject, INotifyCollectionChanged
{
    private ObservableCollection<Category> _rootCategories = new()
    {
        new Category(null, "Paycheck", "Take home wages", true),
        new Category(null, "Other Income", "Non-wage income", true),
        new Category(null, "Household Bills", "Contract Home Payments (Rent, Mortgage, Etc.)"),
        new Category(null, "Household Expenses", "Recurring or One-Time Expenses (Furniture, Supplies, Pest Control, Maintenance, etc.)"),
        new Category(null, "Food & Grocery", "Expected Food Expenses"),
        new Category(null, "Utility Bills", "Required Expenses (Electric, Water, etc.)"),
        new Category(null, "Utility Subscriptions", "Recurring Optional Expenses (Entertainment, etc.)"),
        new Category(null, "Hobby Subscriptions", "Recurring Hobby-related Expenses (Crunch Labs, etc.)"),
        new Category(null, "Hobby Expenses", "One-Time Hobby-related Expenses (Crunch Labs, etc.)"),
        new Category(null, "Professional Subscriptions", "Recurring Profession-related Expenses (Adobe, etc.)"),
        new Category(null, "Professional Expenses", "One-Time Work-related Expenses (Travel Lunches, etc.)"),
        new Category(null, "Auto Bills", "Contract or Recurring Auto Payments (Loans, Insurance)"),
        new Category(null, "Auto Expenses", "Recurring or One-Time Expenses (Fuel, Maintenance, etc.)"),
        new Category(null, "Debt Bills", "Contract or Recurring Debt Payments (Loans)"),
        new Category(null, "Debt Expenses", "Recurring or One-Time Debt Expenses (Payoffs, etc.)"),
        new Category(null, "Legal Bills", "Contract or Recurring Legal Payments (Retainers, etc.)"),
        new Category(null, "Legal Expenses", "One-Time Legal Expenses (Settlements, etc.)"),
        new Category(null, "Taxes", "Recurring or One-Time Taxes (Local, State, Federal, etc.)"),
    };

    public ObservableCollection<Category> RootCategories => _rootCategories;

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)_rootCategories).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)_rootCategories).CollectionChanged -= value;
    }
}