using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Indicators
{
    public partial class Altitude : UserControl
    {
        ViewModel db = ViewModel.getInstance();
        public Altitude()
        {
            InitializeComponent();
            DataContext = db;
        }
    }
}
