using System;

namespace GobelinsWorld.Web.Models
{
    public class ErrorViewModel : HomeIndexViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}