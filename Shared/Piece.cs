namespace InfiniteChess.Shared;

public record Piece(string Name, Piece.Colors Color) {
    public enum Colors {
        Black = 0, White = 1,
    }
}
