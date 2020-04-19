using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using FlightSimulatorApp.Components;
using System.Configuration;

namespace FlightSimulatorApp.Connector
{

    public class Client
    {
        public static Socket clientSocket;
        public static void Start()
        {
            Thread t = new Thread(() =>
            {
                while (true)
                    RunClient(int.Parse(ConfigurationManager.AppSettings["port"]));
            })
            {
                IsBackground = true
            };
            t.Start();
        }
        public static void RunClient(int port)
        {
            // Data buffer for incoming data.

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                IPAddress ipAddress = IPAddress.Parse(ConfigurationManager.AppSettings["ip"]);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP  socket.
                clientSocket = new Socket(ipAddress.AddressFamily,
                     SocketType.Stream, ProtocolType.Tcp);

                bool connected = false;
                while (!connected)
                {
                    try
                    {
                        // Connect the socket to the remote endpoint. Catch any errors.
                        clientSocket.Connect(remoteEP);
                        Console.WriteLine("Socket connected to {0}", clientSocket.RemoteEndPoint.ToString());
                        connected = true;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                while (clientSocket.Connected)
                    SyncValues();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void SyncValues()
        {
            try
            {
                ViewModel Data = ViewModel.getInstance();
                try
                {
                    SetValue("/controls/engines/current-engine/throttle", Data.throttle_value);
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't set /controls/engines/current-engine/throttle");
                }
                try
                {
                    SetValue("/controls/flight/aileron", Data.aileron_value);
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't set /controls/flight/aileron");
                }
                try
                {
                    SetValue("/controls/flight/elevator", Data.elevator);
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't set /controls/flight/elevator");
                }
                try
                {
                    SetValue("/controls/flight/rudder", Data.rudder);
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't set /controls/flight/rudde");
                }
                try
                {
                    Data.heading_deg = GetValue("/instrumentation/heading-indicator/indicated-heading-deg");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /instrumentation/heading-indicator/indicated-heading-deg");
                }
                try
                {
                    Data.vertical_speed = GetValue("/instrumentation/gps/indicated-vertical-speed");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /instrumentation/gps/indicated-vertical-speed");
                }
                try
                {
                    Data.ground_speed = GetValue("/instrumentation/gps/indicated-ground-speed-kt");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /instrumentation/gps/indicated-ground-speed-kt");
                }
                try
                {
                    Data.altimeter_altitude = GetValue("/instrumentation/altimeter/indicated-altitude-ft");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /instrumentation/altimeter/indicated-altitude-ft");
                }
                try
                {
                    Data.internal_roll = GetValue("/instrumentation/attitude-indicator/internal-roll-deg");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /instrumentation/attitude-indicator/internal-roll-deg");
                }
                try
                {
                    Data.internal_pitch = GetValue("/instrumentation/attitude-indicator/internal-pitch-deg");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /instrumentation/attitude-indicator/internal-pitch-deg");
                }
                try
                {
                    Data.altitude = GetValue("/instrumentation/gps/indicated-altitude-ft");

                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /instrumentation/gps/indicated-altitude-ft");
                }
                try
                {
                    Data.pin_x = GetValue("/position/latitude-deg");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /position/latitude-deg");
                }
                try
                {
                    Data.pin_y = GetValue("/position/longitude-deg");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /position/latitude-deg");
                }
                try
                {
                    Data.airspeed = GetValue("/instrumentation/airspeed-indicator/indicated-speed-kt");
                }
                catch (Exception)
                {
                    Console.WriteLine("Couldn't get /instrumentation/airspeed-indicator/indicated-speed-kt");
                }

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

            Thread.Sleep(100);

        }

        public static double GetValue(string key)
        {
            byte[] bytes = new byte[1024];

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(string.Format("get {0}\n", key));

            // Send the data through the socket.
            clientSocket.Send(msg);

            // Receive the response from the remote device.
            int bytesRec = clientSocket.Receive(bytes);
            string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            return double.Parse(data);
        }

        public static void SetValue(string key, double value)
        {
            byte[] bytes = new byte[1024];

            // Encode the data string into a byte array.
            byte[] msg = Encoding.ASCII.GetBytes(string.Format("set {0} {1}\n", key, value));

            // Send the data through the socket.
            clientSocket.Send(msg);

            // Receive the response from the remote device.
            int bytesRec = clientSocket.Receive(bytes);
            string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        }
    }
}