using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Indicators
{
    public partial class Internal_pitch : UserControl
    {
        ViewModel db;
        public Internal_pitch()
        {
            InitializeComponent();
            db = ViewModel.getInstance();
            DataContext = db;
        }
    }
}
