public static class GoldCalculator
{
    private const int GoldPerKill = 10;

    public static int GoldAmount => EnemyCounter.KilledEnemy * GoldPerKill;
}
