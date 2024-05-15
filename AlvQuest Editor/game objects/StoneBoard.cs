using System;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;

namespace AlvQuest_Editor
{
    public class StoneGridData
    {
        public List<(EStoneType, int)> OnFieldCombinations { get; } = [];
        public Dictionary<EStoneType, int> AmountOfCombinedStones { get; } = [];
        public Dictionary<(int x, int y), EStoneType> OnFieldCombinationsStones { get; } = [];

        public void ResetData()
        {
            OnFieldCombinations.Clear();
            OnFieldCombinationsStones.Clear();
            AmountOfCombinedStones.Clear();
        }
        public void AddStone(int x, int y, EStoneType stoneType)
        {
            if (OnFieldCombinationsStones.TryAdd((x, y), stoneType))
            {

            }
        }
    }
    public class StoneBoard  
    {
        //
        StoneGridData StoneGridData { get; set; }
        //
        Arena Arena { get; set; }
        //
        private Random _random = new();
        //
        private const int _gridSize = 8;
        //
        public EStoneType[,] StoneGrid { get; } = new EStoneType[_gridSize, _gridSize];
        //
        private Point _firstPosition;
        //
        private Point _secondPosition;

        /// <summary>
        /// 
        /// </summary>
        public void InitializeGridArray()
        {
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    StoneGrid[i, j] = SetNewStone(i, j);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private EStoneType SetNewStone(int i, int j)
        {
            var newStone  = GetRandomStone();
            while ((j > 1 && newStone == StoneGrid[i, j - 1] && StoneGrid[i, j] == StoneGrid[i, j - 2]) ||
                   (i > 1 && newStone == StoneGrid[i - 1, j] && StoneGrid[i, j] == StoneGrid[i - 2, j]))
            {
                newStone = GetRandomStone();
            }
            return newStone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private EStoneType GetRandomStone()
        {
            return (EStoneType)_random.Next(1, 8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void StoneClick(int x, int y)//Эта функция вызывается из фронта при клике на камень, с неё всё начинается
        {
            var newPoint = new Point(x, y);
            if (_firstPosition.X == -1)
            {
                _firstPosition = newPoint;
            }
            else if (newPoint != _firstPosition)
            {
                var distance =
                    Math.Sqrt(Math.Pow(_firstPosition.X - newPoint.X, 2) + Math.Pow(_firstPosition.Y - newPoint.Y, 2));
                if (distance == 1)
                {
                    _secondPosition = newPoint;
                    SwapStones();
                    UpdateStoneGrid();
                }
                



                for (int i = 0; i < 4; i++)
                    if ((position.Column + _offsetX[i] == _previousStonePosition.Column) && (position.Item2 + _offsetY[i] == _previousStonePosition.Row))
                    {
                        previousStoneIsNearby = true;
                    }
                if (previousStoneIsNearby)
                {
                    SwapStones(position.Column, position.Row, _previousStonePosition.Column, _previousStonePosition.Row);
                    _previousStonePosition.Column = -1; _previousStonePosition.Row = -1;
                    GetTurnSwitch().Switcher = true;
                    while (ScanFieldForPossibleCombinations())
                    {
                        ScanFieldAndCombine();
                        Combine();
                        RemoveStones(_dictionaryOfRemovedStones);
                    }
                    _dictionaryOfRemovedStones.Clear();
                    _amountOfCombinedStones.Clear();
                    _combinationType.Clear();
                }
                else
                {
                    _previousStonePosition = position;
                }
            }
        }
        public void SwapStones()
        {
            var x1 = _firstPosition.X;
            var y1 = _firstPosition.Y;
            var x2 = _secondPosition.X;
            var y2 = _secondPosition.Y;
            (StoneGrid[x1, y1], StoneGrid[x2, y2]) = (StoneGrid[x2, y2], StoneGrid[x1, y1]);
            ResetPoints();
        }
        public void ResetPoints()
        {
            _firstPosition = new Point(-1, -1);
            _secondPosition = new Point(-1, -1);
        }
        public void UpdateStoneGrid()
        {
            while (TryFindStoneMatches())
            {
                Arena.StoneCombination(StoneGridData);
                DestroyCombinedStones();

            }
        }
        public bool TryFindStoneMatches()
        {
            StoneGridData.ResetData();
            bool IsAnyMatches = false;
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    if (StoneGridData.OnFieldCombinationsStones.ContainsKey((i, j)))
                    {
                        if (TryMarkHorizontalMatch(i, j) || TryMarkVerticalMatch(i, j))
                        {
                            IsAnyMatches = true;
                        }
                    }
                }
            }
            return IsAnyMatches;
        }
        public bool TryMarkHorizontalMatch(int x, int y)
        {
            var value = StoneGrid[x, y];
            int offsetX;
            int leftBorderX = x;
            int rightBorderX = x;

            // Check left
            offsetX = x - 1;
            while (offsetX >= 0 && StoneGrid[offsetX, y] == value)
            {
                leftBorderX = offsetX;
                offsetX--;
            }
            // Check right
            offsetX = x + 1;
            while (offsetX < _gridSize && StoneGrid[offsetX, y] == value)
            {
                rightBorderX = offsetX;
                offsetX++;
            }

            var combinationLenth = rightBorderX - leftBorderX + 1;
            if (rightBorderX - leftBorderX > 2)
            {
                StoneGridData.OnFieldCombinations.Add((value, combinationLenth));
                for (int i = leftBorderX; i < rightBorderX + 1; i++)
                {
                    StoneGridData.OnFieldCombinationsStones.TryAdd((i, y), value);
                }
                return true;
            }
            return false;
        }
        public bool TryMarkVerticalMatch(int x, int y)
        {
            var value = StoneGrid[x, y];
            int offsetY;
            int leftBorderY = y;
            int rightBorderY = y;

            // Check up
            offsetY = y - 1;
            while (offsetY >= 0 && StoneGrid[x, offsetY] == value)
            {
                leftBorderY = offsetY;
                offsetY--;
            }
            // Check down
            offsetY = y + 1;
            while (offsetY < _gridSize && StoneGrid[x, offsetY] == value)
            {
                rightBorderY = offsetY;
                offsetY++;
            }

            var combinationLenth = rightBorderY - leftBorderY + 1;
            if (rightBorderY - leftBorderY > 2)
            {
                StoneGridData.OnFieldCombinations.Add((value, combinationLenth));
                for (int i = leftBorderY; i < rightBorderY + 1; i++)
                {
                    StoneGridData.OnFieldCombinationsStones.TryAdd((x, i), value);
                }
                return true;
            }
            return false;
        }
        public void DestroyCombinedStones()
        {
            foreach (var (x, y) in StoneGridData.OnFieldCombinationsStones.Keys)
            {
                StoneGrid[x,y] = EStoneType.None;
            }
        }
        public void StonesFreeFall()
        {
            for (int i = 0; i < StoneGrid.Length; i++)
            {
                EStoneType[] currentcolumn = new EStoneType[8];
                for (int j = 0; j < StoneGrid.Length; j++)
                {

                }
            }
        }
    }
}
