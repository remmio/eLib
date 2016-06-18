using System;

namespace eLib.Crud
{
    public interface IUpdate
    {
        void OnCreated(Guid entityGuid);
        void OnUpdated(Guid entityGuid);
        void OnDeleted(Guid entityGuid);
        void OnMessage(string message);
    }

    public class ServiceEndPoint
    {
        public ServiceEndPoint(string serviceName, string url, string port, bool isSignalRSupported)
        {
            ServiceName = serviceName;
            Url = url;
            Port = port;
            IsSignalRSupported = isSignalRSupported;
        }

        public string ServiceName { get; set; }
        public string Url { get; set; }
        public string Port { get; set; }
        public bool IsSignalRSupported { get; set; }
    }

}
