using System;
using System.Threading.Tasks;
using Furion;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.UnifyResult;
using Furion.UnifyResult.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExamKing.Core.Providers;

/// <summary>RESTful 风格返回值</summary>
[SuppressSniffer]
[UnifyModel(typeof(Furion.UnifyResult.RESTfulResult<>))]
public class RESTfulResultProvider : IUnifyResultProvider
{
    /// <summary>异常返回值</summary>
    /// <param name="context"></param>
    /// <param name="metadata"></param>
    /// <returns></returns>
    public
    #nullable disable
    IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata) =>
    (IActionResult)new JsonResult(
        (object)RESTfulResultProvider.RESTfulResult(metadata.StatusCode, errors: metadata.Errors));

    /// <summary>成功返回值</summary>
    /// <param name="context"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public IActionResult OnSucceeded(ActionExecutedContext context, object data) =>
        (IActionResult)new JsonResult((object)RESTfulResultProvider.RESTfulResult(200, true, data));

    /// <summary>验证失败返回值</summary>
    /// <param name="context"></param>
    /// <param name="metadata"></param>
    /// <returns></returns>
    public IActionResult OnValidateFailed(
        ActionExecutingContext context,
        ValidationMetadata metadata)
    {
        return (IActionResult)new JsonResult(
            (object)RESTfulResultProvider.RESTfulResult(400, errors: ((object)metadata.ValidationResult)));
    }

    /// <summary>特定状态码返回值</summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    /// <param name="unifyResultSettings"></param>
    /// <returns></returns>
    public async Task OnResponseStatusCodes(
        HttpContext context,
        int statusCode,
        UnifyResultSettingsOptions unifyResultSettings)
    {
        UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);
        switch (statusCode)
        {
            case 401:
                await context.Response.WriteAsJsonAsync<Furion.UnifyResult.RESTfulResult<object>>(
                    RESTfulResultProvider.RESTfulResult(statusCode, errors: ((object)"401 Unauthorized")),
                    App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                break;
            case 403:
                await context.Response.WriteAsJsonAsync<Furion.UnifyResult.RESTfulResult<object>>(
                    RESTfulResultProvider.RESTfulResult(statusCode, errors: ((object)"403 Forbidden")),
                    App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                break;
        }
    }

    /// <summary>返回 RESTful 风格结果集</summary>
    /// <param name="statusCode"></param>
    /// <param name="succeeded"></param>
    /// <param name="data"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    private static Furion.UnifyResult.RESTfulResult<object> RESTfulResult(
        int statusCode,
        bool succeeded = false,
        object data = null,
        object errors = null)
    {
        return new Furion.UnifyResult.RESTfulResult<object>()
        {
            StatusCode = new int?(statusCode),
            Succeeded = succeeded,
            Data = data,
            Errors = errors,
            Extras = UnifyContext.Take(),
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
    }
}