using System;
using System.Drawing;

namespace Logic
{
    public class Fifteen
    {
        #region Attributes
        private byte[,] arr;
        private Point empty;

        private Move direction;
        private string history;

        private int depth;

        private Fifteen previous;
        #endregion // Attributes

        
        #region Accessors
        public byte[,] Arr { get => this.arr; }
        public Point Empty { get => this.empty; set => this.empty = value; }
        public string History { get => this.history; set => this.history = value; }
        public Fifteen Previous { get => this.previous; set => this.previous = value; }
        public int Depth { get => this.depth; }
        public Move Direction { get => this.direction; set => direction = value; }
        #endregion //Accessors
        
        #region Constructors

        public Fifteen(byte[,] _arr, Point _empty)
        {
            this.arr = new byte[4,4];
            this.empty = _empty;
            this.history = "";
            this.previous = null;
            this.depth = 0;

            Array.Copy(_arr, this.arr, _arr.Length);
        }

        public Fifteen(Fifteen _oldOne)
        {
            this.arr = new byte[4, 4];
            this.empty = _oldOne.Empty;
            this.history = _oldOne.History;
            this.previous = _oldOne;
            this.depth = _oldOne.Depth + 1;

            Array.Copy(_oldOne.Arr, this.arr, _oldOne.Arr.Length);
        }
        #endregion //Constructors

        public Fifteen Move(Move _direction)
        {
            Fifteen newOne = new Fifteen(this);
            newOne.History += _direction;
            newOne.Direction = _direction;
            Point temp;

            switch(_direction)
            {
                case Logic.Move.Up:
                    Tools.Swap(ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y], ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y - 1]);
                    temp = newOne.Empty;
                    temp.Y--;
                    newOne.Empty = temp;
                    break;

                case Logic.Move.Down:
                    Tools.Swap(ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y], ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y + 1]);
                    temp = newOne.Empty;
                    temp.Y++;
                    newOne.Empty = temp;
                    break;

                case Logic.Move.Left:
                    Tools.Swap(ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y], ref newOne.Arr[newOne.Empty.X - 1, newOne.Empty.Y]);
                    temp = newOne.Empty;
                    temp.X--;
                    newOne.Empty = temp;
                    break;

                case Logic.Move.Right:
                    Tools.Swap(ref newOne.Arr[newOne.Empty.X, newOne.Empty.Y], ref newOne.Arr[newOne.Empty.X + 1, newOne.Empty.Y]);
                    temp = newOne.Empty;
                    temp.X++;
                    newOne.Empty = temp;
                    break;
                default:
                    break;
            }
            return newOne;
        }

        public bool CheckMove(Move _direction)
        {
            if ((_direction == Logic.Move.Up && this.Empty.Y == 0) ||
                (_direction == Logic.Move.Down && this.Empty.Y == 3) ||
                (_direction == Logic.Move.Left && this.Empty.X == 0) ||
                (_direction == Logic.Move.Right && this.Empty.X == 3)) return false;
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

        public bool CheckBack(Move _direction)
        {
            if ((this.direction == Logic.Move.Up && _direction == Logic.Move.Down) ||
                (this.direction == Logic.Move.Down && _direction == Logic.Move.Up) ||
                (this.direction == Logic.Move.Left && _direction == Logic.Move.Right) ||
                (this.direction == Logic.Move.Right && _direction == Logic.Move.Left)) return true;
            else return false;
        }
    }
}
