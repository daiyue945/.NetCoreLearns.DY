using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace 数据校验
{
    public static class IdentityHelper
    {
        public static async Task CheckAsync(this Task<IdentityResult> task)
        {
            var r=await task;
            if (!r.Succeeded)
            {
                throw new Exception(JsonSerializer.Serialize(r.Errors));
            }
        }
    }
}
