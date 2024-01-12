using BarcodeLib;
using BarcodeStandard;
using BinaryKits.Zpl.Label.Elements;
using BinaryKits.Zpl.Viewer.Helpers;
using SkiaSharp;
using System;
using System.Drawing;

namespace BinaryKits.Zpl.Viewer.ElementDrawers
{
    public class BarcodeEAN13ElementDrawer : BarcodeDrawerBase
    {
        public override bool CanDraw(ZplElementBase element)
        {
            return element is ZplBarcodeEan13;
        }

        ///<inheritdoc/>
        public override void Draw(ZplElementBase element)
        {
            Draw(element, new DrawerOptions());
        }

        ///<inheritdoc/>
        public override void Draw(ZplElementBase element, DrawerOptions options)
        {
            if (element is ZplBarcodeEan13 barcode)
            {
                float x = barcode.PositionX;
                float y = barcode.PositionY;

                var content = barcode.Content;
                content = content.PadLeft(12, '0').Substring(0, 12);
                var interpretation = content;

                if (barcode.PrintInterpretationLineAboveCode)
                {
                    int checksum = 0;
                    for (int i = 0; i < 12; i++)
                    {
                        checksum += (content[i] - 48) * (9 - i % 2 * 2);
                    }
                    interpretation = string.Format("{0}{1}", interpretation, checksum % 10);
                }

                float labelFontSize = Math.Min(barcode.ModuleWidth * 7.2f, 72f);
                var labelTypeFace = options.FontLoader("A");
                SKFont labelFont = new SKFont(labelTypeFace, labelFontSize);
                var labelSystemFont = labelFont.ToSystemDrawingFont();
                int labelHeight = barcode.PrintInterpretationLine ? labelSystemFont.Height : 0;
                int labelHeightOffset = barcode.PrintInterpretationLineAboveCode ? labelHeight : 0;

                var barcodeElement = new Barcode
                {
                    BarWidth = barcode.ModuleWidth,
                    BackColor = new SKColorF(Color.White.R, Color.White.G, Color.White.B, Color.White.A),
                    Height = barcode.Height + labelHeight,
                    IncludeLabel = barcode.PrintInterpretationLine,
                    LabelFont = labelFont,
                    AlternateLabel = interpretation,
                };

                using var image = barcodeElement.Encode(BarcodeStandard.Type.Ean13, content);
                this.DrawBarcode(barcodeElement.GetImageData(SaveTypes.Unspecified), barcode.Height, image.Width, barcode.FieldOrigin != null, x, y, labelHeightOffset, barcode.FieldOrientation);
            }
        }
    }
}
