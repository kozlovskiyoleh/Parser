using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    internal class Conductor
    {
        public string From { get; set; }
        public string To { get; set; }
        public string? Connector { get; set; }
        public string? CableCrossSection { get; set; }

        public Conductor(string from, string to, string? connector = null, string? cableCrossSection = null) 
        {
            this.From = from;
            this.To = to;
            this.Connector = GetConnector(connector);
            this.CableCrossSection = cableCrossSection;
        }

        public string? GetConnector(string connector)
        {
            if (connector == null) return null;
            if (connector.Contains('-'))
            {
                return connector[connector.Length-1].ToString();
            }
            return null;
        }
    }
}
