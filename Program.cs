using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        DateTime date;
        IMyTimerBlock timer_up;
        IMyTextPanel panel;
        IMyMotorAdvancedStator rotor;
        IMyCargoContainer cargo;

        Double currentFilling;
        Double maxFilling;



        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }

        public void Main(string argument, UpdateType updateSource)
        {
            timer_up = GridTerminalSystem.GetBlockWithName("Timer up") as IMyTimerBlock;
            panel = GridTerminalSystem.GetBlockWithName("lcd info") as IMyTextPanel;
            rotor = GridTerminalSystem.GetBlockWithName("ultimate rotor drill") as IMyMotorAdvancedStator;
            cargo = GridTerminalSystem.GetBlockWithName("Grandconteneur") as IMyCargoContainer;
            var inventory = cargo.GetInventory(0);
            currentFilling += Convert.ToDouble(inventory.CurrentVolume.RawValue);
            maxFilling += Convert.ToDouble(inventory.MaxVolume.RawValue);
            date = DateTime.Now;

            if (panel != null && timer_up != null)
            {
                panel.WriteText("Temps avant déplacement avant: \n" +
                    timer_up.DetailedInfo.Substring(16) +
                    "\n\n" +
                    "Vitesse de rotation: \n" +
                    rotor.TargetVelocityRPM.ToString() + " tr/min \n\n" +
                    "Taux remplissage conteneur: \n" +
                    Math.Round(100 * (currentFilling / maxFilling), 2).ToString() + "%"+
                    "\n\n\n\n\n" +
                    date.ToString());
            }
        }
    }
}
