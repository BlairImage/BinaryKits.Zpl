using BarcodeLib;
using BarcodeStandard;
using BinaryKits.Zpl.Label.Elements;
using BinaryKits.Zpl.Viewer.Helpers;
using SkiaSharp;
using System;
using System.Drawing;

namespace BinaryKits.Zpl.Viewer.ElementDrawers
{
    public class Barcode39ElementDrawer : BarcodeDrawerBase
    {
        ///<inheritdoc/>
        public override bool CanDraw(ZplElementBase element)
        {
            return element is ZplBarcode39;
        }

        ///<inheritdoc/>
        public override void Draw(ZplElementBase element)
        {
            Draw(element, new DrawerOptions());
        }

        ///<inheritdoc/>
        public override void Draw(ZplElementBase element, DrawerOptions options)
        {
            if (element is ZplBarcode39 barcode)
            {
                float x = barcode.PositionX;
                float y = barcode.PositionY;

                var content = barcode.Content;
                var interpretation = string.Format("*{0}*", content.Trim('*'));

                float labelFontSize = Math.Min(barcode.ModuleWidth * 7.2f, 72f);
                var labelTypeFace = options.FontLoader("A");
                SKFont labelFont = new SKFont(labelTypeFace, labelFontSize);
                var labelSystemFont = labelFont.ToSystemDrawingFont();
                int labelHeight = barcode.PrintInterpretationLine ? labelSystemFont.Height : 0;
                int labelHeightOffset = barcode.PrintInterpretationLineAboveCode ? labelHeight : 0;

                var barcodeElement = new Barcode
                {
                    BarWidth = barcode.ModuleWidth,
                    BackColor = new SKColorF(Color.Transparent.R, Color.Transparent.G, Color.Transparent.B, Color.Transparent.A),
                    Height = barcode.Height + labelHeight,
                    IncludeLabel = barcode.PrintInterpretationLine,
                    LabelFont = labelFont,
                    AlternateLabel = interpretation
                };

                using var image = barcodeElement.Encode(BarcodeStandard.Type.Code39Extended, content);
                this.DrawBarcode(barcodeElement.GetImageData(SaveTypes.Unspecified), barcode.Height, image.Width, barcode.FieldOrigin != null, x, y, labelHeightOffset, barcode.FieldOrientation);
            }
        }
    }
}
