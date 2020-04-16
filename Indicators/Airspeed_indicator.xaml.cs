using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Indicators
{
    public partial class Airspeed_indicator : UserControl
    {
        ViewModel db = ViewModel.getInstance();
        public Airspeed_indicator()
        {
            InitializeComponent();
            DataContext = db;
        }
    }
}
