using System.Collections;

namespace InfiniteChess.Shared;

public record Board(Piece?[,] Pieces) : IEnumerable<(int x, int y)> {
	private long _id;
	public void Add(Piece piece, int x, int y)
		=> Pieces[x, y] = piece with {Id = _id++};

	public int Width => Pieces.GetLength(0);
	public int Height => Pieces.GetLength(1);

	public IEnumerator<(int x, int y)> GetEnumerator() {
		for (var x = 0; x < Width; x++) {
			for (var y = 0; y < Height; y++) {
				yield return (x, y);
			}
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
