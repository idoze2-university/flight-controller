using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Indicators
{
    public partial class Internal_roll : UserControl
    {
        ViewModel db;
        public Internal_roll()
        {
            InitializeComponent();
            db = ViewModel.getInstance();
            DataContext = db;
        }
    }
}
