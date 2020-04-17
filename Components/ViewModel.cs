using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;
namespace FlightSimulatorApp.Components
{
    public class ViewModel : INotifyPropertyChanged
    {
        private static ViewModel singleton = null;
        private Model model;
        private ViewModel() { model = new Model(); }
        public static ViewModel getInstance()
        {
            if (singleton != null)
            {
                return singleton;
            }
            return (singleton = new ViewModel());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propname)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }

        //-------------------Consts-------------------//
        public const double PAD_RADIUS = 85;
        public const double SLIDER_INIT_AILERON = 5;
        public const double SLIDER_INIT_THROTTLE = 0;
        public const double SLIDER_MAX = 10;
        public const double SLIDER_MIN = 0;
        public const double PIN_INIT_X = 21.3814681;
        public const double PIN_INIT_Y = -157.7118886;
        //-------------------------------------------//


        //-------------------Controllers-------------------//
        private double asv = SLIDER_INIT_AILERON, av = 0;
        private double tsv = SLIDER_INIT_THROTTLE, tv = 0;
        private double x = 0, y = 0, r = 0, e = 0;
        private Thickness _point_margin = new Thickness();

        public double aileron_slider_value
        {
            get { return asv; }
            set
            {
                if (value > SLIDER_MAX) { asv = SLIDER_MAX; }
                else if (value < SLIDER_MIN) { asv = SLIDER_MIN; }
                else { asv = value; }
                av = (asv - SLIDER_INIT_AILERON) / SLIDER_INIT_AILERON;
                NotifyPropertyChanged("aileron_value");
                NotifyPropertyChanged("aileron_slider_value");
            }
        }
        public double throttle_slider_value
        {
            get { return tsv; }
            set
            {
                if (value > SLIDER_MAX) { tsv = SLIDER_MAX; }
                else if (value < SLIDER_MIN) { tsv = SLIDER_MIN; }
                else { tsv = value; }
                tv = tsv / SLIDER_MAX;
                NotifyPropertyChanged("throttle_value");
                NotifyPropertyChanged("throttle_slider_value");
            }
        }

        public double aileron_value
        {
            get
            {
                return av;
            }
            set
            {
                if (value >= 1)
                {
                    asv = SLIDER_MAX;
                }
                else if (value <= -1)
                {
                    asv = SLIDER_MIN;
                }
                else
                {
                    asv = (value * SLIDER_INIT_AILERON + SLIDER_INIT_AILERON);
                }
                av = (asv - SLIDER_INIT_AILERON) / SLIDER_INIT_AILERON;
                NotifyPropertyChanged("aileron_value");
                NotifyPropertyChanged("aileron_slider_value");
            }
        }
        public double throttle_value
        {
            get
            {
                return tv;
            }
            set
            {
                if (value >= 1)
                {
                    tsv = SLIDER_MAX;
                }
                else if (value <= 0)
                {
                    tsv = SLIDER_MIN;
                }
                else
                {
                    tsv = value * 10;
                }
                tv = tsv / SLIDER_MAX;
                NotifyPropertyChanged("throttle_value");
                NotifyPropertyChanged("throttle_slider_value");
            }
        }

        public double X
        {
            get { return x; }
            set
            {
                if (value > PAD_RADIUS) { x = PAD_RADIUS; }
                else if (value < PAD_RADIUS) { x = -PAD_RADIUS; }
                else { x = value; }
                r = x / PAD_RADIUS;
                _point_margin.Left = x;
                NotifyPropertyChanged("point_margin");
                NotifyPropertyChanged("rudder");
                NotifyPropertyChanged("X");
            }
        }
        public double Y
        {
            get { return y; }
            set
            {
                if (value > PAD_RADIUS) { y = PAD_RADIUS; }
                else if (value < PAD_RADIUS) { y = -PAD_RADIUS; }
                else { y = value; }
                e = -y / PAD_RADIUS;
                _point_margin.Top = y;
                NotifyPropertyChanged("point_margin");
                NotifyPropertyChanged("elevator");
                NotifyPropertyChanged("Y");
            }
        }

