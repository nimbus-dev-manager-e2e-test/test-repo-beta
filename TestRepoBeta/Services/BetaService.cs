using Microsoft.Extensions.Logging;
using Nimbus.TestRepoGamma.Services;

namespace Nimbus.TestRepoBeta.Services;

/// <summary>
/// Business logic service that orchestrates operations across the platform.
/// Depends on GammaService for shared validation and processing.
/// </summary>
public class BetaService
{
    private readonly ILogger<BetaService> _logger;
    private readonly GammaService _gammaService;

    public BetaService(ILogger<BetaService> logger, GammaService gammaService)
    {
        _logger = logger;
        _gammaService = gammaService;
    }

    /// <summary>
    /// Executes the main workflow: validate then process.
    /// </summary>
    public async Task<WorkflowResult> RunWorkflowAsync(string input, CancellationToken ct = default)
    {
        _logger.LogInformation("Starting workflow for input: {Input}", input);

        if (!_gammaService.ValidateInput(input))
        {
            return new WorkflowResult { Success = false, Error = "Validation failed" };
        }

        var items = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var processed = await _gammaService.ProcessBatchAsync(items, ct);

        return new WorkflowResult
        {
            Success = true,
            ItemsProcessed = processed
        };
    }
}
