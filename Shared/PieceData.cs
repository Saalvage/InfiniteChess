namespace InfiniteChess.Shared; 

public class PieceData {
	public string ImageUrl { get; set; }
	public string MoveScript { get; set; }

	public string GetImageUrl(Piece.Colors color)
		=> Path.GetDirectoryName(ImageUrl) + "/" + Path.GetFileNameWithoutExtension(ImageUrl) + "_" +
		(color == Piece.Colors.White ? "white" : "black") + Path.GetExtension(ImageUrl);
}