        public double rudder
        {
            get { return r; }
            set
            {
                if (value > 1) { r = 1; }
                else if (value < -1) { r = -1; }
                else { r = value; }
                x = r * PAD_RADIUS;
                _point_margin.Left = x;
                NotifyPropertyChanged("point_margin");
                NotifyPropertyChanged("rudder");
                NotifyPropertyChanged("X");
            }
        }
        public double elevator
        {
            get { return e; }
            set
            {
                if (value > 1) { e = 1; }
                else if (value < -1) { e = -1; }
                else { e = value; }
                y = -e * PAD_RADIUS;
                _point_margin.Top = y;
                NotifyPropertyChanged("point_margin");
                NotifyPropertyChanged("elevator");
                NotifyPropertyChanged("Y");
            }
        }

        public Thickness point_margin
        {
            get { return _point_margin; }
            set
            {
                rudder = value.Left;
                elevator = value.Top;
            }
        }
        //------------------------------------------------//

        //-------------------Indicators-------------------//
        private double _airspeed = 0, _altimeter_altitude = 0, _altitude = 0;
        private double _ground_speed = 0, _heading_deg = 0, _internal_pitch = 0, _internal_roll = 0;
        private double _vertical_speed = 0;

        public double airspeed
        {
            get { return _airspeed; }
            set
            {
                _airspeed = value;
                NotifyPropertyChanged("airspeed");
            }
        }
        public double altimeter_altitude
        {
            get { return _altimeter_altitude; }
            set
            {
                _altimeter_altitude = value;
                NotifyPropertyChanged("altimeter_altitude");
            }
        }
        public double altitude
        {
            get { return _altitude; }
            set
            {
                _altitude = value;
                NotifyPropertyChanged("altitude");
            }
        }
        public double ground_speed
        {
            get { return _ground_speed; }
            set
            {
                _ground_speed = value;
                NotifyPropertyChanged("ground_speed");
            }
        }
        public double heading_deg
        {
            get { return _heading_deg; }
            set
            {
                _heading_deg = value;
                NotifyPropertyChanged("heading_deg");
            }
        }
        public double internal_pitch
        {
            get { return _internal_pitch; }
            set
            {
                _internal_pitch = value;
                NotifyPropertyChanged("internal_pitch");
            }
        }
        public double internal_roll
        {
            get { return _internal_roll; }
            set
            {
                _internal_roll = value;
                NotifyPropertyChanged("internal_roll");
            }
        }
        public double vertical_speed
        {
            get { return _vertical_speed; }
            set
            {
                _vertical_speed = value;
                NotifyPropertyChanged("vertical_speed");
            }
        }
        //------------------------------------------------//

        //-------------------Map-------------------//
        private double _pin_x = PIN_INIT_X, _pin_y = PIN_INIT_Y;
        private Location _pin_location = new Location(PIN_INIT_X, PIN_INIT_Y);
        public double pin_x
        {
            get { return _pin_x; }
            set
            {
                _pin_x = value;
                _pin_location.Latitude = _pin_x;
                NotifyPropertyChanged("pin_x");
                NotifyPropertyChanged("pin_location");
                NotifyPropertyChanged("pin_location_string");
            }
        }
        public string pin_location_string
        {
            get { return string.Format("{0},{1}", _pin_x, _pin_y); }
        }

        public double pin_y
        {
            get { return _pin_y; }
            set
            {
                _pin_y = value;
                _pin_location.Longitude = _pin_y;
                NotifyPropertyChanged("pin_y");
                NotifyPropertyChanged("pin_location");
                NotifyPropertyChanged("pin_location_string");
            }
        }
        public Location pin_location
        {
            get
            {
                return _pin_location;
            }
            set { }
        }
        //------------------------------------------------//

        public void Command(Enum command, object data)
        {
            switch (command)
            {
                case Commands.Commands.KNOB_MOVE_CMD:
                    Thickness t = model.Knob_Move(data);
                    point_margin = t;
                    break;
                case Commands.Commands.KNOB_RELEASE_CMD:
                    Thickness t1 = model.Knob_Release();
                    point_margin = t1;
                    break;
            }
        }
    }
}
