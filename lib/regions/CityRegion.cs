public class CityRegion : ScoreRegion {
    public CityRegion(GameState state) : base(state) { }
    
    public override int GetScore(bool lite = false) {
        int sum = 0;
        sum += areas.FindAll(a => a.type == AreaType.City).Count * 2;
        if (!lite) sum += areas.FindAll(a => a.shield).Count * 2;
        if (IsComplete() && lite) sum += 5;
        return sum;
    }

}