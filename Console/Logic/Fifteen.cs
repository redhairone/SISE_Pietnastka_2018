using System;
using System.Collections.Generic;
using System.Drawing;

namespace Logic
{
    public class Fifteen : IComparable<Fifteen>
    {
        #region Attributes
        private readonly int[,] arr;
        private readonly int length;
        private readonly int width;

        private Point empty;
        private int heuristic;

        private char direction;
        private string history;

        private readonly int depth;

        private Fifteen previous;
        private readonly List<Fifteen> next;
        #endregion // Attributes
        
        #region Accessors
        public int[,] Arr { get => this.arr; }
        public int Width { get => this.width; }
        public int Length { get => this.length; }
        public Point Empty { get => this.empty; set => this.empty = value; }
        public string History { get => this.history; set => this.history = value; }
        public Fifteen Previous { get => this.previous; set => this.previous = value; }
        public List<Fifteen> Next { get => this.next; }
        public int Depth { get => this.depth; }
        public char Direction { get => this.direction; set => direction = value; }
        public int Heuristic { get => this.heuristic; set => heuristic = value; }
        #endregion //Accessors
        
        #region Constructors

        public Fifteen(int[,] _arr, int _width, int _length, Point _empty)
        {
            this.width = _width;
            this.length = _length;
            this.arr = new int[_width,_length];
            this.next = new List<Fifteen>();
            this.empty = _empty;
            this.history = "";
            this.previous = null;
            this.depth = 0;

            Array.Copy(_arr, this.arr, _arr.Length);
        }

        public Fifteen(Fifteen _oldOne)
        {
            this.arr = new int[_oldOne.Width, _oldOne.Length];
            this.width = _oldOne.Width;
            this.length = _oldOne.Length;
            this.next = new List<Fifteen>();
            this.empty = _oldOne.Empty;
            this.history = _oldOne.History;
            this.previous = _oldOne;
            this.depth = _oldOne.Depth + 1;

            this.arr = _oldOne.Arr.Clone() as int[,];
        }
        #endregion //Constructors

        public Fifteen Move(char _direction)
        {
            Fifteen newOne = new Fifteen(this);
            newOne.History += _direction;
            newOne.Direction = _direction;
            Point temp = newOne.Empty;

            switch(_direction)
            {
                case 'U':
                    Tools.Swap(ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y], ref newOne.Arr[newOne.Empty.X - 1, newOne.Empty.Y]);
                    temp.X--;
                    break;

                case 'D':
                    Tools.Swap(ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y], ref newOne.Arr[newOne.Empty.X + 1, newOne.Empty.Y]);
                    temp.X++;
                    break;

                case 'L':
                    Tools.Swap(ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y], ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y - 1]);
                    temp.Y--;
                    break;

                case 'R':
                    Tools.Swap(ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y], ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y + 1]);
                    temp.Y++;
                    break;
                default:
                    break;
            }

            newOne.Empty = temp;
            return newOne;
        }

        public bool CheckMove(char _direction)
        {
            if ((_direction == 'U' && this.Empty.X == 0) ||
                (_direction == 'D' && this.Empty.X == width-1) ||
                (_direction == 'L' && this.Empty.Y == 0) ||
                (_direction == 'R' && this.Empty.Y == length-1)) return false;
            else return true;
        }

        public bool CheckCompletion()
        {
            int index = 1;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (this.arr[i, j] != index++) return false;
                }
            }

            for(int i = 0; i < 3; i++)
            {
                if (this.arr[3, i] != index++) return false;
            }

            if (this.arr[3, 3] != 0) return false;

            return true;
        }

        public bool CheckSimilarity(Fifteen _fifteen)
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j =0; j < 4; j++)
                {
                    if (_fifteen.Arr[i, j] != this.arr[i, j]) return false;
                }
            }

            return true;
        }

        public bool CheckBack(char _direction)
        {
            if ((this.direction == 'U' && _direction == 'D') ||
                (this.direction == 'D' && _direction == 'U') ||
                (this.direction == 'L' && _direction == 'R') ||
                (this.direction == 'R' && _direction == 'L')) return true;
            else return false;
        }

        public void CreateNextFifteens(string _searchOrder)
        {
            foreach(char sign in _searchOrder)
            {
                if (CheckMove(sign) && !CheckBack(sign)) this.next.Add(this.Move(sign));
            }
        }

        public void Show()
        {
            for(int i = 0; i < width; i ++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.Write(arr[i,j] + " ");
                }
                Console.Write('\n');
            }
        }

        public void CountHeuristic(string _strategyInfo)
        {
            if(_strategyInfo == "hamm")
            {
                heuristic = 0;

                int counter = 1;
                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < length; j++)
                    {
                        if (arr[i, j] != counter) heuristic++;
                    }
                }

                heuristic += depth;
            }
            else
            {
                heuristic = 0;

                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < length; j++)
                    {
                        if(arr[i,j] != i*width+j)
                        {
                            int ii = arr[i, j] / width;
                            int jj = arr[i, j] % width;

                            heuristic += Math.Abs(i - ii);
                            heuristic += Math.Abs(j - jj);
                        }
                    }
                }

                heuristic += depth;
            }
        }

        public int CompareTo(Fifteen other)
        {
            if (this.heuristic > other.Heuristic) return 1;
            else if (this.heuristic < other.Heuristic) return -1;
            else return 0;
        }
    }
}
