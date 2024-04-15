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


    public Graph Graph => _graph;
    private Graph _graph = null!;
    public ReactiveCommand<ClickEventArgs, Item> AddItem { get; }
    
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
        _adpClient.ItemAdded
            .Watch()
            .Select(response => response.Data!.ItemAdded.ToLocal())
            .Subscribe(OnItemAdded);
        
        _logger = logger;
        (_loadedNodes, _loadedConnectors) = InitDiagramModel();
        
        AddItem = ReactiveCommand.CreateFromTask<ClickEventArgs, Item>(AddItemAsync);
        
        LoadNodes = ReactiveCommand.CreateFromTask(LoadNodesAsync);
        _nodes = LoadNodes.ToProperty(this, x => x.Nodes);
        
        LoadConnectors = ReactiveCommand.CreateFromTask(LoadConnectorsAsync);
        _connectors = LoadConnectors.ToProperty(this, x => x.Connectors);
    }

    private void OnItemAdded(Item item)
    {
        
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
    

    public async Task Initialize()
    {
        try
        {
            var result = await _adpClient.AddGraph.ExecuteAsync("Test graph");
            _graph = result.Data!.AddGraph.Graph!.ToLocal();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to load graph");
            throw;
        }
    }
}