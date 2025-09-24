using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Rando
{
    public partial class Rando : Form
    {
        private List<Trackpoint> _trackpoints;

        public Rando()
        {
            InitializeComponent();

            // Ici tu charges ton fichier GPX dans _trackpoints
            _trackpoints = LoadTrackpoints("gemmikandersteg.gpx");
        }

        private void Rando_Form_Paint(object sender, PaintEventArgs e)
        {
            if (_trackpoints == null || _trackpoints.Count < 2)
                return;

            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            // Gradient de couleurs selon l'altitude
            Color[] gradient = new Color[]
            {
                Color.FromArgb(255, 144, 238, 144),
                Color.FromArgb(255, 162, 216, 128),
                Color.FromArgb(255, 180, 194, 112),
                Color.FromArgb(255, 198, 172, 96),
                Color.FromArgb(255, 216, 150, 80),
                Color.FromArgb(255, 234, 128, 64),
                Color.FromArgb(255, 244, 106, 48),
                Color.FromArgb(255, 248,  84, 36),
                Color.FromArgb(255, 252,  62, 24),
                Color.FromArgb(255, 254,  48, 18),
                Color.FromArgb(255, 255,  32, 12),
                Color.FromArgb(255, 255,  16,  6),
                Color.FromArgb(255, 255,   0,  0)
            };

            // Trouver les bornes GPS
            double latMin = double.MaxValue, latMax = double.MinValue;
            double lonMin = double.MaxValue, lonMax = double.MinValue;

            for (int i = 0; i < _trackpoints.Count; i++)
            {
                Trackpoint t = _trackpoints[i];
                if (t.Latitude < latMin) latMin = t.Latitude;
                if (t.Latitude > latMax) latMax = t.Latitude;
                if (t.Longitude < lonMin) lonMin = t.Longitude;
                if (t.Longitude > lonMax) lonMax = t.Longitude;
            }

            // Dessiner ligne par ligne avec couleur selon altitude
            for (int i = 1; i < _trackpoints.Count; i++)
            {
                Trackpoint prev = _trackpoints[i - 1];
                Trackpoint curr = _trackpoints[i];

                int x1 = (int)((prev.Longitude - lonMin) / (lonMax - lonMin) * width);
                int y1 = (int)(height - (prev.Latitude - latMin) / (latMax - latMin) * height);

                int x2 = (int)((curr.Longitude - lonMin) / (lonMax - lonMin) * width);
                int y2 = (int)(height - (curr.Latitude - latMin) / (latMax - latMin) * height);

                int index = (int)(curr.Elevation / 100);
                if (index >= gradient.Length) index = gradient.Length - 1;

                using (Pen pen = new Pen(gradient[index], 2))
                {
                    e.Graphics.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }

        // Méthode simplifiée pour charger les Trackpoints depuis un fichier GPX
        private List<Trackpoint> LoadTrackpoints(string filePath)
        {
            List<Trackpoint> list = new List<Trackpoint>();

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(filePath);

            System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("gpx", "http://www.topografix.com/GPX/1/1");

            var nodes = doc.SelectNodes("//gpx:trkpt", nsmgr);
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                double lat = double.Parse(node.Attributes["lat"].Value, System.Globalization.CultureInfo.InvariantCulture);
                double lon = double.Parse(node.Attributes["lon"].Value, System.Globalization.CultureInfo.InvariantCulture);
                double ele = double.Parse(node["ele"].InnerText, System.Globalization.CultureInfo.InvariantCulture);

                list.Add(new Trackpoint { Latitude = lat, Longitude = lon, Elevation = ele });
            }

            return list;
        }
    }
}
