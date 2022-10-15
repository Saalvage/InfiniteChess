namespace InfiniteChess.Shared;

public record Board(Piece?[,] Pieces) {
	private long _id;
	public void Add(Piece piece, int x, int y)
		=> Pieces[x, y] = piece with {Id = _id++};
}
