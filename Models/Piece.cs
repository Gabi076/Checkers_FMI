using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Checkers.Models
{
    public enum PieceColor
    {
        Black,
        Red
    }
    public enum PieceType
    {
        Normal,
        King
    }
    public class Piece : BaseNotification
    {
        private PieceColor color;
        private PieceType type;
        private string texture;
        private Square square;
        public Piece(PieceColor Color, PieceType Type = PieceType.Normal)
        {
            this.Color = Color;
            this.Type = Type;
            if (Color == PieceColor.Red)
            {
                texture = "/Checkers;component/Resources/RedPiece.png";
            }
            else
            {
                texture = "/Checkers;component/Resources/BlackPiece.png";
            }
            if (Type == PieceType.King && Color == PieceColor.Red)
            {
                texture = "/Checkers;component/Resources/RedKing.png";
            }
            if (Type == PieceType.King && Color == PieceColor.Black)
            {
                texture = "/Checkers;component/Resources/BlackKing.png";
            }
        }
        public Piece() { }
        public PieceColor Color {get { return color; }
            set
            {
                color = value;
                NotifyPropertyChanged();
            }
        }
        public PieceType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
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
        public Square Sqr
        {
            get
            {
                return square;
            }
            set
            {
                square = value;
                NotifyPropertyChanged();
            }
        }
    }
}
