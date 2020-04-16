using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Indicators
{
    public partial class Elevator_indicator : UserControl
    {
        ViewModel db = ViewModel.getInstance();
        public Elevator_indicator()
        {
            InitializeComponent();
            DataContext = db;
        }
    }
}
