using System.Windows.Controls;
using FlightSimulatorApp.Components;
using System.Threading;
namespace FlightSimulatorApp.Map
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class Map : UserControl
    {
        ViewModel db;
        public Map()
        {
            InitializeComponent();
            db = ViewModel.getInstance();
            DataContext = db;
        }
    }
}
