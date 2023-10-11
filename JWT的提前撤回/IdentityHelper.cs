using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace JWT的提前撤回
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
