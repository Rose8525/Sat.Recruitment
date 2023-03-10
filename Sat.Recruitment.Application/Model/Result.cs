namespace Sat.Recruitment.Application.Model
{
    /*
     * Suggestion: Change the string to string[].
     * As it's a refactoring; We dont want to change the contract.
    */
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
