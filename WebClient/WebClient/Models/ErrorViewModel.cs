using System;

namespace WebClient.Models
{
    /// <summary>
    /// Error view model
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Request id;
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Is show request id
        /// </summary>
        public bool ShowRequestId
        {
            get
            {
                return !string.IsNullOrEmpty(this.RequestId);
            }
        }
    }
}