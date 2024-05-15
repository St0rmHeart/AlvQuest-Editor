namespace AlvQuest_Editor
{
    public class Arena
    {
        #region _____________________ПОЛЯ_____________________

        //
        private readonly CharacterSlot _player;
        //
        private readonly CharacterSlot _enemy;
        //
        private CharacterSlot _activePlayer;
        //
        private CharacterSlot _passivePlayer;
        
        //
        private readonly TurnSwitch _turnSwitchModule = new();
        //
        private readonly DamageModule _damageModule = new();
        //
        private readonly Random _randomModule = new();

        //
        private const int _gridSize = 8;
        //
        public EStoneType[,] StoneGridArray { get; } = new EStoneType[_gridSize, _gridSize];
        //
        private Dictionary<(int, int), EStoneType> _dictionaryOfRemovedStones = new Dictionary<(int, int), EStoneType>();//пункт 1.1
        //
        private List<(EStoneType, int)> _combinationType = new List<(EStoneType, int)>();
        //
        private Dictionary<EStoneType, int> _amountOfCombinedStones = new Dictionary<EStoneType, int>();
        //
        private (int Column, int Row) _previousStonePosition = (-1, -1);
        //
        private readonly int[] _offsetX = { -1, 0, 0, 1 };
        private readonly int[] _offsetY = { 0, 1, -1, 0 };
        #endregion

        #region ______________________КОНСТРУКТОР______________________

        /// <summary>
        /// Конструктор Арены, где устанавливается персонаж игрока и персонаж противника,
        /// а так же призводится и полная предварительная настройка и подготовка к сражению
        /// </summary>
        /// <param name="player">Персонаж игрока</param>
        /// <param name="enemy"> персонаж противника</param>
        public Arena(Character player, Character enemy)
        {
            //Установка персонажей игрока противника-компьютера
            _player = new CharacterSlot(player);
            _enemy = new CharacterSlot(enemy);

            //проброска ссылок между всеми объектами
            CharacterSlot[] initArray = new CharacterSlot[2] { _player, _enemy };
            for (int i = 0; i < initArray.Length; i++)
            {
                var pointer1 = initArray[i];
                var pointer2 = initArray[(i + 1) % 2];

                //установка ссылки на переключателя хода
                pointer1.TurnSwitcherModule = _turnSwitchModule;
                //установка ссылки на модуль урона
                pointer1.DamageModule = _damageModule;
                //установка ссылки на оппонента
                pointer1.CurrentOpponent = pointer2;

                //инициализация всех эффектов в снаряжении персонажа
                foreach (Equipment item in pointer1.Character.Equipment.Values)
                {
                    foreach (IEffect effect in item.Effects)
                    {
                        effect.Installation(pointer1, pointer2);
                    }
                }
            }
            foreach (CharacterSlot characterSlot in initArray)
            {
                foreach (ECharacteristic characteristic in CONSTANT.CHAR_DER_PAIRS.Keys)
                {
                    characterSlot.Data[characteristic][EDerivative.Value].SetFinalValue();
                }
                foreach (ECharacteristic characteristic in CONSTANT.CHAR_DER_PAIRS.Keys)
                {
                    foreach (EDerivative derivative in CONSTANT.CHAR_DER_PAIRS[characteristic])
                    {
                        (characterSlot.Data[characteristic][derivative] as CommonParameter)?.UpdateA0();
                    }
                }
                (characterSlot.Data[ECharacteristic.Endurance][EDerivative.CurrentHealth] as CurrentCommonParameter).CurrentValue =
                    characterSlot.Data[ECharacteristic.Endurance][EDerivative.MaxHealth].FinalValue; ;
            }

            //У кого из персонажей больше ловкость - тот и начинает ход первым
            var playerDexterity = _player.Data[ECharacteristic.Dexterity][EDerivative.Value].FinalValue;
            var enemyDexterity = _enemy.Data[ECharacteristic.Dexterity][EDerivative.Value].FinalValue;
            if (playerDexterity >= enemyDexterity)
            { _activePlayer = _player; _passivePlayer = _enemy; }
            else
            { _activePlayer = _enemy; _passivePlayer = _player; }
            //инициализация эффектов у обоих персонажей
        }
        #endregion

        #region _____________________МЕТОДЫ_____________________
        /// <summary>
        /// Пока реализует совмещение активным игроком какой-либо комбинации камней, на игровой доске.
        /// </summary>
        /// <param name="stoneType">Тип камня.</param>
        /// <param name="amount">Количество.</param>
        public void StoneCombination(EStoneType stoneType, int amount)
        {
            _activePlayer.StoneCombination(stoneType, amount);
            //CompleteStep();
        }
        public void StoneCombination(StoneGridData stoneBoardData)
        {

        }

        public void CompleteStep()
        {
            if (_turnSwitchModule.Switcher)
            {
                _activePlayer.CompleteStep();
                (_passivePlayer, _activePlayer) = (_activePlayer, _passivePlayer);
            }
        }
        public TurnSwitch GetTurnSwitch()
        {
            return _turnSwitchModule;
        }

        public void InitializeGridArray()
        {
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    StoneGridArray[i, j] = RandomStone();
                }
            }
            // Проверка и корректировка значений
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    // Проверка по горизонтали
                    while (j > 1 && StoneGridArray[i, j] == StoneGridArray[i, j - 1] && StoneGridArray[i, j] == StoneGridArray[i, j - 2])
                    {
                        StoneGridArray[i, j] = RandomStone();
                    }

                    // Проверка по вертикали
                    while (i > 1 && StoneGridArray[i, j] == StoneGridArray[i - 1, j] && StoneGridArray[i, j] == StoneGridArray[i - 2, j])
                    {
                        StoneGridArray[i, j] = RandomStone();
                    }
                }
            }
        }

        public void StoneClick(int x, int y)//Эта функция вызывается из фронта при клике на камень, с неё всё начинается
        {
            (int Column, int Row) position = (x, y);
            bool previousStoneIsNearby = false;
            if (_previousStonePosition.Column == -1)
            {
                _previousStonePosition = position;
            }
            else
            {
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

        public EStoneType RandomStone()
        {
            return (EStoneType)_randomModule.Next(1, 8);
        }

        public void SwapStones(int x1, int y1, int x2, int y2)
        {
            (StoneGridArray[x2, y2], StoneGridArray[x1, y1]) = (StoneGridArray[x1, y1], StoneGridArray[x2, y2]);
        }

        public void RemoveStones(Dictionary<(int, int), EStoneType> dictionaryOfRemovedStones)
        {
            var RemovedStones = new List<(int Column, int Row)>();
            foreach (KeyValuePair<(int, int), EStoneType> s in dictionaryOfRemovedStones)
            {
                RemovedStones.Add(s.Key);
            }
            (int, int) tmp;
            for (int i = 1; i < RemovedStones.Count(); i++)
            {
                if (RemovedStones[i - 1].Row > RemovedStones[i].Row)
                {
                    tmp = RemovedStones[i - 1];
                    RemovedStones[i - 1] = RemovedStones[i];
                    RemovedStones[i] = tmp;
                    i--;
                }
            }
            foreach ((int, int) stone in RemovedStones)
            {
                StoneFall(stone.Item1, stone.Item2);
            }
        }

        public void StoneFall(int x, int y)
        {
            for (int i = y; i > 0; i--)
            {
                StoneGridArray[x, i] = StoneGridArray[x, i - 1];
            }
            StoneGridArray[x, 0] = RandomStone();
        }

        public void ScanFieldAndCombine()
        {
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    CheckForCombination(i, j);
                }
            }
        }
        public void CheckForCombination(int x, int y)
        {
            EStoneType InitStoneType = StoneGridArray[x, y];
            int CombinationSizeX = 0;
            int CombinationSizeY = 0;
            var DORSHorizontal = new Dictionary<(int, int), EStoneType>();//DictionaryOfRemovedStones но локальный, для комбинаций по горизонтали
            var DORSVertical = new Dictionary<(int, int), EStoneType>();//DictionaryOfRemovedStones но локальный, для комбинаций по вертикали
            var CT = new List<(EStoneType, int)>();//CombinationType но локальный
            var AOCS = new Dictionary<EStoneType, int>();//AmountOfCombinedStones но локальный
            for (int i = Math.Max(x - 1, 0); i > 0; i--)//комбинации по горизонтали
            {
                if (StoneGridArray[i, y] == InitStoneType)
                {
                    CombinationSizeX++;
                    if (!DORSHorizontal.ContainsKey((i, y)))
                        DORSHorizontal.Add((i, y), InitStoneType);
                }
                else
                {
                    break;
                }
            }
            for (int i = x; i < _gridSize; i++)
            {
                if (StoneGridArray[i, y] == InitStoneType)
                {
                    CombinationSizeX++;
                    if (!DORSHorizontal.ContainsKey((i, y)))
                        DORSHorizontal.Add((i, y), InitStoneType);
                }
                else
                {
                    break;
                }
            }

            for (int i = y; i < _gridSize; i++)//комбинации по вертикали
            {
                if (StoneGridArray[x, i] == InitStoneType)
                {
                    CombinationSizeY++;
                    if (!DORSVertical.ContainsKey((x, i)))
                        DORSVertical.Add((x, i), InitStoneType);
                }
                else
                {
                    break;
                }
            }
            for (int i = Math.Max(y - 1, 0); i > 0; i--)
            {
                if (StoneGridArray[x, i] == InitStoneType)
                {
                    CombinationSizeY++;
                    if (!DORSVertical.ContainsKey((x, i)))
                        DORSVertical.Add((x, i), InitStoneType);
                }
                else
                {
                    break;
                }
            }

            if (CombinationSizeX >= 3)
            {
                CT.Add((InitStoneType, DORSHorizontal.Count()));//Записываем тип комбинации
                foreach (KeyValuePair<(int, int), EStoneType> c in DORSHorizontal)
                {
                    if (!_dictionaryOfRemovedStones.ContainsKey(c.Key))
                    {
                        _dictionaryOfRemovedStones.Add(c.Key, c.Value);//Переписываем в глобальный словарь
                    }
                }
                AOCS.Add(InitStoneType, 0);//Инициализация
                foreach (KeyValuePair<(int, int), EStoneType> c in DORSHorizontal)
                {
                    AOCS[InitStoneType]++;//Переписываем в глобальный словарь
                }
                foreach (KeyValuePair<EStoneType, int> c in AOCS)
                {
                    if (_amountOfCombinedStones.ContainsKey(c.Key))
                    {
                        _amountOfCombinedStones[c.Key] += c.Value;//Добавляем к уже имеющемуся значению
                    }
                    else
                    {
                        _amountOfCombinedStones.Add(c.Key, c.Value);//Записываем новое значение
                    }
                }
                foreach ((EStoneType, int) c in CT)
                {
                    _combinationType.Add(c);//Переписываем в глобальный словарь
                }
            }

            if (CombinationSizeY >= 3)//Аналогично
            {
                CT.Add((InitStoneType, DORSVertical.Count()));
                foreach (KeyValuePair<(int, int), EStoneType> c in DORSVertical)
                {
                    if (!_dictionaryOfRemovedStones.ContainsKey(c.Key))
                    {
                        _dictionaryOfRemovedStones.Add(c.Key, c.Value);
                    }
                }
                if (!AOCS.ContainsKey(InitStoneType))
                {
                    AOCS.Add(InitStoneType, 0);
                }
                foreach (KeyValuePair<(int, int), EStoneType> c in DORSVertical)
                {
                    AOCS[InitStoneType]++;
                }
                foreach (KeyValuePair<EStoneType, int> c in AOCS)
                {
                    if (_amountOfCombinedStones.ContainsKey(c.Key))
                    {
                        _amountOfCombinedStones[c.Key] += c.Value;
                    }
                    else
                    {
                        _amountOfCombinedStones.Add(c.Key, c.Value);
                    }
                }
                foreach ((EStoneType, int) c in CT)
                {
                    _combinationType.Add(c);
                }
            }
        }

        public bool ScanFieldForPossibleCombinations()
        {
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    if (IsEligibleForCombination(i, j) == true)
                        return true;
                }
            }
            return false;
        }

        public bool IsEligibleForCombination(int x, int y)
        {
            EStoneType InitStoneType = StoneGridArray[x, y];
            int CombinationSizeX = 0;
            int CombinationSizeY = 0;
            for (int i = Math.Max(x - 1, 0); i > 0; i--)
            {
                if (StoneGridArray[i, y] == InitStoneType)
                {
                    CombinationSizeX++;
                }
                else
                {
                    break;
                }
            }
            for (int i = x; i < _gridSize; i++)
            {
                if (StoneGridArray[i, y] == InitStoneType)
                {
                    CombinationSizeX++;
                }
                else
                {
                    break;
                }
            }

            for (int i = y; i < _gridSize; i++)
            {
                if (StoneGridArray[x, i] == InitStoneType)
                {
                    CombinationSizeY++;
                }
                else
                {
                    break;
                }
            }
            for (int i = Math.Max(y - 1, 0); i > 0; i--)
            {
                if (StoneGridArray[x, i] == InitStoneType)
                {
                    CombinationSizeY++;
                }
                else
                {
                    break;
                }
            }
            if ((CombinationSizeX > 2) || (CombinationSizeY > 2))
                return true;
            return false;
        }

        public void Combine()
        {
            StoneGridData CD = new StoneGridData();
            CD.AmountOfCombinedStones = _amountOfCombinedStones;
            CD.OnFieldCombinations = _combinationType;
            //_activePlayer.StoneCombination(CD);
        }

        public int GetGridSize()
        {
            return _gridSize;
        }
        public EStoneType[,] GetGridArray()
        {
            return StoneGridArray;
        }
        #endregion
    }
}
