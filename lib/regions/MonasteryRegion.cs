public class MonasteryRegion : ScoreRegion {
    public MonasteryRegion(GameState state) : base(state) { }

    public override int GetScore(bool lite = false) {
        if (areas.Count < 1) return 0;
        int x = areas[0].pos.X;
        int y = areas[0].pos.Y;
        int sum = 0;
        if (state.board.Get(x, y + 1) != null) sum++;
        if (state.board.Get(x + 1, y + 1) != null) sum++;
        if (state.board.Get(x + 1, y) != null) sum++;
        if (state.board.Get(x + 1, y - 1) != null) sum++;
        if (state.board.Get(x, y - 1) != null) sum++;
        if (state.board.Get(x - 1, y - 1) != null) sum++;
        if (state.board.Get(x - 1, y) != null) sum++;
        if (state.board.Get(x - 1, y + 1) != null) sum++;
        if (!lite) sum++;
        return sum;
    }

    public override bool IsComplete() {
        if (areas.Count < 1) return false;
        int x = areas[0].pos.X;
        int y = areas[0].pos.Y;
        return state.board.Get(x, y + 1) != null &&
               state.board.Get(x + 1, y + 1) != null &&
               state.board.Get(x + 1, y) != null &&
               state.board.Get(x + 1, y - 1) != null &&
               state.board.Get(x, y - 1) != null &&
               state.board.Get(x - 1, y - 1) != null &&
               state.board.Get(x - 1, y) != null &&
               state.board.Get(x - 1, y + 1) != null;
    }
}