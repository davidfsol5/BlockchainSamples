using Newtonsoft.Json;
using System;

namespace BlockchainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ElapsedTimer timer = new ElapsedTimer();
            int miningDifficulty = 4;
            Console.WriteLine($"Starting chain, difficulty {miningDifficulty}...");

            // Create a blocjchain and mine three blocks.
            Blockchain phillyCoin = new Blockchain(miningDifficulty);
            Console.WriteLine($"Genesis block mined: {timer.ElapsedTime()}");
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            Console.WriteLine($"One data block mined: {timer.ElapsedTime()}");
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
            Console.WriteLine($"Two data blocks mined: {timer.ElapsedTime()}");
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            Console.WriteLine($"Three data blocks mined: {timer.ElapsedTime()}");
            Console.ReadKey();

            // Hack Block 1, lie about loan amount
            timer.Reset();
            phillyCoin.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            Console.WriteLine($"Block 1 hacked. Update amount to 1000: {timer.ElapsedTime()}");
            Console.ReadKey();

            // Try to cover up the lie by rehashing Block 1
            timer.Reset();
            phillyCoin.Chain[1].Hash = phillyCoin.Chain[1].CalculateHash();
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            Console.WriteLine($"Block 1 rehashed: {timer.ElapsedTime()}");
            Console.ReadKey();

            // Try to cover up the lie by remining  Block 1
            timer.Reset();
            phillyCoin.Chain[1].Mine(phillyCoin.MiningDifficulty);
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            Console.WriteLine($"Block 1 remined: {timer.ElapsedTime()}");
            Console.ReadKey();

            // Resort to remining from the hacked block to the end of the chain
            timer.Reset();
            phillyCoin.Chain[1].Mine(phillyCoin.MiningDifficulty);
            Console.WriteLine($"First data block remined: {timer.ElapsedTime()}");
            phillyCoin.Chain[2].PreviousHash = phillyCoin.Chain[1].Hash;
            phillyCoin.Chain[2].Mine(phillyCoin.MiningDifficulty);
            Console.WriteLine($"Second data block remined: {timer.ElapsedTime()}");
            phillyCoin.Chain[3].PreviousHash = phillyCoin.Chain[2].Hash;
            phillyCoin.Chain[3].Mine(phillyCoin.MiningDifficulty);
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            Console.WriteLine($"Chain remined from hack to end: {timer.ElapsedTime()}");
            Console.ReadKey();
        }
    }
}
