using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using CPTech.Models;

namespace CPTech.MiddleWare
{
    #region ExceptionMiddleware
    public class ExceptionMiddleware
    {
        /// <summary>
        /// 处理HTTP请求的函数。
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //抛给下一个中间件
                await _next(context);
            }
            catch (NetException ex)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(SerializerErrorMessage(ex.Code, ex.Message)).ConfigureAwait(false);
            }
            catch (FormatException)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(SerializerErrorMessage(500, "二维码无效，请重新获取二维码")).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                response.ContentType = "application/json";
                await context.Response.WriteAsync(SerializerErrorMessage(500, message)).ConfigureAwait(false);
            }
            finally
            {
                await WriteExceptionAsync(context);
            }
        }

        private async Task WriteExceptionAsync(HttpContext context)
        {
            var code = context.Response.StatusCode;
            switch (code)
            {
                case 200:
                case 204:
                    return;
                case 401:
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(SerializerErrorMessage(code, "token已过期，请重新登录！")).ConfigureAwait(false);
                    break;
                default:
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(SerializerErrorMessage(code, $"未知错误，code:{code}！")).ConfigureAwait(false);
                    break;
            }
        }

        private string SerializerErrorMessage(int code, string message)
        {
            return JsonSerializer.Serialize(ResultModel.Error(code, message),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
        }
    }
    #endregion

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseException(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionMiddleware>();
    }
}
