using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Perception.TrainTurn
{
    public enum CellType
    {
        Empty,
        Path,
        Obstacle,
        Start,
        Finish,
    }

    [Serializable]
    public class GridCell
    {
        public Vector2Int position;
        public CellType cellType;

        public GridCell(int positionX, int positionY, CellType cellType)
        {
            this.position = new Vector2Int(positionX, positionY);
            this.cellType = cellType;
        }

        public override string ToString()
        {
            switch (cellType)
            {
                case CellType.Empty:
                    return "E";
                case CellType.Path:
                    return "P";
                case CellType.Obstacle:
                    return "O";
                case CellType.Start:
                    return "S";
                case CellType.Finish:
                    return "F";
            }
            return " ";
        }
    }

    [Serializable]
    public class Grid
    {
        public GridCell[,] grid;
        public Vector2Int currentPosition;
        public List<Vector2Int> visitedCells = new ();
        
        public Vector2Int startPosition;
        public Vector2Int finishPosition;

        private System.Random random = new (DateTime.Now.Millisecond);

        public void GenerateStartAndFinish()
        {
            int startPositionX = random.Next(0, 3);
            int finishPosionX = random.Next(0, 3);
            startPosition = new Vector2Int(startPositionX, 0); // Bottom
            finishPosition = new Vector2Int(finishPosionX, 2); // Top
            visitedCells.Add(startPosition);
            currentPosition = startPosition;
        }
        
        public void GenerateGrid()
        {
            visitedCells.Clear();
            GenerateStartAndFinish();
            grid = new GridCell[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    grid[x, y] = new GridCell(x, y, CellType.Empty);
                }
            }
            
            grid[startPosition.x, startPosition.y] = new GridCell(startPosition.x, startPosition.y, CellType.Start);
            grid[finishPosition.x, finishPosition.y] = new GridCell(finishPosition.x, finishPosition.y, CellType.Finish);
            
            GeneratePath();
        }

        public void GeneratePath()
        {
            bool isSuccess = false;
            while (!isSuccess)
            {
                ResetGrid();

                isSuccess = TryGeneratePath();
                if (!isSuccess)
                {
                    Debug.Log("Couldn't generate a valid path, trying again...");
                }
            }
            
            // Add obstacles
            FinishGrid();
        }

        public bool TryGeneratePath()
        {
            Vector2Int currentPos = startPosition;
            while (currentPos != finishPosition)
            {
                List<Vector2Int> neighbors = GetNeighbors(currentPos, visitedCells);
                if (neighbors.Count == 0)
                {
                    Debug.Log($"Reached a dead end with no neighbors at ({currentPos.x}, {currentPos.y}), generating a new grid");
                    return false;
                }
                
                Vector2Int nextPosition = neighbors[random.Next(0, neighbors.Count)];
                visitedCells.Add(nextPosition);
                
                if (nextPosition != finishPosition)
                {
                    grid[nextPosition.x, nextPosition.y] = new GridCell(nextPosition.x, nextPosition.y, CellType.Path);
                }
                
                string direction = "";
                if (nextPosition.x < currentPos.x)
                {
                    direction += "Left ";
                }
                else if (nextPosition.x > currentPos.x)
                {
                    direction += "Right ";
                }
                else if (nextPosition.y > currentPos.y)
                {
                    direction += "Up ";
                }
                else if (nextPosition.y < currentPos.y)
                {
                    direction += "Down ";
                }
                Debug.Log($"Path constructed {direction} from ({currentPos.x}, {currentPos.y}) -> ({nextPosition.x}, {nextPosition.y})");
                
                currentPos = nextPosition;
            }
            
            return true;
        }

        public List<Vector2Int> GetNeighbors(Vector2Int position, List<Vector2Int> visitedCellsList)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();
            Vector2Int upPosition = position + Vector2Int.up;
            if (!visitedCellsList.Contains(upPosition) && IsPositionInGrid(upPosition))
            {
                neighbors.Add(upPosition);
            }

            Vector2Int leftPosition = position + Vector2Int.left;
            if (!visitedCellsList.Contains(leftPosition) && IsPositionInGrid(leftPosition))
            {
                neighbors.Add(leftPosition);
            }
            
            Vector2Int rightPosition = position + Vector2Int.right;
            if (!visitedCellsList.Contains(rightPosition) && IsPositionInGrid(rightPosition))
            {
                neighbors.Add(rightPosition);
            }
            
            Vector2Int downPosition = position + Vector2Int.down;
            if (!visitedCellsList.Contains(downPosition) && IsPositionInGrid(downPosition))
            {
                neighbors.Add(downPosition);
            }
            
            return neighbors;
        }

        public bool IsPositionInGrid(Vector2Int position)
        {
            return position.x >= 0 && position.x < 3 && position.y >= 0 && position.y < 3;
        }

        public void FinishGrid()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (grid[x, y].cellType == CellType.Empty)
                    {
                        grid[x, y].cellType = CellType.Obstacle;
                    }
                }
            }
        }

        public void PrintGrid()
        {
            string result = "";
            for (int y = 2; y >= 0; y--)
            {
                for (int x = 0; x < 3; x++)
                {
                    result += grid[x, y];
                }
                result += "\n";
            }
            Debug.Log("Got grid : \n" + result);
        }
        
        public void ResetGrid()
        {
            visitedCells.Clear();
            visitedCells.Add(startPosition);
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    grid[x, y].cellType = CellType.Empty;
                }
            }

            grid[startPosition.x, startPosition.y].cellType = CellType.Start;
            grid[finishPosition.x, finishPosition.y].cellType = CellType.Finish;
        }
    }
}