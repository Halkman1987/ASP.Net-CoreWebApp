namespace WebApplication.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            LogConsole(context);
            await LogFile(context);
            await _next(context);

           /* Console.WriteLine($"[{DateTime.Now}]: New reqest to http://{context.Request.Host.Value + context.Request.Path}");
            await _next.Invoke(context);*/
        }
        private void LogConsole(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }
        private async Task LogFile(HttpContext context)
        {
            string logMess = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
            //await File.AppendAllTextAsync(logFilePath, logMess);


            string dest_path = xml_destination_path_tb.Text + "/" + corps + " корпус/" + floor + " этаж/" + cabinet + " кабинет";
            
            if (Directory.Exists(logFilePath) != true) Directory.CreateDirectory(logFilePath);
            try
            {
                File.Copy(filename, dest_path + "/" + Path.GetFileName(filename));
            }
            catch (IOException copyError)
            {
                MessageBox.Show(copyError.Message);
            }
        } 
    }
}
