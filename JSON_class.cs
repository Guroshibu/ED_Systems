using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Systems
{
    //"event":"FSDJump"
    public class FSDJump
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string StarSystem { get; set; } = "";
        public UInt64 SystemAddress { get; set; } = 0;
        public List<double> StarPos { get; set; }
        public string SystemAllegiance { get; set; } = "";
        public string SystemEconomy { get; set; } = "";
        public string SystemEconomy_Localised { get; set; } = "";
        public string SystemSecondEconomy { get; set; } = "";
        public string SystemSecondEconomy_Localised { get; set; } = "";
        public string SystemGovernment { get; set; } = "";
        public string SystemGovernment_Localised { get; set; } = "";
        public string SystemSecurity { get; set; } = "";
        public string SystemSecurity_Localised { get; set; } = "";
        public long Population { get; set; } = 0;
        public string Body { get; set; } = "";
        public UInt64 BodyID { get; set; } = 0;
        public string BodyType { get; set; } = "";
        public double JumpDist { get; set; } = 0;
        public double FuelUsed { get; set; } = 0;
        public double FuelLevel { get; set; } = 0;

    }
    //"event":"Scan"
    public class Parent
    {
        public int Planet { get; set; } = 0;
        public int Star { get; set; } = 0;
        public int Null { get; set; } = 0;
    }

    public class Material
    {
        public string Name { get; set; } = "";
        public string Name_Localised { get; set; } = "";
        public double Percent { get; set; } = 0;
    }

    public class Composition
    {
        public double Ice { get; set; } = 0;
        public double Rock { get; set; } = 0;
        public double Metal { get; set; } = 0;
    }

    public class AtmosphereComposition
    {
        public string Name { get; set; } = "";
        public double Percent { get; set; } = 0;
    }

    public class Ring
    {
        public string Name { get; set; } = "";
        public string RingClass { get; set; } = "";
        public double MassMT { get; set; } = 0;
        public double InnerRad { get; set; } = 0;
        public double OuterRad { get; set; } = 0;
    }

    public class Scan
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string ScanType { get; set; } = "";
        public string BodyName { get; set; } = "";
        public UInt64 BodyID { get; set; } = 0;
        public List<Parent> Parents { get; set; }
        public string StarSystem { get; set; } = "";
        public UInt64 SystemAddress { get; set; } = 0;
        public double DistanceFromArrivalLS { get; set; } = 0;
        public bool TidalLock { get; set; } = false;
        public string TerraformState { get; set; } = "";
        public string PlanetClass { get; set; } = "";
        public string Atmosphere { get; set; } = "";
        public List<AtmosphereComposition> AtmosphereComposition { get; set; }
        public string Volcanism { get; set; } = "";
        public double MassEM { get; set; } = 0;
        public double Radius { get; set; } = 0;
        public double SurfaceGravity { get; set; } = 0;
        public double SurfaceTemperature { get; set; } = 0;
        public double SurfacePressure { get; set; } = 0;
        public bool Landable { get; set; } = false;
        public List<Material> Materials { get; set; }
        public Composition Composition { get; set; }
        public double SemiMajorAxis { get; set; } = 0;
        public double Eccentricity { get; set; } = 0;
        public double OrbitalInclination { get; set; } = 0;
        public double Periapsis { get; set; } = 0;
        public double OrbitalPeriod { get; set; } = 0;
        public double RotationPeriod { get; set; } = 0;
        public double AxialTilt { get; set; } = 0;
        public List<Ring> Rings { get; set; }
        public string ReserveLevel { get; set; } = "";
        public bool WasDiscovered { get; set; } = false;
        public bool WasMapped { get; set; } = false;


    }
    //"event":"SAASignalsFound"
    public class Signal
    {
        public string Type { get; set; } = "";
        public string Type_Localised { get; set; } = "";
        public int Count { get; set; } = 0;

    }

    public class SAASignalsFound
    {
        public DateTime timestamp { get; set; } = new DateTime();
        public string @event { get; set; } = "";
        public string BodyName { get; set; } = "";
        public UInt64 SystemAddress { get; set; } = 0;
        public UInt64 BodyID { get; set; } = 0;
        public List<Signal> Signals { get; set; }

    }
    //EDSM info
     public class EDSMRing
    {
        public string name { get; set; } = "";
        public string type { get; set; } = "";
        public double? mass { get; set; } = 0;
        public double? innerRadius { get; set; } = 0;
        public double? outerRadius { get; set; } = 0;

    }

    public class EDSMBody
    {
        public UInt64 id { get; set; } = 0;
        public string name { get; set; } = "";
        public string type { get; set; } = "";
        public string subType { get; set; } = "";
        public double? distanceToArrival { get; set; } = 0;
        public bool? isMainStar { get; set; } = false;
        public bool? isScoopable { get; set; } = false;
        public int? age { get; set; } = 0;
        public string luminosity { get; set; } = "";
        public double? absoluteMagnitude { get; set; } = 0;
        public double? solarMasses { get; set; } = 0;
        public double? solarRadius { get; set; } = 0;
        public double? surfaceTemperature { get; set; } = 0;
        public double? orbitalPeriod { get; set; } = 0;
        public double? semiMajorAxis { get; set; } = 0;
        public double? orbitalEccentricity { get; set; } = 0;
        public double? orbitalInclination { get; set; } = 0;
        public double? argOfPeriapsis { get; set; } = 0;
        public double? rotationalPeriod { get; set; } = 0;
        public bool? rotationalPeriodTidallyLocked { get; set; } = false;
        public double? axialTilt { get; set; } = 0;
        public bool? isLandable { get; set; } = false;
        public double? gravity { get; set; } = 0;
        public double? earthMasses { get; set; } = 0;
        public double? radius { get; set; } = 0;
        public string volcanismType { get; set; } = "";
        public string atmosphereType { get; set; } = "";
        public string terraformingState { get; set; } = "";
        public List<EDSMRing> rings { get; set; }

    }

    public class EDSMInfo
    {
        public UInt64 id { get; set; } = 0;
        public string name { get; set; } = "";
        public List<EDSMBody> bodies { get; set; }

    }


}
