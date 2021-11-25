using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsVizualizator.Dijkstra_Algorithm
{
    public class DijkstraAlgorithmClass
    {
        Graph graph;

        List<NodeInfo> infos;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="graph">Граф</param>
        public DijkstraAlgorithmClass(Graph graph)
        {
            this.graph = graph;
        }

        /// <summary>
        /// Инициализация информации
        /// </summary>
        void InitInfo()
        {
            infos = new List<NodeInfo>();
            foreach (var v in graph.Nodes)
            {
                infos.Add(new NodeInfo(v));
            }
        }

        /// <summary>
        /// Получение информации о вершине графа
        /// </summary>
        /// <param name="v">Вершина</param>
        /// <returns>Информация о вершине</returns>
        NodeInfo GetVertexInfo(GraphNode v)
        {
            foreach (var i in infos)
            {
                if (i.Node.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Поиск непосещенной вершины с минимальным значением суммы
        /// </summary>
        /// <returns>Информация о вершине</returns>
        public NodeInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            NodeInfo minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        /// <summary>
        /// Поиск кратчайшего пути по названиям вершин
        /// </summary>
        /// <param name="startName">Название стартовой вершины</param>
        /// <param name="finishName">Название финишной вершины</param>
        /// <returns>Кратчайший путь</returns>
        public string FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(graph.FindNode(startName), graph.FindNode(finishName));
        }

        /// <summary>
        /// Поиск кратчайшего пути по вершинам
        /// </summary>
        /// <param name="startVertex">Стартовая вершина</param>
        /// <param name="finishVertex">Финишная вершина</param>
        /// <returns>Кратчайший путь</returns>
        public string FindShortestPath(GraphNode startVertex, GraphNode finishVertex)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }

        /// <summary>
        /// Вычисление суммы весов ребер для следующей вершины
        /// </summary>
        /// <param name="info">Информация о текущей вершине</param>
        void SetSumToNextVertex(NodeInfo info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Node.Edges)
            {
                var nextInfo = GetVertexInfo(e.ConnectedNodes);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousNode = info.Node;
                }
            }
        }

        /// <summary>
        /// Формирование пути
        /// </summary>
        /// <param name="startVertex">Начальная вершина</param>
        /// <param name="endVertex">Конечная вершина</param>
        /// <returns>Путь</returns>
        string GetPath(GraphNode startVertex, GraphNode endVertex)
        {
            var path = endVertex.ToString();
            while (startVertex != endVertex)
            {
                endVertex = GetVertexInfo(endVertex).PreviousNode;
                path = endVertex.ToString() + path;
            }

            return path;
        }
    }
}
