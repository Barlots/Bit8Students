using System.Linq;
using FluentResults;

namespace Bit8.Students.WebApi.Common
{
    public static class ReasonExtensions
    {
        public static string[] ToErrorArray(this Result result)
        {
            return result.Errors.Select(x => x.Message).ToArray();
        }
    }
}