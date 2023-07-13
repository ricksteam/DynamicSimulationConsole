using Shared.Models;

namespace Engines;

public abstract class ScoringEngine
{
    // const double LENGTH_WEIGHT = 0.5;
    // const double BRIDGE_CONDITION_WEIGHT = 1.0;
    //
    // public void CalculateScore(IEnumerable<OsmNode> route, object convoy)
    // {
    //     var totalLength = 0.0;
    //     var totalBridgeCondition = 0.0;
    //
    //     foreach (OsmNode segment in route)
    //     {
    //         totalLength += segment.Length;
    //         
    //         if (segment.Bridge != null)
    //         {
    //             if (convoy.Weight > segment.Bridge.Capacity) 
    //                 return double.MaxValue;
    //
    //             totalBridgeCondition += segment.Bridge.ConditionScore;
    //         }
    //     }
    //
    //     return LENGTH_WEIGHT * totalLength + BRIDGE_CONDITION_WEIGHT * totalBridgeCondition;
    // }
    //
    // private double CalculateStrategicImportance(IEnumerable<OsmNode> route)
    // {
    //     
    // }
    // private double CalculateAlternateOptions(IEnumerable<OsmNode> route)
    // {
    //     
    // }
    // private double CalculateConvoySuitability(IEnumerable<OsmNode> route, object convoy)
    // {
    //     
    // }
}