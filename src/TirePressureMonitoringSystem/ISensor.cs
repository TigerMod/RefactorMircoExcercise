using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public interface ISensor
    {
        /// <summary>
        /// Returns pressure of the tire
        /// </summary>
        double PopNextPressurePsiValue();
    }
}
