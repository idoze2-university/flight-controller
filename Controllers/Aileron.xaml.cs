using System.Windows.Controls;
using FlightSimulatorApp.Components;
namespace FlightSimulatorApp.Controllers
{
    public partial class Aileron : UserControl
    {
        ViewModel vm = ViewModel.getInstance();
        public Aileron()
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}