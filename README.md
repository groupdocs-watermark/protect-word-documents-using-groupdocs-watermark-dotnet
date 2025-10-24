# 🛡️ Protect Word Documents using GroupDocs.Watermark for .NET

[![Product Page](https://img.shields.io/badge/Product%20Page-2865E0?style=for-the-badge&logo=appveyor&logoColor=white)](https://products.groupdocs.com/watermark/net/) 
[![Docs](https://img.shields.io/badge/Docs-2865E0?style=for-the-badge&logo=Hugo&logoColor=white)](https://docs.groupdocs.com/watermark/net/) 
[![Blog](https://img.shields.io/badge/Blog-2865E0?style=for-the-badge&logo=WordPress&logoColor=white)](https://blog.groupdocs.com/category/watermark/) 
[![Free Support](https://img.shields.io/badge/Free%20Support-2865E0?style=for-the-badge&logo=Discourse&logoColor=white)](https://forum.groupdocs.com/c/watermark) 
[![Temporary License](https://img.shields.io/badge/Temporary%20License-2865E0?style=for-the-badge&logo=rocket&logoColor=white)](https://purchase.groupdocs.com/temporary-license)

## 📖 About This Repository

This repository demonstrates practical implementations of **GroupDocs.Watermark for .NET, a tool for protecting Microsoft Word documents using various watermarking techniques.

The examples show how to: - Add simple and tiled text watermarks
- Lock headers and specific document sections with passwords
- Use editable content ranges to balance protection and usability
- Combine Word editing restrictions with GroupDocs.Watermark APIs

These implementations are designed for developers who want to move beyond Microsoft Word's basic watermark feature and use professional .NET routines for document protection.

## 💡 The Challenge

### What is GroupDocs.Watermark?

[**GroupDocs.Watermark for .NET**](https://docs.groupdocs.com/watermark/net/) is a powerful **document watermarking API** that allows developers to protect, brand,
and secure Word, PDF, Excel, PowerPoint, and over 40 other file formats programmatically.

**Key capabilities:** - Add, search, and remove watermarks of any type
- Lock watermarks with password protection
- Protect against unauthorized removal
- Automate watermarking for large-scale document processing
- Works without Microsoft Office --- pure .NET implementation

It's ideal for legal contracts, confidential reports, and branded client documents.

## ⚙️ Pre-requirements

To protect Word documents effectively, you can use one or more of the following methods:

1.  **Header watermark** -- Simple watermark added to the document header. Easy to implement, but can be manually removed.
2.  **Tiled watermark** -- Multiple repeated text instances across the page, making removal tedious.
3.  **Password-protected section** -- Watermark placed in a locked section, editable only with a password.
4.  **Locked header + editable ranges** -- Header area (with watermark) locked for editing, while selected body areas remain editable.
5.  **Full document restriction** -- Document locked using WordProcessingLockType with read-only or form-field access.

These methods are fully demonstrated in the included code examples. Each approach builds upon the previous one, offering increasing levels of
security.

## 📂 Repository Structure

    Protect-Word-Documents-using-GroupDocs.Watermark-for-.NET/
    │
    ├── GroupDocs.Watermark-for-.NET-Word-Protection-Sample.csproj  # .NET 6 project file
    ├── Program.cs                                                  # Entry point: runs protection routines
    │   ├── AddSimpleHeaderWatermark                                # Basic watermark in header
    │   ├── AddTiledWatermark                                       # Repeated tiled watermark
    │   ├── AddTiledImageWatermark                                  # Repeated tiled image watermark
    │   ├── AddLockedSectionWatermark                               # Password-protected hidden section
    │   └── AddLockedHeaderWatermark                                # Locked header + editable content
    ├── Resources/                                                  # Input/output test files(create this folder where you need)
    └── README.md                                                   # This documentation


## How to protect a Word document with tiled image watermark

**Protection Level:** Medium-High | **Difficulty:** Easy | **Best for:** Brand protection, copyright claims, and professional documents

Image watermarks take protection to the next level by using your company logo, signature, or custom graphics instead of plain text. When tiled across the document, they create a professional security layer that's harder to replicate or forge than simple text watermarks.

```csharp
private static void AddImageWatermark()
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
        watermarker.Save(Path.Combine(OutputDir, "image_watermark_word.docx"));
    }
} 
```
**See the professional image watermark in action:**

![action](https://github.com/groupdocs/groupdocs.github.io/blob/master/img/github_samples/groupdocs-watermark/tiled_image_watermark.gif)

## Advanced protection: Locked Header with Editable Content Ranges

**Protection Level:** Very High | **Difficulty:** Medium | **Best for:** Documents requiring both security and user interaction

This sophisticated approach combines locked headers with Microsoft Word's editable ranges feature. The watermark sits in a password-protected header that's completely locked, while specific document areas remain editable for legitimate users.

**How it works:** The entire header section (containing your watermark) is locked with read-only protection. The document body is then marked with editable ranges, creating a security model where users can only modify pre-approved sections.

```csharp
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
```

**Watch the locked header protection:**

![action](https://github.com/groupdocs/groupdocs.github.io/blob/master/img/github_samples/groupdocs-watermark/watermark_locked_in_header.gif)

## Related Topics to Investigate

If you are working with documents, the following topics may be useful for further investigation:

* **AI-Powered Watermarking: Protect Documents with Smart Context-Aware Marking** – Learn how to integrate GroupDocs.Watermark into your AI agent:  
  [AI-Powered Watermarking](https://blog.groupdocs.com/watermark/ai-driven-dynamic-watermarks/)
   
* **Python Tiling Watermark Examples: How to Create Repeated Watermarks in Documents** – See how GroupDocs operates with different types of tiling watermarks on Python:  
  [Python Tiling Watermark Examples](https://blog.groupdocs.com/watermark/tiling-watermark-python/)  

> 💬 *This repository is part of GroupDocs educational samples.
> All names and data are automatically generated for demonstration purposes only.*

[![Product Page](https://img.shields.io/badge/Product%20Page-2865E0?style=for-the-badge&logo=appveyor&logoColor=white)](https://products.groupdocs.com/watermark/net/) 
[![Docs](https://img.shields.io/badge/Docs-2865E0?style=for-the-badge&logo=Hugo&logoColor=white)](https://docs.groupdocs.com/watermark/net/) 
[![Blog](https://img.shields.io/badge/Blog-2865E0?style=for-the-badge&logo=WordPress&logoColor=white)](https://blog.groupdocs.com/category/watermark/) 
[![Free Support](https://img.shields.io/badge/Free%20Support-2865E0?style=for-the-badge&logo=Discourse&logoColor=white)](https://forum.groupdocs.com/c/watermark) 
[![Temporary License](https://img.shields.io/badge/Temporary%20License-2865E0?style=for-the-badge&logo=rocket&logoColor=white)](https://purchase.groupdocs.com/temporary-license)
