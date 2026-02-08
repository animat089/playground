using AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Options;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation;
using AnimatLabs.WorkflowForge.Workflows.Sample.NightlyReconciliation.Services;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WorkflowForge.Loggers;
using WF = WorkflowForge.WorkflowForge;

namespace AnimatLabs.WorkflowForge.CoravelScheduledWorkflows.Jobs;

public sealed class ReconciliationJob : IInvocable
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentService _paymentService;
    private readonly IInventoryService _inventoryService;
    private readonly IEmailSender _emailSender;
    private readonly IOptions<ReconciliationJobOptions> _options;
    private readonly ILogger<ReconciliationJob> _logger;

    public ReconciliationJob(
        IOrderRepository orderRepository,
        IPaymentService paymentService,
        IInventoryService inventoryService,
        IEmailSender emailSender,
        IOptions<ReconciliationJobOptions> options,
        ILogger<ReconciliationJob> logger)
    {
        _orderRepository = orderRepository;
        _paymentService = paymentService;
        _inventoryService = inventoryService;
        _emailSender = emailSender;
        _options = options;
        _logger = logger;
    }

    public async Task Invoke()
    {
        var options = _options.Value;

        _logger.LogInformation("Starting scheduled reconciliation workflow (DemoFailure={DemoFailure})", options.DemoFailure);

        var workflow = NightlyReconciliationWorkflow.Build(
            _orderRepository,
            _paymentService,
            _inventoryService,
            _emailSender);

        using var foundry = WF.CreateFoundry(
            workflowName: workflow.Name,
            initialProperties: new Dictionary<string, object?>
            {
                [ReconciliationKeys.BatchSize] = 3,
                [ReconciliationKeys.DemoFailure] = options.DemoFailure
            });

        using var smith = WF.CreateSmith(
            new ConsoleLogger("WF"));

        try
        {
            await smith.ForgeAsync(workflow, foundry).ConfigureAwait(false);
            _logger.LogInformation("Reconciliation workflow finished successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Reconciliation workflow failed (compensation should have run for completed steps)");
        }
    }
}