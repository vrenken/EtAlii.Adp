// Copyright (c) Peter Vrenken. All rights reserved.

using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Execution.Processing;
using HotChocolate.Resolvers;

namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public class ErrorLoggingDiagnosticsEventListener(ILogger<ErrorLoggingDiagnosticsEventListener> logger)
    : ExecutionDiagnosticEventListener
{
    public override void ResolverError(
        IMiddlewareContext context,
        IError error)
    {
        logger.LogError(error.Exception, "Error in resolver: {ErrorMessage}", error.Message);
    }

    public override void TaskError(
        IExecutionTask task,
        IError error)
    {
        logger.LogError(error.Exception, "Error in task: {ErrorMessage}", error.Message);
    }

    public override void RequestError(
        IRequestContext context,
        Exception exception)
    {
        logger.LogError(exception, "Error in request with {OperationId}", context.OperationId);
    }

    public override void SubscriptionEventError(
        SubscriptionEventContext context,
        Exception exception)
    {
        logger.LogError(exception, "Event Error in subscription with {SubscriptionId}", context.Subscription.Id);
    }

    public override void SubscriptionTransportError(
        ISubscription subscription,
        Exception exception)
    {
        logger.LogError(exception, "Transport error in subscription with {SubscriptionId} ", subscription.Id);
    }

    public override void SyntaxError(
        IRequestContext context,
        IError error)
    {
        logger.LogError(error.Exception, "Syntax error in request with {OperationId}: {ErrorMessage}", context.OperationId, error.Message);
    }

    public override void ResolverError(
        IRequestContext context,
        ISelection selection,
        IError error)
    {
        logger.LogError(error.Exception, "Resolver error in request with {OperationId}: {ErrorMessage}", context.OperationId, error.Message);
    }

    public override void ValidationErrors(
        IRequestContext context,
        IReadOnlyList<IError> errors)
    {
        foreach (var error in errors)
        {
            logger.LogError(error.Exception, "Validation error in request with {OperationId}: {ErrorMessage}", context.OperationId, error.Message);
        }
    }
}
