﻿using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using ZXing.Common;
using ZXing.CoreCompat.System.Drawing;
using ZXing.ImageSharp;

namespace QRCode.Controllers
{
    [ApiController]
    public class DecodeController : ControllerBase
    {
        [HttpPost]
        [Route("api/v1/decode")]
        public dynamic Decode([FromBody] dynamic request)
        {
            string base64 = request.content;
            byte[] data = Convert.FromBase64String(base64);

            using (var stream = new MemoryStream(data))
            {
                //var bitmap = Bitmap.FromStream(stream) as Bitmap; //Problems with the library on non-Windows platforms
                var bitmap = SixLabors.ImageSharp.Image.Load(stream); //Works fine
                var result = Decode(bitmap);

                //QR Code not found or cannot be decoded
                if (result == null)
                {
                    return new ContentResult
                    {
                        ContentType = "application/json",
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Content = "{\"status\":\"error\"}"
                    };
                }

                //QR Code found and decoded
                return new
                {
                    status = "success",
                    type = result.BarcodeFormat.ToString().ToLower(),
                    text = result.Text
                };
            }
        }

        protected ZXing.Result Decode(Bitmap bitmap)
        {
            //PM> Install-Package ZXing.Net.Bindings.CoreCompat.System.Drawing
            var source = new BitmapLuminanceSource(bitmap);

            //PM> Install-Package ZXing.Net
            var reader = new BarcodeReader(null, null, ls => new GlobalHistogramBinarizer(ls))
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions
                {
                    TryHarder = true,
                }
            };

            var result = reader.Decode(source);
            return result;
        }

        protected ZXing.Result Decode(Image<Rgba32> bitmap)
        {
            //PM> Install-Package ZXing.Net.Bindings.ImageSharp
            var source = new ImageSharpLuminanceSource<Rgba32>(bitmap);

            //PM> Install-Package ZXing.Net
            var reader = new BarcodeReader(null, null, ls => new GlobalHistogramBinarizer(ls))
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions
                {
                    TryHarder = true,
                }
            };

            var result = reader.Decode(source);
            return result;
        }
    }
}
