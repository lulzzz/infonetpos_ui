using System;
using System.Threading.Tasks;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.EntityLayer.Exception;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class SerializeManager
    {
        private const int MaxRetryAttempts = 1;

        private const int RetryDelayMs = 1111;

        //initialize log for this class
        private InfonetLog _log = InfonetLogManager.GetLogger<SerializeManager>();

        protected async Task PerformTask(SerializeAction serializeTask)
        {
            var success = false;

            //perform the task up to MAX_RETRY_ATTEMPTS attempts,
            while (!success && serializeTask.AttemptCount < MaxRetryAttempts)
            {
                try
                {
                    await serializeTask.Perform();
                    success = true;
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(InternalServerException))
                    {  //if any exception occurs, we might retry
                        if (serializeTask.AttemptCount >= MaxRetryAttempts)
                        {
                            //to many times, bubble up exception
                            throw;
                        }

                        //try again
                        success = false;

                        //delay between retry attempts
                        await Task.Delay(RetryDelayMs);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

    }
}
