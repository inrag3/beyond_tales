namespace _Project.Runtime.Config
{
    public interface ILootConfig
    {
        int MaxLootCount { get; }
        int LootMinDistance { get; }
        int LootMaxDistance { get; }
        int LootFrontForce { get; }
        string LootRedPath { get; }
        string LootGreenPath { get; }
        string LootBluePath { get; }
    }
}