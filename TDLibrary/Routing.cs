using System.Collections.Generic;
using System.Xml;
using System.Web.Routing;

namespace TDLibrary
{
    public class Routing
    {
        public string RouteName { get; set; }
        public string RouteURL { get; set; }
        public string PhysicalFile { get; set; }

        public static List<Routing> RoutingNodes(string xmlFile)
        {
            List<Routing> _RoutingNodes = new List<Routing>();
            XmlReader oku = XmlReader.Create(xmlFile);

            while (oku.Read())
            {
                if (oku.NodeType == XmlNodeType.Element && oku.Name == "Route")
                {
                    _RoutingNodes.Add(new Routing()
                    {
                        RouteName = oku.GetAttribute("RouteName").ToString(),
                        RouteURL = oku.GetAttribute("RouteURL").ToString(),
                        PhysicalFile = oku.GetAttribute("PhysicalFile").ToString(),
                    });
                }
            }

            oku.Close();

            return _RoutingNodes;
        }

        public static void RegisterRoutes(List<Routing> _RoutingNodes)
        {
            RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");

            foreach (Routing item in _RoutingNodes)
            {
                RouteTable.Routes.MapPageRoute(item.RouteName, item.RouteURL, item.PhysicalFile);
            }
        }
    }
}
