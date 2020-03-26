using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks
{
    /// <summary>
    /// Serialization action class
    /// </summary>
    public abstract class SerializeAction
    {
        protected const object NoResponse = null;

        protected const string ApplicationJSON = "application/json";

        private readonly InfonetLog _log = InfonetLogManager.GetLogger<SerializeAction>();

        protected SerializeAction(string name)
        {
            Name = name;
            AttemptCount = 0;
        }

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Attempt count 
        /// </summary>
        public int AttemptCount { get; private set; }

        /// <summary>
        /// Response Value
        /// </summary>
        public object ResponseValue { get; set; }

        /// <summary>
        /// Call this function to perform work of this task
        /// </summary>
        /// <returns></returns>
        public async Task Perform()
        {
            AttemptCount++;
            ResponseValue = null;

            _log.Info(string.Format("Performing {0} Attempt={1}", Name, AttemptCount));

            ResponseValue = await OnPerform();
        }

        /// <summary>
        /// Override in base class to perform work for this task
        /// </summary>
        /// <returns>option response value object</returns>
        protected abstract Task<object> OnPerform();

        /// <summary>
        /// Handles the Generic exception thrown by the API
        /// </summary>
        /// <param name="data">data from the API</param>
        protected async Task<object> HandleExceptions(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    if (data.Contains("Invalid Auth token"))
                    {
                        throw UserNotAuthorizedException(data);
                    }
                    else
                    {
                        throw ApiDataException(data);
                    }
                case HttpStatusCode.NoContent:
                case HttpStatusCode.Conflict:
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.NotAcceptable:
                    throw ApiDataException(data);
                case HttpStatusCode.NotFound:
                    throw NotFoundException(data);
                case HttpStatusCode.Forbidden:
                    throw SwitchUserException(data);
                case HttpStatusCode.InternalServerError:
                    throw InternalServerException(data);
                case HttpStatusCode.BadGateway:
                    throw ServerNotConnectedException();
                default:
                    return NoResponse;
            }
        }

        /// <summary>
        /// Generates Api Data Exception based on the data
        /// </summary>
        /// <param name="data">JSON Data</param>
        /// <returns>API Data Exception</returns>
        protected ApiDataException NotFoundException(string data)
        {
            try
            {
                var error = new DeSerializer().MapError(data);

                var errorModel = new Mapper().MapError(error);
                return new ApiDataException
                {
                    Error = errorModel
                };
            }
            catch (Exception ex)
            {
                return new ApiDataException
                {
                    Error = new EntityLayer.Entities.Common.Error
                    {
                        Message = "Server is not connected"
                    }
                };
            }
        }

        /// <summary>
        /// Generates Api Data Exception based on the data
        /// </summary>
        /// <param name="data">JSON Data</param>
        /// <returns>API Data Exception</returns>
        protected ApiDataException ApiDataException(string data)
        {
            var error = new DeSerializer().MapError(data);

            var errorModel = new Mapper().MapError(error);
            return new ApiDataException
            {
                Error = errorModel
            };
        }

        /// <summary>
        /// Generates UserAlreadyLoggedOnException based on the data
        /// </summary>
        /// <param name="data">JSON Data</param>
        /// <returns>UserAlreadyLoggedOnException</returns>
        protected UserAlreadyLoggedOnException UserAlreadyLoggedOnException(string data)
        {
            var error = new DeSerializer().MapError(data);

            var errorModel = new Mapper().MapError(error);
            return new UserAlreadyLoggedOnException
            {
                Error = errorModel
            };
        }

        /// <summary>
        /// Generates Purchase Order Required Exception
        /// </summary>
        /// <param name="data">JSON Data</param>
        /// <returns>Purchase Order Required Exception Exception</returns>
        protected PurchaseOrderRequiredException PurchaseOrderRequiredException(string data)
        {
            var error = new DeSerializer().MapError(data);

            var errorModel = new Mapper().MapError(error);
            return new PurchaseOrderRequiredException
            {
                Error = errorModel
            };
        }

        /// <summary>
        /// Generates Internal Server Exception when API Returns 500
        /// </summary>
        /// <param name="data">JSON Data</param>
        /// <returns>InternalServerException</returns>
        protected InternalServerException InternalServerException(string data)
        {
            var error = new DeSerializer().MapInternalServerError(data);

            var errorModel = new Mapper().MapError(error);
            return new InternalServerException
            {
                Error = errorModel
            };
        }

        protected ApiDataException ServerNotConnectedException()
        {
            return new ApiDataException
            {
                Error = new EntityLayer.Entities.Common.Error
                {
                    Message = "Could not establish the connection with API"
                }
            };
        }

        /// <summary>
        /// Generates Switch user exception when API Returns 403
        /// </summary>
        /// <param name="data">JSON Data</param>
        /// <returns>SwitchUserException</returns>
        protected SwitchUserException SwitchUserException(string data)
        {
            var error = new DeSerializer().MapError(data);

            var errorModel = new Mapper().MapError(error);
            return new SwitchUserException
            {
                Error = errorModel
            };
        }

        /// <summary>
        /// Generates User not authorized exception
        /// </summary>
        /// <param name="data">JSON Data</param>
        /// <returns>InternalServerException</returns>
        protected UserNotAuthorizedException UserNotAuthorizedException(string data)
        {
            var error = new DeSerializer().MapError(data);

            var errorModel = new Mapper().MapError(error);
            return new UserNotAuthorizedException
            {
                Error = errorModel
            };
        }

        protected PumpsOfflineException PumpsOfflineException(string data)
        {
            var error = new DeSerializer().MapError(data);

            var errorModel = new Mapper().MapError(error);
            return new PumpsOfflineException
            {
                Error = errorModel
            };
        }
    }
}
