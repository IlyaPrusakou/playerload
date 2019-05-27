using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    [Serializable]
    public class FailedToPlayException: PlayerException     //AL5 - Player1 / 2.CustomExceptions.
    {
        public string Path { get; set; }
        public string CustomStackTrace { get; set; }

        public FailedToPlayException()
        {
        }
        public FailedToPlayException(string message) : base(message)
        {
        }
        public FailedToPlayException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected FailedToPlayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public FailedToPlayException(string path, string stacktrace, string message, Exception innerException): base (message, innerException)
        {
            Path = path;
            CustomStackTrace = stacktrace;
        }
    }
}
