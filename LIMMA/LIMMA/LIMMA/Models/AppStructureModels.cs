using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Interfaces;

namespace LIMMA.Models
{
    public class Tenant
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }

        public Node RootNode { get; set; }

        public Widget RootWidget { get; set; }
    }

    public class Node
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Position { get; set; }
        public string ParentNodeID { get; set; }
        public List<Node> Children { get; set; }
        public string PageID { get; set; }
        public TenantPage Page { get; set; }
        public List<NodeParameter> NodeParameters { get; set; }
        public NodeDataSources NodeDataSources { get; set; }

    }

    public class TenantPage
    {
        public string ID { get; set; }
        public string Label { get; set; }

        public Widget RootWidget { get; set; }

    }

    public class Widget
    {
        public string ID { get; set; }
        public string WidgetTypeID { get; set; }
        public WidgetModel Model { get; set; }
        public List<Widget> Children { get; set; }
        public List<Widget> HeaderChildren { get; set; }
        public List<Widget> FooterChildren { get; set; }
        public bool IsContentPadded { get; set; }
        public HeaderOrFooter Header { get; set; }
        public HeaderOrFooter Footer { get; set; }
        public string ParentID { get; set; }
    }

    public class HeaderOrFooter
    {
        public int Height { get; set; }
        public bool IsPadded { get; set; }
        public bool IsPinned { get; set; }
    }

    public class WidgetModel
    {
        public WidgetSettings WidgetSettings { get; set; }
    }

    public class WidgetSettings
    {
        public string Height { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundColor { get; set; }
        public string Pattern { get; set; }
        public string NumberFormat { get; set; }
        public string MarginTop { get; set; }
        public string PaddingLeft { get; set; }
        public string PaddingRight { get; set; }
        public int TextAlign { get; set; }
        public string Brand { get; set; }
        public string NavPaddingTop { get; set; }
        public bool KeepPageParameters { get; set; }
        public bool IsInverse { get; set; }
        public string StartingNodeGuid { get; set; }
        public int NavStyle { get; set; }
        public int Alignment { get; set; }
        public int Style { get; set; }
        public List<Column> Columns { get; set; }
        public string Keywords { get; set; }
        public CustomSettings CustomSettings { get; set; }
        public ContentSettings ContentSettings { get; set; }
        public NodeReference Children { get; set; }
        public NodeReference HeaderChildren { get; set; }
        public NodeReference FooterChildren { get; set; }
        public bool IsContentPadded { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public int BreaksBelow { get; set; }

    }

    public class Column
    {
        public int Offset { get; set; }
        public int Span { get; set; }
        public List<NodeReference> Content { get; set; }
    }

    public class ContentSettings
    {
        public string ID { get; set; }
    }

    public class CustomSettings
    {
        public string ID { get; set; }
        public Sources Sources { get; set; }
    }

    public class Sources
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public Value Value { get; set; }

        public List<Binding> Bindings { get; set; }

    }

    public class Binding
    {
        public string DataSource { get; set; }
        public string Expression { get; set; }
        public string Widget { get; set; }
        public string BindingTarget { get; set; }
    }

    public class Value
    {
        public int Snapshot { get; set; }
        public string Label { get; set; }
        public int Mode { get; set; }
        public string DeviceType { get; set; }
        public string DeviceID { get; set; }
        public int FilterType { get; set; }
        public string FilterAttributeID { get; set; }
        public string FilterAttributeDatasourceID { get; set; }
        public string FilterAttributeDatasourceExpression { get; set; }
        public string FilterParameterID { get; set; }
        public string ParameterID { get; set; }
    }

    public class NodeReference
    {
        public string ID { get; set; }
    }

    public class NodeParameter
    {
        public string Pusteblume { get; set; }
    }

    public class NodeDataSources
    {
        public List<IDataSource> Sources { get; set; }
        public List<NodeBindings> Bindings { get; set; }
    }
}
