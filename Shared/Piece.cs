namespace InfiniteChess.Shared;

public record struct Piece(string Name, Piece.Colors Color, long Id = -1) {
    public enum Colors {
        Black = 0, White = 1,
    }
}
