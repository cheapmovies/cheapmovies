// RetryHelper. For more features, use Polly.
// https://alastaircrabtree.com/implementing-the-retry-pattern-for-async-tasks-in-c/

using System;
using System.Linq;
using System.Threading.Tasks;
//using log4net;

namespace CheapMovies.Common.Utilities
{
    public static class RetryHelper
    {
        //private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static async Task<string> RetryOnExceptionAsync(
            int times, TimeSpan delay, Func<Task<string>> operation)
        {
            return await RetryOnExceptionAsync<Exception>(times, delay, operation);
        }

        public static async Task<string> RetryOnExceptionAsync<TException>(
            int times, TimeSpan delay, Func<Task<string>> operation) where TException : Exception
        {
            string result = string.Empty;

            if (times <= 0) 
                throw new ArgumentOutOfRangeException(nameof(times));

            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    result = await operation();
                    break;
                }
                catch (TException ex)
                {
                    if (attempts == times)
                        // Give up but log it.
                        return string.Empty;

                    await CreateDelayForException(times, attempts, delay, ex);
                }
            } while (true);

            return result;
        }
        
        private static Task CreateDelayForException(
            int times, int attempts, TimeSpan delay, Exception ex)
        {
            if (delay == null)
            {
                var delaySeconds = IncreasingDelayInSeconds(attempts);
                delay = TimeSpan.FromSeconds(delaySeconds);
            }
            // Log.Warn($"Exception on attempt {attempts} of {times}. " + 
            //           "Will retry after sleeping for {delay}.", ex);
            return Task.Delay(delay);
        }
        
        internal static int[] DelayPerAttemptInSeconds = 
        {
            (int) TimeSpan.FromSeconds(2).TotalSeconds,
            (int) TimeSpan.FromSeconds(30).TotalSeconds,
            (int) TimeSpan.FromMinutes(2).TotalSeconds,
            (int) TimeSpan.FromMinutes(10).TotalSeconds,
            (int) TimeSpan.FromMinutes(30).TotalSeconds
        };
        
        static int IncreasingDelayInSeconds(int failedAttempts)
        {
            if (failedAttempts <= 0) throw new ArgumentOutOfRangeException();

            return failedAttempts > DelayPerAttemptInSeconds.Length ? DelayPerAttemptInSeconds.Last() : DelayPerAttemptInSeconds[failedAttempts];
        }
    }
}