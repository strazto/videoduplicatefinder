using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
// MediaTypeHeaderValue
using Microsoft.Net.Http.Headers;

namespace DuplicateFinderWeb.Data.Formatters
{
	public class ImageOutputFormatter : OutputFormatter
	{
		public ImageOutputFormatter()
		{
			SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("image/jpeg"));
		}
		protected override bool CanWriteType(Type type) {
			return typeof(Image) == type;
		}

		public override async Task WriteResponseBodyAsync(
			OutputFormatterWriteContext context)
		{
			var httpContext = context.HttpContext;
			var serviceProvider = httpContext.RequestServices;

			var logger = serviceProvider.GetRequiredService<ILogger<ImageOutputFormatter>>();
			
			if (! (context.Object is Image)) {
				logger.LogInformation("Cannot format {Object} of type {ObjectType}", context.Object, context.ObjectType);
				httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				return;

			}


			Image img = (Image)context.Object;
			logger.LogInformation("Formatting {@image}", img);
			using (MemoryStream memStream = new MemoryStream())
			{
				img.Save(memStream, ImageFormat.Jpeg);
				Byte[] buffer = memStream.ToArray();
				httpContext.Response.ContentType = "image/jpeg";
				await httpContext.Response.Body.WriteAsync(buffer);
			}

			
		}
	}
}
