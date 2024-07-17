using SkiaSharp;
using System;
using System.Diagnostics;

namespace BinaryKits.Zpl.Viewer.ElementDrawers
{
    public class DrawerOptions
    {
        public Func<string, SKTypeface> FontLoader { get; set; } = DefaultFontLoader;

        public SKEncodedImageFormat RenderFormat { get; set; } = SKEncodedImageFormat.Png;

        /// <summary>
        /// Applies label over a white background after rendering all elements
        /// </summary>
        public bool OpaqueBackground { get; set; } = false;

        public int RenderQuality { get; set; } = 80;

        public bool ReplaceDashWithEnDash { get; set; } = true;

        public static Func<string, SKTypeface> DefaultFontLoader = fontName => {
            var typeface = SKTypeface.Default;
            if (fontName == "0")
            {
                var currentPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                var path = System.IO.Path.Join(currentPath, @"ATTriumvirate-CondensedBold.ttf");

                typeface = SKTypeface.FromFile(path);

                if (typeface is null)
                    typeface = SKTypeface.FromFamilyName("Helvetica", SKFontStyleWeight.Bold, SKFontStyleWidth.SemiCondensed, SKFontStyleSlant.Upright);
            }
            else
            {
                typeface = SKTypeface.FromFamilyName("DejaVu Sans Mono", SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
            }

            return typeface;
        };
    }
}
