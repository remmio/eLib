using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace eLib.Collections
{
    public class GraphWriter
    {
        //from http://stackoverflow.com/questions/8199600/c-sharp-directed-graph-generating-library

        public struct Graph
        {
            public Node[] Nodes;
            public Link[] Links;
        }

        public struct Node
        {
            [XmlAttribute]
            public string Id;
            [XmlAttribute]
            public string Label;

            public Node(string id, string label)
            {
                Id = id;
                Label = label;
            }
        }

        public struct Link
        {
            [XmlAttribute]
            public string Source;
            [XmlAttribute]
            public string Target;
            [XmlAttribute]
            public string Label;

            public Link(string source, string target, string label)
            {
                Source = source;
                Target = target;
                Label = label;
            }
        }

        public List<Node> Nodes { get; protected set; }
        public List<Link> Links { get; protected set; }

        public GraphWriter()
        {
            Nodes = new List<Node>();
            Links = new List<Link>();
        }

        public void AddNode(Node n) => Nodes.Add(n);

        public void AddLink(Link l) => Links.Add(l);

        public void Serialize(string xmlpath)
        {
            var g = new Graph
            {
                Nodes = Nodes.ToArray(),
                Links = Links.ToArray()
            };

            var root = new XmlRootAttribute("DirectedGraph") {Namespace = "http://schemas.microsoft.com/vs/2009/dgml"};
            var serializer = new XmlSerializer(typeof(Graph), root);
            var settings = new XmlWriterSettings {Indent = true};
            var xmlWriter = XmlWriter.Create(xmlpath, settings);
            serializer.Serialize(xmlWriter, g);
        }
    }
}
