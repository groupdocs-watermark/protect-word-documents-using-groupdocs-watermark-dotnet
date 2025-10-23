using GroupDocs.Watermark;
using GroupDocs.Watermark.Common;
using GroupDocs.Watermark.Options.WordProcessing;
using GroupDocs.Watermark.Watermarks;

namespace GroupDocs.Watermark_for_.NET_Word_Protection_Sample
{
    class Program
    {
        private const string LicensePath = "license.lic";
        private static readonly string InputFile = Path.Combine("Resources", "Resume.docx");
        private static readonly string OutputDir = Path.Combine("Results");

        static void Main()
        {
            Console.WriteLine("=== GroupDocs.Watermark Word Sample ===");

            ApplyLicense();
            EnsureOutputDirectory();

            // Choose the example you want to run:
            AddSimpleHeaderWatermark();
            // AddTiledWatermark();
            // AddLockedWatermark_AllowOnlyFormFields();
            // AddLockedHeaderWatermark();

            Console.WriteLine("Done!");
        }

        private static void ApplyLicense()
        {
            try
            {
                var license = new License();
                license.SetLicense(LicensePath);
                Console.WriteLine("License applied successfully.");
            }
            catch
            {
                Console.WriteLine("Warning: License not found. Running in evaluation mode.");
            }
        }

        private static void EnsureOutputDirectory()
        {
            if (!Directory.Exists(OutputDir))
                Directory.CreateDirectory(OutputDir);
        }

        private static void AddSimpleHeaderWatermark()
        {
            Console.WriteLine("Adding simple header watermark...");

            var loadOptions = new WordProcessingLoadOptions();
            using (var watermarker = new Watermarker(InputFile, loadOptions))
            {
                var watermark = new TextWatermark("Confidential", new Font("Arial", 19))
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    RotateAngle = 25,
                    ForegroundColor = Color.Red,
                    Opacity = 0.8
                };
                watermarker.Add(watermark, options);
                watermarker.Save(Path.Combine(OutputDir, "header_watermark.docx"));
            }

            Console.WriteLine("Header watermark added.");
        }

        private static void AddTiledWatermark()
        {
            Console.WriteLine("Adding tiled watermark...");

            var loadOptions = new WordProcessingLoadOptions();
            using (var watermarker = new Watermarker(InputFile, loadOptions))
            {
                var watermark = new TextWatermark("Protected Document", new Font("Arial", 19))
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    RotateAngle = 25,
                    ForegroundColor = Color.Red,
                    Opacity = 0.9,
                    TileOptions = new TileOptions
                    {
                        LineSpacing = new MeasureValue
                        {
                            MeasureType = TileMeasureType.Percent,
                            Value = 12
                        },
                        WatermarkSpacing = new MeasureValue
                        {
                            MeasureType = TileMeasureType.Percent,
                            Value = 12
                        }
                    }
                };

                var options = new WordProcessingWatermarkSectionOptions
                {
                    Name = "TiledShape",
                    AlternativeText = "Repeated watermark"
                };

                watermarker.Add(watermark, options);
                watermarker.Save(Path.Combine(OutputDir, "tiled_watermark.docx"));
            }

            Console.WriteLine("Tiled watermark added.");
        }

        private static void AddTiledImageWatermark()
        {        
            using (Watermarker watermarker = new Watermarker(InputFile))
            {
                // Create the image watermark object
                var watermark = new ImageWatermark("logo.png");

                // Configure tile options
                watermark.TileOptions = new TileOptions()
                {
                    LineSpacing = new MeasureValue()
                    {
                        MeasureType = TileMeasureType.Percent,
                        Value = 10
                    },
                    WatermarkSpacing = new MeasureValue()
                    {
                        MeasureType = TileMeasureType.Percent,
                        Value = 8
                    },
                };

                // Set watermark properties
                watermark.Opacity = 0.7;
                watermark.RotateAngle = -30;

                // Add watermark
                watermarker.Add(watermark);
                watermarker.Save(OutputDir, "image_watermark.docx");
            }
        } 

        private static void AddLockedWatermark_AllowOnlyFormFields()
        {
            Console.WriteLine("Adding locked watermark (allow form fields)...");

            using (var watermarker = new Watermarker(InputFile))
            {
                var watermark = new TextWatermark("Do Not Edit", new Font("Arial", 36, FontStyle.Bold | FontStyle.Italic))
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Opacity = 0.4,
                    RotateAngle = 45,
                    ForegroundColor = Color.Red
                };

                var options = new WordProcessingWatermarkPagesOptions
                {
                    IsLocked = true,
                    Password = "012345",
                    LockType = WordProcessingLockType.AllowOnlyFormFields
                };

                watermarker.Add(watermark, options);
                watermarker.Save(Path.Combine(OutputDir, "locked_allow_form_fields.docx"));
            }

            Console.WriteLine("Locked watermark added (AllowOnlyFormFields).");
        }

        private static void AddLockedHeaderWatermark()
        {
            Console.WriteLine("Adding locked header watermark...");

            var loadOptions = new WordProcessingLoadOptions();
            using (var watermarker = new Watermarker(InputFile, loadOptions))
            {
                var watermark = new TextWatermark("Company Confidential", new Font("Arial", 19))
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    RotateAngle = 25,
                    ForegroundColor = Color.Red,
                    Opacity = 0.8
                };

                var options = new WordProcessingWatermarkSectionOptions
                {
                    SectionIndex = 0,
                    IsLocked = true,
                    Password = "012345",
                    LockType = WordProcessingLockType.ReadOnly
                };

                watermarker.Add(watermark, options);
                watermarker.Save(Path.Combine(OutputDir, "locked_header_watermark.docx"));
            }

            Console.WriteLine("Locked header watermark added.");
        }
    }
}
