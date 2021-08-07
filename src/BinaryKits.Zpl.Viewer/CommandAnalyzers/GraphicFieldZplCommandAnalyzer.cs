﻿using BinaryKits.Zpl.Label.Elements;

namespace BinaryKits.Zpl.Viewer.CommandAnalyzers
{
    public class GraphicFieldZplCommandAnalyzer : ZplCommandAnalyzerBase
    {
        public GraphicFieldZplCommandAnalyzer(VirtualPrinter virtualPrinter) : base("^GF", virtualPrinter)
        { }

        public override ZplElementBase Analyze(string zplCommand)
        {
            var x = 0;
            var y = 0;
            var bottomToTop = false;

            if (this.VirtualPrinter.NextElementPosition != null)
            {
                x = this.VirtualPrinter.NextElementPosition.X;
                y = this.VirtualPrinter.NextElementPosition.Y;

                bottomToTop = this.VirtualPrinter.NextElementPosition.CalculateFromBottom;
            }

            var zplCommandData = zplCommand.Substring(this.PrinterCommandPrefix.Length);

            var zplDataParts = zplCommandData.Split(',');


            var compressionType = zplDataParts[0][0];
            var binaryByteCount = 0;
            var graphicFieldCount = 0;
            var bytesPeRow = 7;
            var data = string.Empty;

            if (zplDataParts.Length > 1)
            {
                _ = int.TryParse(zplDataParts[1], out binaryByteCount);
            }
            if (zplDataParts.Length > 2)
            {
                _ = int.TryParse(zplDataParts[2], out graphicFieldCount);
            }
            if (zplDataParts.Length > 3)
            {
                _ = int.TryParse(zplDataParts[3], out bytesPeRow);
            }
            if (zplDataParts.Length > 4)
            {
                data = zplDataParts[4];
            }

            return new ZplGraphicField(x, y, binaryByteCount, graphicFieldCount, bytesPeRow, data, bottomToTop: bottomToTop, compressionType);
        }
    }
}
