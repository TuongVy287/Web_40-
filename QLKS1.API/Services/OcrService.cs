// using System.Text.RegularExpressions;
// using Tesseract;

// namespace QLKS.API.Services
// {
//     public class OcrService : IOcrService
//     {
//         private readonly string _tessdataPath = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");

//         public object ExtractCCCDInfo(Stream imageStream)
//         {
//             using var engine = new TesseractEngine(_tessdataPath, "vie", EngineMode.Default);
//             using var img = Pix.LoadFromMemory(ReadFully(imageStream));
//             using var page = engine.Process(img);

//             string text = page.GetText();

//             return ParseCCCDText(text);
//         }

//         private byte[] ReadFully(Stream input)
//         {
//             using var ms = new MemoryStream();
//             input.CopyTo(ms);
//             return ms.ToArray();
//         }

//         private object ParseCCCDText(string text)
//         {
//             var lines = text.Split('\n').Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

//             string fullName = lines.FirstOrDefault(l => Regex.IsMatch(l, @"^[A-Z\s]+$")) ?? "";
//             string dob = lines.FirstOrDefault(l => Regex.IsMatch(l, @"\d{2}/\d{2}/\d{4}")) ?? "";
//             string idNumber = lines.FirstOrDefault(l => Regex.IsMatch(l, @"\d{12}")) ?? "";

//             return new
//             {
//                 FullName = fullName,
//                 DateOfBirth = dob,
//                 CCCDNumber = idNumber
//             };
//         }
//     }
// }
