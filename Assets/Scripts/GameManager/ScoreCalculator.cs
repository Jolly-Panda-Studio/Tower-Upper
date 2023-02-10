public static class ScoreCalculator
{
    private const int KillScore = 10;

    public static int Score => EnemyCounter.KilledEnemy * KillScore;
}
