void Main()
{
	Init();
	//map.Dump();

	bool[,] visited = new bool[rowCount, colCount];

	List<List<(int, int)>> groups = new List<List<(int, int)>>();

	for (int row = 0; row < rowCount; row++)
	{
		for (int col = 0; col < colCount; col++)
		{
			if (!visited[row, col])
			{
				List<(int, int)> currentGroup = new List<(int, int)>();
				DFS(map, row, col, visited, currentGroup, map[row][col]);
				if (currentGroup.Count > 0)
					groups.Add(currentGroup);
			}
		}
	}

	int cost = 0;
	foreach (List<(int, int)> group in groups)
	{
		var bounds = CalculateBounds(group);
		
		//var sides = CalculateSides(group);
		
		cost += group.Count * bounds;
		
		
	}

	cost.Dump();


}


int CalculateBounds(List<(int, int)> group)
{
	int bounds = 0;


	foreach ((int, int) item in group)
	{
		if (!group.Any(i => i.Item1 == item.Item1 + 1 && i.Item2 == item.Item2))
		{
			bounds++;
		}
		if (!group.Any(i => i.Item1 == item.Item1 - 1 && i.Item2 == item.Item2))
		{
			bounds++;
		}
		if (!group.Any(i => i.Item1 == item.Item1 && i.Item2 == item.Item2 + 1))
		{
			bounds++;
		}
		if (!group.Any(i => i.Item1 == item.Item1 && i.Item2 == item.Item2 - 1))
		{
			bounds++;
		}
	}
	return bounds;
}


static void DFS(char[][] grid, int row, int col, bool[,] visited, List<(int, int)> currentGroup, char targetChar)
{
	int rows = grid.Length;
	int cols = grid[0].Length;


	if (row < 0 || row >= rows || col < 0 || col >= cols)
		return;

	if (visited[row, col] || grid[row][col] != targetChar)
		return;

	// Mark this cell as visited
	visited[row, col] = true;

	// Add this position to the current group
	currentGroup.Add((row, col));

	// Visit all 4 adjacent neighbors (up, down, left, right)
	for (int i = 0; i < 4; i++)
	{
		int newRow = row + dRow[i];
		int newCol = col + dCol[i];
		DFS(grid, newRow, newCol, visited, currentGroup, targetChar);
	}
}

static readonly int[] dRow = { -1, 1, 0, 0 };
static readonly int[] dCol = { 0, 0, -1, 1 };


void Init()
{
	string[] lines = File.ReadAllLines(@"C:\temp\input2.txt");
	rowCount = lines.Length;
	colCount = lines[0].Length;
	map = new char[rowCount][];
	for (int i = 0; i < rowCount; i++)
	{
		map[i] = lines[i].ToCharArray();
	}

}

char[][] map;
int rowCount;
int colCount;

