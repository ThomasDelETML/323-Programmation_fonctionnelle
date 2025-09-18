using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Rando
{
    public partial class Rando : Form
    {
        private List<Trackpoint> _trackpoints;

        public Rando()
        {
            InitializeComponent();

            // Load the GPX file (make sure the .gpx file is copied into bin/Debug or bin/Release)
            _trackpoints = GpxReader.LoadTrackpoints("gemmikandersteg.gpx");
        }

        private void Rando_Form_Paint(object sender, PaintEventArgs e)
        {
            if (_trackpoints == null || _trackpoints.Count < 2)
                return;

            using (Pen myPen = new Pen(Color.Red, 2))
            {
                // crude coordinate conversion
                var points = _trackpoints.Select(tp =>
                    new Point(
                        (int)(tp.Longitude * 1000) % this.ClientSize.Width,
                        (int)(tp.Latitude * -1000) % this.ClientSize.Height
                    )).ToArray();

                e.Graphics.DrawLines(myPen, points);
            }
        }
    }
}
