public class RoadRegion : ScoreRegion {
    public RoadRegion(GameState state) : base(state) { }
    
    public override int GetScore(bool lite = false) {
        int sum = 0;
        sum += areas.Count;
        if (IsComplete() && lite) sum += 2;
        return sum;
    }
}