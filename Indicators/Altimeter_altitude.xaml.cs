using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Indicators
{
    public partial class Altimeter_altitude : UserControl
    {
        ViewModel db = ViewModel.getInstance();
        public Altimeter_altitude()
        {
            InitializeComponent();
            DataContext = db;
        }
    }
}
