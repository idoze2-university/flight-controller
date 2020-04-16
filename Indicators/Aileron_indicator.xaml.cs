using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Indicators
{
    public partial class Aileron_indicator : UserControl
    {
        ViewModel db = ViewModel.getInstance();
        public Aileron_indicator()
        {
            InitializeComponent();
            DataContext = db;
        }
    }
}
