using System.Collections.Generic;

namespace Sat.Recruitment.Application.Model
{
    /// <summary>
    /// This is used for API v1.
    /// </summary>
    public class ResultV1
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
