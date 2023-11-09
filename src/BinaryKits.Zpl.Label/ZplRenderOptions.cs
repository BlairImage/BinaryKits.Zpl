namespace BinaryKits.Zpl.Label
{
    /// <summary>
    /// Holding rendering settings
    /// </summary>
    public class ZplRenderOptions
    {
        /// <summary>
        /// Include ^XA and ^XZ
        /// </summary>
        public bool AddStartEndFormat { get; set; }
        /// <summary>
        /// Include ^LH0,0
        /// </summary>
        public bool AddDefaultLabelHome { get; set; }

        /// <summary>
        /// Overrides default label darkness if set ~SD
        /// </summary>
        public int? Darkness { get; set; }

        /// <summary>
        /// Offset to move label content up and down ^LT
        /// </summary>
        public int? TopOffset { get; set; }

        /// <summary>
        /// Offset to move label content left and right ^LS
        /// </summary>
        public int? LeftOffset { get; set; }

        /// <summary>
        /// Sets the label width ^PW
        /// </summary>
        public int? LabelWidth { get; set; }

        /// <summary>
        /// The number of copies of a label that should be printed
        /// </summary>
        public int? QtyToPrint { get; set; }

        /// <summary>
        /// ^CI
        /// </summary>
        public string ChangeInternationalFontEncoding { get; set; }

        public bool DisplayComments { get; set; }

        public bool AddEmptyLineBeforeElementStart { get; set; }

        /// <summary>
        /// SourcePrint DPI
        /// </summary>
        /// <remarks>Default: 203</remarks>
        public int SourcePrintDpi { get; set; }

        /// <summary>
        /// TargetPrint DPI
        /// </summary>
        /// <remarks>Default: 203</remarks>
        public int TargetPrintDpi { get; set; }

        public bool CompressedRendering { get; set; }

        public double ScaleFactor { get { return (double)TargetPrintDpi / SourcePrintDpi; } }

        public int Scale(int input)
        {
            return (int)(input * ScaleFactor);
        }

        public double Scale(double input)
        {
            return input * ScaleFactor;
        }

        public ZplRenderOptions()
        {
            AddStartEndFormat = true;
            AddDefaultLabelHome = true;
            ChangeInternationalFontEncoding = ZplConstants.InternationalFontEncoding.CI28;
            SourcePrintDpi = TargetPrintDpi = 203;
            Darkness = 30;
        }
    }
}
