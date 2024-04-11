﻿using System.Reactive.Disposables;
using System.Reactive.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ReactiveUI;
using Syncfusion.Blazor.Diagram;

namespace EtAlii.Adp.Client;

public partial class Edit
{
    private SfDiagramComponent _diagram = null!;

    private readonly DiagramObjectCollection<Node> _nodes = new();
    /// <summary>
    /// Defines Diagram's connectors collection.
    /// </summary>
    private readonly DiagramObjectCollection<Connector> _connectors = new();

    [Inject] public new EditPageViewModel ViewModel { get => base.ViewModel!; set => base.ViewModel = value; }

    protected override async Task OnInitializedAsync()
    {
        this.WhenActivated(disposableRegistration =>
        {
            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Nodes,
                    view => view._nodes)
                .DisposeWith(disposableRegistration);
            
            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Connectors,
                    view => view._connectors)
                .DisposeWith(disposableRegistration);
        });

        await ViewModel.LoadNodes.Execute().ToTask();
        await ViewModel.LoadConnectors.Execute().ToTask();

        // if (ViewModel.Nodes.Any())
        // {
        //     _nodes.AddRange(ViewModel.Nodes);
        // }
        //
        // if (ViewModel.Connectors.Any())
        // {
        //     _connectors.AddRange(ViewModel.Connectors);
        // }
    }

    private void OnClicked(ClickEventArgs e)
    {
        // if (_addActionButton.IsToggled)
        // {
        //     var startItem = new Item { Id = Guid.NewGuid(), Position = new((float)e.Position.X, (float)e.Position.Y), Size = new(145, 60), Label = "New action" };
        //     var node = NodeFactory.Create(startItem);
        //     _nodes.Add(node);
        // }
        
        
        // if (e.Element == null)
        // {
        //     _diagramTool = DiagramInteractions.ZoomPan;
        //     //StateHasChanged();
        // }
        // else if (e.Element != null)
        // {
        //     _diagramTool = DiagramInteractions.Default;
        //     //StateHasChanged();
        // }
    }
}