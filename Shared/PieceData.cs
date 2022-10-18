namespace InfiniteChess.Shared; 

public record PieceData(string ImageUrl, string MoveScript) {
	public string GetImageUrl(Piece.Colors color)
		=> Path.GetDirectoryName(ImageUrl) + "/" + Path.GetFileNameWithoutExtension(ImageUrl) + "_" +
		(color == Piece.Colors.White ? "white" : "black") + Path.GetExtension(ImageUrl);

	public static Dictionary<string, PieceData> Basics { get; } = new() {
		{"pawn", new("img/pawn.svg", @"
function canMove(color, x, y, toX, toY)
	if x == toX then
		if BOARD[toX][toY] ~= EMPTY_SQUARE then
			return false
		end

		if y + 1 == toY and color == WHITE or y - 1 == toY and color == BLACK then
			return true
		end

		if color == WHITE  and y == 2 and toY == 4 and BOARD[toX][3] == EMPTY_SQUARE
			or color == BLACK and y == BOARD_HEIGHT - 1 and toY == BOARD_HEIGHT - 3 and BOARD[toX][BOARD_HEIGHT - 2] == EMPTY_SQUARE then
			return true
		else
			return false
		end
	else
		-- TODO: en passant
		if BOARD[toX][toY] == EMPTY_SQUARE then
			return false
		else
			if BOARD[toX][toY].color ~= color and math.abs(x - toX) == 1
				and (color == WHITE and y + 1 == toY or color == BLACK and y - 1 == toY) then
				return true
			else
				return false
			end
		end
	end
end

function getValidMoves(color, x, y)
	local moves = { }
	local dir = color == WHITE and 1 or -1
	if BOARD[x][y + dir] == EMPTY_SQUARE then
		table.insert(moves, { x, y + dir })
		if BOARD[x][y + dir * 2] == EMPTY_SQUARE and (color == WHITE and y == 2 or color == BLACK and y == BOARD_HEIGHT - 1) then
			table.insert(moves, { x, y + dir * 2 })
		end
	end
	for i = -1, 1, 2 do
		if isValidPosition(x + i, y + dir) and BOARD[x + i][y + dir] ~= EMPTY_SQUARE and BOARD[x + i][y + dir].color ~= color then
			table.insert(moves, { x + i, y + dir })
		end
	end
	return moves
end
")},
		{ "knight", new("img/knight.svg", @"
function canMove(color, x, y, toX, toY)
	if (math.abs(x - toX) == 2 and math.abs(y - toY) == 1
		or math.abs(x - toX) == 1 and math.abs(y - toY) == 2)
			and (BOARD[toX][toY] == EMPTY_SQUARE or BOARD[toX][toY].color ~= color) then
		return true
	else
		return false
	end
end

function getValidMoves(color, x, y)
	local moves = { }
	for i = -2, 2 do
		for j = -2, 2 do
			local toX = x + i
			local toY = y + j
			if math.abs(i) + math.abs(j) == 3 and isValidPosition(toX, toY) then
				if BOARD[toX][toY] == EMPTY_SQUARE or BOARD[toX][toY].color ~= color then
					table.insert(moves, { x + i, y + j })
				end
			end
		end
	end
	return moves
end
")},
		{"bishop", new("img/bishop.svg", @"
function canMove(color, x, y, toX, toY)
	if math.abs(x - toX) == math.abs(y - toY) then
		if BOARD[toX][toY] ~= EMPTY_SQUARE and BOARD[toX][toY].color == color then
			return false
		end

		local dirX = x < toX and 1 or -1
		local dirY = y < toY and 1 or -1

		for i = 1, math.abs(x - toX) - 1 do
			if BOARD[x + i * dirX][y + i * dirY] ~= EMPTY_SQUARE then
				return false
			end
		end

		return true
	else
		return false
	end
end

function getValidMoves(color, x, y)
	local moves = { }
	local min = math.min(BOARD_WIDTH, BOARD_HEIGHT)
	local dirs = { { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } }
	for i, dir in ipairs(dirs) do
		for j = 1, min do
			local toX = x + j * dir[1]
			local toY = y + j * dir[2]
			if isValidPosition(toX, toY) then
				if BOARD[toX][toY] == EMPTY_SQUARE or BOARD[toX][toY].color ~= color then
					table.insert(moves, { toX, toY })
				end
				if BOARD[toX][toY] ~= EMPTY_SQUARE then
					break
				end
			end
		end
	end
	return moves
end
")},
		{"rook", new("img/rook.svg", @"
function canMove(color, x, y, toX, toY)
	if x == toX or y == toY then
		if BOARD[toX][toY] ~= EMPTY_SQUARE and BOARD[toX][toY].color == color then
			return false
		end

		local dirX = x < toX and 1 or -1
		local dirY = y < toY and 1 or -1

		if x == toX then
			for i = 1, math.abs(y - toY) - 1 do
				if BOARD[x][y + i * dirY] ~= EMPTY_SQUARE then
					return false
				end
			end
		else
			for i = 1, math.abs(x - toX) - 1 do
				if BOARD[x + i * dirX][y] ~= EMPTY_SQUARE then
					return false
				end
			end
		end

		return true
	else
		return false
	end
end

function getValidMoves(color, x, y)
	local moves = { }
	local max = math.max(BOARD_WIDTH, BOARD_HEIGHT)
	local dirs = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } }
	for i, dir in ipairs(dirs) do
		for j = 1, max do
			local toX = x + j * dir[1]
			local toY = y + j * dir[2]
			if isValidPosition(toX, toY) then
				if BOARD[toX][toY] == EMPTY_SQUARE or BOARD[toX][toY].color ~= color then
					table.insert(moves, { toX, toY })
				end
				if BOARD[toX][toY] ~= EMPTY_SQUARE then
					break
				end
			end
		end
	end
	return moves
end
")},
		{"queen", new("img/queen.svg", @"
function canMove(color, x, y, toX, toY)
	if x == toX or y == toY or math.abs(x - toX) == math.abs(y - toY) then
		if BOARD[toX][toY] ~= EMPTY_SQUARE and BOARD[toX][toY].color == color then
			return false
		end

		local dirX = x < toX and 1 or -1
		local dirY = y < toY and 1 or -1

		if x == toX then
			for i = 1, math.abs(y - toY) - 1 do
				if BOARD[x][y + i * dirY] ~= EMPTY_SQUARE then
					return false
				end
			end
		elseif y == toY then
			for i = 1, math.abs(x - toX) - 1 do
				if BOARD[x + i * dirX][y] ~= EMPTY_SQUARE then
					return false
				end
			end
		else
			for i = 1, math.abs(x - toX) - 1 do
				if BOARD[x + i * dirX][y + i * dirY] ~= EMPTY_SQUARE then
					return false
				end
			end
		end

		return true
	else
		return false
	end
end

function getValidMoves(color, x, y)
	local moves = { }
	local max = math.max(BOARD_WIDTH, BOARD_HEIGHT)
	local dirs = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 },
				   { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } }
	for i, dir in ipairs(dirs) do
		for j = 1, max do
			local toX = x + j * dir[1]
			local toY = y + j * dir[2]
			if isValidPosition(toX, toY) then
				if BOARD[toX][toY] == EMPTY_SQUARE or BOARD[toX][toY].color ~= color then
					table.insert(moves, { toX, toY })
				end
				if BOARD[toX][toY] ~= EMPTY_SQUARE then
					break
				end
			end
		end
	end
	return moves
end
")},
		{"king", new("img/king.svg", @"
function canMove(color, x, y, toX, toY)
	if math.abs(x - toX) <= 1 and math.abs(y - toY) <= 1
		and (BOARD[toX][toY] == EMPTY_SQUARE or BOARD[toX][toY].color ~= color) then
		return true
	else
		return false
	end
end

function getValidMoves(color, x, y)
	local moves = { }
	local possibleMoves = { { x + 1, y }, { x - 1, y }, { x, y + 1 }, { x, y - 1 },
			                { x + 1, y + 1 }, { x - 1, y - 1 }, { x - 1, y + 1 }, { x + 1, y - 1 } }
	for i, move in ipairs(possibleMoves) do
		if isValidPosition(move[1], move[2]) and (BOARD[move[1]][move[2]] == EMPTY_SQUARE or BOARD[move[1]][move[2]].color ~= color) then
			table.insert(moves, move)
		end
	end
	return moves
end
")},
	};
}
