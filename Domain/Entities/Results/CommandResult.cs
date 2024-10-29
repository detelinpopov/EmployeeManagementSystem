namespace Domain.Entities.Results
{
    public abstract class CommandResult
    {
        public bool Success => !Errors.Any();

        public IList<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

        public string GetErrorsAsString()
        {
            return string.Join(",", Errors.Select(e => e.ErrorMessage).ToArray());
        }
    }
}
