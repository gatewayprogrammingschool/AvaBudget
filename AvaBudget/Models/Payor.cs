using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaBudget.Models;

public partial class Payor<TScheduleTempo> : ObservableValidator, IPayor
    where TScheduleTempo : Enum
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private ISchedule<TScheduleTempo> _schedule;

    public Payor(string name, ISchedule<TScheduleTempo> schedule) => (_name, _schedule) = (name, schedule);

    public override string ToString() => Name;
}
