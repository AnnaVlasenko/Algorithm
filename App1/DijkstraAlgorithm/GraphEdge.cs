using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Dijkstra_Algorithm
{
    public class GraphEdge
    {
        /// <summary>
        /// Связанная вершина
        /// </summary>
        public GraphNode ConnectedNodes { get; }

        /// <summary>
        /// Вес ребра
        /// </summary>
        public int EdgeWeight { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="node">Связанная вершина</param>
        /// <param name="weight">Вес ребра</param>
        public GraphEdge(GraphNode node, int weight)
        {
            ConnectedNodes = node;
            EdgeWeight = weight;
        }
    }
}
