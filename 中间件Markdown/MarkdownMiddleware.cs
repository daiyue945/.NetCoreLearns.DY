using System.Text;

namespace 中间件Markdown
{
    public class MarkdownMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment hostenv;
        public MarkdownMiddleware(RequestDelegate next, IWebHostEnvironment hostenv)
        {
            this.next = next;
            this.hostenv = hostenv;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.ToString();//请求文件路径
            if (!path.EndsWith(".md", true, null))//只处理md文件，忽略大小写
            {
                await next.Invoke(context);
                return;
            }
            var file = hostenv.WebRootFileProvider.GetFileInfo(path);//根据路径找到文件
            if (!file.Exists)
            {
                await next.Invoke(context);//如果当前文件不存在，直接往下走
                return;
            }
            using var stream = file.CreateReadStream();//文件转换成流
            //检测流的编码格式
            Ude.CharsetDetector detector = new Ude.CharsetDetector();
            detector.Feed(stream);
            detector.DataEnd();
            string charset = detector.Charset ?? "UTF-8";//如果格式为空，默认为utf-8
            stream.Position = 0;//流的位置复原,因为CharsetDetector已经把stream的指针往后挪了，需要重新挪回去
            //重新读取流
            using StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
            string mdText = await reader.ReadToEndAsync();//读取出了markdown格式的md文件

            MarkdownSharp.Markdown md = new MarkdownSharp.Markdown();
            string html = md.Transform(mdText);//md->html
            context.Response.ContentType = "text/html;charset=UTF-8";
            await context.Response.WriteAsync(html);
        }
    }
}
