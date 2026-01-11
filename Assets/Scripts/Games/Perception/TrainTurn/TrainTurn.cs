using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.Perception.TrainTurn
{
    public class TrainTurn : MonoBehaviour
    {
        [Header("Grid")]
        public Grid grid = new ();
        public int movements;

        [Header("Grid Cells")] 
        public GameObject gridParent; // The parent of the cells to be spawned
        public List<GameObject> gridCells = new ();
        public GameObject emptyCellPrefab;
        public GameObject obstacleCellPrefab;

        [Header("Cell Positions")] 
        public float[] cellPositionsX = new float[3];
        public float[] cellPositionsY = new float[3];
        
        private void Start()
        {
            GenerateGrid();
        }

        public void GenerateGrid()
        {
            foreach (GameObject cell in gridCells)
            {
                Destroy(cell);
            }
            gridCells.Clear();
            
            grid.GenerateGrid();
            grid.PrintGrid();
            movements = 0;
            ShowGrid();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                GenerateGrid();
            }
        }

        public void ShowGrid()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    ShowGridCell(grid.grid[x, y], x, y);
                }
            }
        }

        public void ShowGridCell(GridCell gridCell, int x, int y)
        {
            GameObject newGridCell;
            switch (gridCell.cellType)
            {
                case CellType.Obstacle:
                    newGridCell = Instantiate(obstacleCellPrefab, new Vector2(cellPositionsX[x], cellPositionsY[y]), Quaternion.identity, gridParent.transform);
                    break;
                default:
                    newGridCell = Instantiate(emptyCellPrefab, new Vector2(cellPositionsX[x], cellPositionsY[y]), Quaternion.identity, gridParent.transform);
                    break;
            }
            gridCells.Add(newGridCell);
        }

        public void OnLeftButtonClicked()
        {
            Vector2Int nextPosition = grid.currentPosition + Vector2Int.left;
            CheckForCorrectMovement(nextPosition);
        }

        public void OnForwardButtonClicked()
        {
            Vector2Int nextPosition = grid.currentPosition + Vector2Int.up;
            CheckForCorrectMovement(nextPosition);
        }

        public void OnRightButtonClicked()
        {
            Vector2Int nextPosition = grid.currentPosition + Vector2Int.right;
            CheckForCorrectMovement(nextPosition);
        }

        public void CheckForCorrectMovement(Vector2Int nextPosition)
        {
            movements++;
            if (grid.visitedCells[movements] != nextPosition)
            {
                Lose();
                return;
            }
            grid.currentPosition = nextPosition;
            if (grid.currentPosition == grid.finishPosition)
            {
                Win();
            }
        }

        public void Win()
        {
            GameManager.Win();
            RestartLevel();
        }

        public void Lose()
        {
            GameManager.Lose();
            RestartLevel();
        }

        public void RestartLevel()
        {
            GenerateGrid();
        }
    }
}