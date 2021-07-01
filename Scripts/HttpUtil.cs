using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Core_Web_API_test.Scripts {

    public class HttpUtil {

        public async static Task<string> GetBody (HttpRequest request) {
            var reader = new StreamReader(request.Body);
            return await reader.ReadToEndAsync();
        }

        public async static Task<T> GetJsonBody <T> (HttpRequest request) {
            var body = await GetBody(request);
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
