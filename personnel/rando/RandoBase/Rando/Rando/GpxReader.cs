using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rando
{
    internal class GpxReader
    {
        public static List<Trackpoint> LoadTrackpoints(string filePath)
        {
            XDocument gpxDoc = XDocument.Load(filePath);
            XNamespace ns = gpxDoc.Root.Name.Namespace;

            var trackpoints = (from trkpt in gpxDoc.Descendants(ns + "trkpt")
                               select new Trackpoint(
                                   (double)trkpt.Attribute("lat"),
                                   (double)trkpt.Attribute("lon"),
                                   (double?)trkpt.Element(ns + "ele") ?? 0
                               )).ToList();

            return trackpoints;
        }
    }
}
