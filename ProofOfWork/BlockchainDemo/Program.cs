using Newtonsoft.Json;
using System;

namespace BlockchainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ElapsedTimer timer = new ElapsedTimer();
            int miningDifficulty = 3;

            Blockchain phillyCoin = new Blockchain(miningDifficulty);
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));

            Console.WriteLine($"Three data blocks mined: {timer.ElapsedTime()}");
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));

            phillyCoin.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";
            Console.WriteLine($"Block 1 hacked. Update amount to 1000: {timer.ElapsedTime()}");
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            phillyCoin.Chain[1].Hash = phillyCoin.Chain[1].CalculateHash();
            Console.WriteLine($"Block 1 rehash: {timer.ElapsedTime()}");
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            phillyCoin.Chain[2].PreviousHash = phillyCoin.Chain[1].Hash;
            phillyCoin.Chain[2].Hash = phillyCoin.Chain[2].CalculateHash();
            phillyCoin.Chain[3].PreviousHash = phillyCoin.Chain[2].Hash;
            phillyCoin.Chain[3].Hash = phillyCoin.Chain[3].CalculateHash();
            Console.WriteLine($"Rest of chain rehashed: {timer.ElapsedTime()}");
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            phillyCoin.Chain[1].Mine(phillyCoin.MiningDifficulty);
            phillyCoin.Chain[2].PreviousHash = phillyCoin.Chain[1].Hash;
            phillyCoin.Chain[2].Mine(phillyCoin.MiningDifficulty);
            phillyCoin.Chain[3].PreviousHash = phillyCoin.Chain[2].Hash;
            phillyCoin.Chain[3].Mine(phillyCoin.MiningDifficulty);
            Console.WriteLine($"Chain remined from hack to end: {timer.ElapsedTime()}");
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            Console.ReadKey();
        }
    }
}
