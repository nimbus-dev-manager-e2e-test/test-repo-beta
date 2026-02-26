namespace Nimbus.TestRepoBeta;

/// <summary>
/// Result of a workflow execution.
/// </summary>
public class WorkflowResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public int ItemsProcessed { get; set; }
}
