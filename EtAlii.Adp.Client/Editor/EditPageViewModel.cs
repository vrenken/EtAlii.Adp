using System.Numerics;
using System.Reactive;
using ReactiveUI;
using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client;

public partial class EditPageViewModel : ReactiveObject
{
    private readonly IAdpClient _adpClient;
    private readonly ILogger<EditPageViewModel> _logger;

    //private Graph
        
    /// <summary>
    /// Defines Diagram's nodes collection.
    /// </summary>
    public Node[] Nodes => _nodes.Value;
    private readonly ObservableAsPropertyHelper<Node[]> _nodes;


    public Graph Graph => _graph.Value;
    private readonly ObservableAsPropertyHelper<Graph> _graph;

    public ReactiveCommand<ClickEventArgs, Item> AddItem { get; }

    public ReactiveCommand<Unit, Graph> Load { get; }

    public ReactiveCommand<Unit, Node[]> LoadNodes { get; }
    private readonly IReadOnlyList<Node> _loadedNodes;
    public Connector[] Connectors => _connectors.Value;
    private readonly ObservableAsPropertyHelper<Connector[]> _connectors;
    public ReactiveCommand<Unit, Connector[]> LoadConnectors { get; }

    private readonly IReadOnlyList<Connector> _loadedConnectors;
    
    public Vector2 ViewCenter { get; set; }
    public float Zoom { get; set; }
    public Vector2 ViewPort { get; set; }

    public EditPageViewModel(IAdpClient adpClient, ILogger<EditPageViewModel> logger)
    {
        _adpClient = adpClient;
        _logger = logger;
        (_loadedNodes, _loadedConnectors) = InitDiagramModel();

        Load = ReactiveCommand.CreateFromTask(LoadAsync);
        _graph = Load.ToProperty(this, x => x.Graph, scheduler: RxApp.MainThreadScheduler);

        AddItem = ReactiveCommand.CreateFromTask<ClickEventArgs, Item>(AddItemAsync);
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

    private async Task<Item> AddItemAsync(ClickEventArgs e)
    {
        try
        {
            var result = await _adpClient.AddItem.ExecuteAsync(Graph.Id.ToString(), "New item", (float)e.Position.X, (float)e.Position.Y);
            return result.Data!.AddItem.Item!.ToLocal();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to add item");
            throw;
        }
    }
    

    private async Task<Graph> LoadAsync()
    {
        try
        {
            var result = await _adpClient.AddGraph.ExecuteAsync("Test graph");
            return result.Data!.AddGraph.Graph!.ToLocal();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to load graph");
            throw;
        }
    }
}