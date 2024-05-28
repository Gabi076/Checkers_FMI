using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Checkers.Models
{
    public enum SquareColor
    {
        Black,
        White
    }
    public class Square: BaseNotification
    {
        private int row;
        private int col;
        private SquareColor sColor;
        private string texture;
        private string hint;
        private Piece piece;
        //public Square(int row, int col, SquareColor color, Piece piece)
        //{
        //    this.row = row;
        //    this.col = col;
        //    this.sColor = color;
        //    if (color == SquareColor.Black)
        //    {
        //        texture = "/Checkers;component/Resources/BlackSquare.png";
        //    }
        //    else
        //    {
        //        texture = "/Checkers;component/Resources/WhiteSquare.png";
        //    }
        //    this.piece = piece;
        //}
        public Square(int Row, int Col, SquareColor SColor, Piece Pic)
        {
            this.Row = Row;
            this.Col = Col;
            this.SColor = SColor;
            if (SColor == SquareColor.Black)
            {
                texture = "/Checkers;component/Resources/BlackSquare.png";
            }
            else
            {
                texture = "/Checkers;component/Resources/WhiteSquare.png";
            }
            this.Pic = Pic;
        }
        public int Row { get { return row; } 
            set 
            {
                row = value;
                NotifyPropertyChanged();
            } 
        }
        public int Col { get { return col; }
            set
            {
                col = value;
                NotifyPropertyChanged();
            }
        }
        public SquareColor SColor {  get { return sColor; }
            set
            {
                sColor = value;
                NotifyPropertyChanged();
            }
        }
        [JsonIgnore]
        public string Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
                NotifyPropertyChanged();
            }
        }
        [JsonIgnore]
        public string Hint
        {
            get
            {
                return hint;
            }
            set
            {
                hint = value;
                NotifyPropertyChanged();
            }
        }
        public Piece Pic
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
                NotifyPropertyChanged();
            }
        }
    }
}
