using log4net.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemService._Infrastructure
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogMiddleware> _logger;

        private RequestResponseLog _logInfo;

        public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logInfo = new RequestResponseLog();

            HttpRequest request = context.Request;
            _logInfo.Url = request.Path.ToString();
            _logInfo.Headers = request.Headers.ToDictionary(k => k.Key, v => string.Join(";", v.Value.ToList()));
            _logInfo.Method = request.Method;
            _logInfo.ExcuteStartTime = DateTime.Now;

            //获取request.Body内容
            if (request.Method.ToLower().Equals("post"))
            {

                request.EnableBuffering(); //启用倒带功能，就可以让 Request.Body 可以再次读取

                Stream stream = request.Body;
                byte[] buffer = new byte[request.ContentLength.Value];
                stream.Read(buffer, 0, buffer.Length);
                _logInfo.RequestBody = Encoding.UTF8.GetString(buffer);

                request.Body.Position = 0;

            }
            else if (request.Method.ToLower().Equals("get"))
            {
                _logInfo.RequestBody = request.QueryString.Value;
            }

            //获取Response.Body内容
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                _logInfo.ResponseBody = await FormatResponse(context.Response);
                _logInfo.ExcuteEndTime = DateTime.Now;
                _logger.LogInformation(_logInfo.ToString());

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
    }

    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
