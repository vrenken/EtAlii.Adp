using System.Reactive;
using ReactiveUI;
using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client;

public partial class EditPageViewModel : ReactiveObject
{
    /// <summary>
    /// Defines Diagram's nodes collection.
    /// </summary>
    public Node[] Nodes => _nodes.Value;
    private readonly ObservableAsPropertyHelper<Node[]> _nodes;
    public ReactiveCommand<Unit, Node[]> LoadNodes { get; }
    private readonly IReadOnlyList<Node> _loadedNodes;
    public Connector[] Connectors => _connectors.Value;
    private readonly ObservableAsPropertyHelper<Connector[]> _connectors;
    public ReactiveCommand<Unit, Connector[]> LoadConnectors { get; }
    private readonly IReadOnlyList<Connector> _loadedConnectors;

    public EditPageViewModel()
    {
        (_loadedNodes, _loadedConnectors) = InitDiagramModel();
        
        LoadNodes = ReactiveCommand.CreateFromTask(LoadNodesAsync);
        _nodes = LoadNodes.ToProperty(this, x => x.Nodes, scheduler: RxApp.MainThreadScheduler);
        
        LoadConnectors = ReactiveCommand.CreateFromTask(LoadConnectorsAsync);
        _connectors = LoadConnectors.ToProperty(this, x => x.Connectors, scheduler: RxApp.MainThreadScheduler);
    }
    
    private Task<Node[]> LoadNodesAsync()
    {
        return Task.FromResult(_loadedNodes.ToArray());
    }    
    private Task<Connector[]> LoadConnectorsAsync()
    {
        return Task.FromResult(_loadedConnectors.ToArray());
    }
}