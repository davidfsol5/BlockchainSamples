using Newtonsoft.Json;
using System;

namespace BlockchainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ElapsedTimer timer = new ElapsedTimer();
            int miningDifficulty = 0;
            Console.WriteLine($"Starting chain, difficulty {miningDifficulty}...");

            // Create a blockchain and mine three blocks.
            Blockchain phillyCoin = new Blockchain(miningDifficulty);
            Console.WriteLine($"Genesis block mined: {timer.ElapsedTime()}");
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:David,receiver:Louis,amount:10}"));
            Console.WriteLine($"One data block mined: {timer.ElapsedTime()}");
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Louis,receiver:David,amount:5}"));
            Console.WriteLine($"Two data blocks mined: {timer.ElapsedTime()}");
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Louis,receiver:David,amount:5}"));
            Console.WriteLine($"Three data blocks mined: {timer.ElapsedTime()}");
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()} {timer.ElapsedTime()}");
            Console.WriteLine();
            //Console.ReadKey();

            // Hack Block 1, lie about loan amount
            timer.Reset();
            phillyCoin.Chain[1].Data = "{sender:David,receiver:Louis,amount:10000}";
            //Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Block 1 hacked to lie about loan. Update loan amount to 10,000: {timer.ElapsedTime()}");
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()} {timer.ElapsedTime()}");
            Console.WriteLine();
            //Console.ReadKey();

            // Try to cover up the lie by rehashing Block 1
            timer.Reset();
            phillyCoin.Chain[1].Hash = phillyCoin.Chain[1].CalculateHash();
            //Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Try to cover up lie. Rehash Block 1: {timer.ElapsedTime()}");
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()} {timer.ElapsedTime()}");
            Console.WriteLine();
            //Console.ReadKey();

            // Try to cover up the lie by remining  Block 1
            timer.Reset();
            phillyCoin.Chain[1].Nonce = 0;
            phillyCoin.Chain[1].Mine(phillyCoin.MiningDifficulty);
            //Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Try to cover up lie. Remine Block 1: {timer.ElapsedTime()}");
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()} {timer.ElapsedTime()}");
            Console.WriteLine();
            //Console.ReadKey();

            // Resort to remining from the hacked block to the end of the chain
            //timer.Reset();
            //phillyCoin.Chain[1].Mine(phillyCoin.MiningDifficulty);
            Console.WriteLine($"Try to cover up lie. Remine remaining blocks:");
            phillyCoin.Chain[2].PreviousHash = phillyCoin.Chain[1].Hash;
            phillyCoin.Chain[2].Nonce = 0;
            phillyCoin.Chain[2].Mine(phillyCoin.MiningDifficulty);
            Console.WriteLine($"Block 2 remined: {timer.ElapsedTime()}");
            phillyCoin.Chain[3].PreviousHash = phillyCoin.Chain[2].Hash;
            phillyCoin.Chain[3].Nonce = 0;
            phillyCoin.Chain[3].Mine(phillyCoin.MiningDifficulty);
            Console.WriteLine($"Block 3 remined: {timer.ElapsedTime()}");
            //Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            //Console.WriteLine($"Chain remined from hack to end: {timer.ElapsedTime()}");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
