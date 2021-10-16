using System.Diagnostics;

namespace net6_api.PipelineBehaviors
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
                                            CancellationToken cancellationToken,
                                            RequestHandlerDelegate<TResponse> next)
        {
            var timer = Stopwatch.StartNew();

            /// pre
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine($"{request.GetType().Name} is executing");
            //Console.ResetColor();
            _logger.LogInformation($"{request.GetType().Name} is executing");
            var response = await next();

            /// post
            timer.Stop();
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine($"{request.GetType().Name} has finished in {timer.ElapsedMilliseconds}");
            //Console.ResetColor();
            _logger.LogInformation($"{request.GetType().Name} has finished in {timer.ElapsedMilliseconds}");
            return response;
        }
    }
}
