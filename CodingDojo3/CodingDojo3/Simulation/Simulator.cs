﻿using CodingDojo3.ViewModel;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

namespace CodingDojo3.Simulation
{
    public class Simulator
    {
        private static Random rand = new Random(5);
        public List<ItemVm> Items { get; set; }

        /// <summary>
        /// Generates Demo Data (Sensors and Actuators and Starts manipulating the Values every 3 Secs.
        /// Light is only changed if Mode=auto!
        /// </summary>
        public Simulator(List<ItemVm> items)
        {
            this.Items = items;
            GenerateDemoData();

            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 3);
            t.Tick += T_Tick;
            t.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            UpdateDemoData();
        }

        private void GenerateDemoData()
        {
            Items.Add(new ItemVm(new Switch("0.01", "TA Wohnzimmer", "WZ", 1)));
            Items.Add(new ItemVm(new Switch("0.02", "TA Wohnzimmer", "WZ", 2)));
            Items.Add(new ItemVm(new Switch("0.03", "TA Badezimmer", "Bad", 3)));
            Items.Add(new ItemVm(new MotionDedector("1.100", "IR Haustüre", "Garderobe", 5)));
            Items.Add(new ItemVm(new MotionDedector("1.101", "IR Wohnzimmer", "WZ", 6)));
            Items.Add(new ItemVm(new Temperature("0.10", "Temp Badezimmer", "Bad", 7)));
            Items.Add(new ItemVm(new Temperature("0.11", "Temp Wohnimmer", "WZ", 8)));
            Items.Add(new ItemVm(new Temperature("0.12", "Temp Aussen Nord", "Garten", 9)));
            Items.Add(new ItemVm(new TwilightSwitch("0.20", "Dämmerungssensor Nord", "Garten", 10)));
            //Actors
            Items.Add(new ItemVm(new Light("2.00", "Licht Wohnzimmer", "WZ", 100)));
            Items.Add(new ItemVm(new Light("2.01", "Licht Aussen", "Garten", 101)));
            Items.Add(new ItemVm(new Light("2.02", "Licht Badezimmer", "Bad", 102)));
            Items.Add(new ItemVm(new PowerJack("2.03", "Dose Badezimmer", "Bad", 103)));
            Items.Add(new ItemVm(new PowerJack("2.04", "Dose Wohnzimmer", "WZ", 104)));
            Items.Add(new ItemVm(new PowerJack("2.05", "Dose Wohnzimmer", "WZ", 105)));
            UpdateDemoData(); // sets values
        }

        private void UpdateDemoData()
        {
            try
            {
                foreach (var item in Items)
                {
                    item.Value = GenerateValue(item.ValueType);
                }
            }
            catch (Exception e)
            {
                var m = e.Message;
            }

        }

        private object GenerateValue(Type t)
        {
            if (t == typeof(bool))
                return RandNo();

            if (t == typeof(byte))
                return rand.Next(0, 3);

            if (t == typeof(Single))
                return Math.Round((rand.Next(0, 110) - 50) / 1.2, 1);

            if (t == typeof(int))
                return (rand.Next(0, 1000));

            return null;
        }

        private int RandNo()
        {
            if (rand.Next(1, 100) > 50) return 1;
            else return 0;
        }
    }
}
