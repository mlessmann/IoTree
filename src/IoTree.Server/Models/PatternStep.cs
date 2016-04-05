using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTree.Server.Models
{
    public class PatternStep
    {
        public TimeSpan Duration { get; set; }
        
        public Dictionary<int, double> LedValues { get; private set; }

        public PatternStep()
        {
            Duration = TimeSpan.Zero;
            LedValues = new Dictionary<int, double>();
        }

        public bool IsUnchangedSuccessor(PatternStep predecessor)
        {
            return LedValues.All(kvp =>
            {
                double otherValue;
                if (!predecessor.LedValues.TryGetValue(kvp.Key, out otherValue))
                    return false;
                return otherValue == kvp.Value;
            });
        }
    }
}