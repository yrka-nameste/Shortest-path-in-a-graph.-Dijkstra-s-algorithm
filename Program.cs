using System;

class DijkstraAlgorithm
{
 // Входные данные, ввод и вывод
    static void Main()
    {
        Console.Write("Введите количество вершин графа: ");
        int verticesCount = int.Parse(Console.ReadLine());

        Console.Write("Введите начальную вершину отсчета: ");
        int startVertex = int.Parse(Console.ReadLine());

        int[,] graph = GenerateRandomGraph(verticesCount);

        Console.WriteLine("Веса ребер графа:");
        PrintGraph(graph);

        int[] shortestDistances = Dijkstra(graph, startVertex);

        Console.WriteLine("\nКратчайшие расстояния от вершины " + startVertex + " до остальных вершин:");
        PrintDistances(shortestDistances);
    }
    // Генерация матрицы смежности
    static int[,] GenerateRandomGraph(int verticesCount)
    {
        Random random = new Random();
        int[,] graph = new int[verticesCount, verticesCount];

        for (int i = 0; i < verticesCount; i++)
        {


            for (int j = 0; j < verticesCount; j++)
            {
                if (i == j) { graph[i, j] = 0; }
                else
                {
                    int weight = random.Next(1, 10);
                    graph[i, j] = weight;
                    graph[j, i] = weight;
                }
            }
        }

        return graph;
    }
    // Вывод матрицы
    static void PrintGraph(int[,] graph)
    {
        int verticesCount = graph.GetLength(0);

        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = 0; j < verticesCount; j++)
            {
                Console.Write(graph[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static int[] Dijkstra(int[,] graph, int startVertex)
    { // Инициализация массивов для хранения кратчайших растояний и посещенных вершин
        int verticesCount = graph.GetLength(0);
        int[] shortestDistances = new int[verticesCount];
        bool[] visited = new bool[verticesCount];
        //Установление начальные значения 
        for (int i = 0; i < verticesCount; i++)
        {
            shortestDistances[i] = int.MaxValue;
            visited[i] = false;
        }
        // Установление до стартовой вершин устанавливается в 0
        shortestDistances[startVertex] = 0;
        
        for (int count = 0; count < verticesCount - 1; count++)// обработка каждой вершины кроме стартовой
        {
            int minDistance = MinDistance(shortestDistances, visited);// ищем минимальную дистанцию 
            visited[minDistance] = true;// говорим что вершина посещена 

            for (int v = 0; v < verticesCount; v++)
            {
                if (!visited[v] && graph[minDistance, v] != 0 &&
                    shortestDistances[minDistance] != int.MaxValue &&
                    shortestDistances[minDistance] + graph[minDistance, v] < shortestDistances[v])
                {
                    shortestDistances[v] = shortestDistances[minDistance] + graph[minDistance, v];
                }
            }// проверяем посещена ли вершина, растояние до минимальной дистанции не является бесконечноесть 
            // если сумма расстояния от ночальной вершины до текущей через минимальнуюдистанцию то эта сумма стоновится кротчайшим путем 
        }
        // Возвращение массива с кротчайщим расстоянием 
        return shortestDistances;
    }

    static int MinDistance(int[] distances, bool[] visited)
    {
        int min = int.MaxValue, minIndex = -1;// говорим что минимальное значение является мксимальным, а минимальный индекс явлеяется -1 т.к он не может быть индексом вершинины в реальном графе 
        int verticesCount = distances.Length;

        for (int v = 0; v < verticesCount; v++)
        {
            if (!visited[v] && distances[v] <= min)// проверка посщенности вершины  и расстояние до текущей вершины меньше или равно текущему 
            {
                min = distances[v];// обновление минимальности 
                minIndex = v;// номер текущей вершины
            }
        }

        return minIndex;
    }

    static void PrintDistances(int[] distances)
    {
        int verticesCount = distances.Length;

        for (int i = 0; i < verticesCount; i++)
        {
            if (distances[i] == int.MaxValue)
            {
                Console.WriteLine($"Вершина {i} не принадлежит графу ");
            }
            else
                Console.WriteLine("До вершины " + i + ": " + distances[i]);
        }
    }
}
