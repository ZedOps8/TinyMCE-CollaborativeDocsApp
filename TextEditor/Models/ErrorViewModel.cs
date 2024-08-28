namespace TextEditor.Models
{
    // The ErrorViewModel class is used to pass error information to the error view.
    public class ErrorViewModel
    {
        // RequestId holds the unique identifier for the current request, used for error tracking.
        public string? RequestId { get; set; }

        // ShowRequestId is a computed property that determines whether the RequestId is not null or empty.
        // It is used in the error view to conditionally display the RequestId.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
