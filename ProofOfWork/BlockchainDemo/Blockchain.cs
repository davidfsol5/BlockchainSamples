using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainDemo
{
    public class Blockchain
    {
        public IList<Block> Chain { set;  get; }
        public int MiningDifficulty { set; get; } = 2;
        public string ValidMinedBlockHashPrefix { get; }

        public Blockchain(int miningDifficulty)
        {
            // Check for argument validity
            if (miningDifficulty < 0)
            {
                throw new ArgumentOutOfRangeException("difficulty", "Value of diffculty cannot be negative.");
            }

            // Initialize this instance of the class
            this.Chain = new List<Block>();
            this.MiningDifficulty = miningDifficulty;
            this.ValidMinedBlockHashPrefix = new string('0', miningDifficulty);

            AddGenesisBlock();
        }


        private Block CreateGenesisBlock()
        {
            Block genesisBlock = new Block(DateTime.Now, null, "{}");
            genesisBlock.Mine(this.MiningDifficulty);
            return genesisBlock;
        }

        private void AddGenesisBlock()
        {

            Chain.Add(CreateGenesisBlock());
        }
        
        public Block GetGenesisBlock()
        {
            return Chain[0];
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            //block.Hash = block.CalculateHash();
            block.Mine(this.MiningDifficulty);
            Chain.Add(block);
        }

        private string GetBlockHashPrefix(Block block)
        {
            return block.Hash.Substring(0, this.MiningDifficulty);
        }

        private bool IsGenesisBlockValid()
        {
            Block genesisBlock = this.GetGenesisBlock();

            if (genesisBlock.Index != 0)
            {
                return false;
            }

            if (this.GetBlockHashPrefix(genesisBlock) != this.ValidMinedBlockHashPrefix)
            {
                return false;
            }

            if (genesisBlock.Hash != genesisBlock.CalculateHash())
            {
                return false;
            }

            if (genesisBlock.PreviousHash != null)
            {
                return false;
            }

            return true;
        }

        public bool IsValid()
        {
            // Check Genesis Block
            if (!this.IsGenesisBlockValid())
            {
                return false;
            }

            // Check data blocks
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Index != i)
                {
                    return false;
                }

                if (this.GetBlockHashPrefix(currentBlock) != this.ValidMinedBlockHashPrefix)
                {
                    return false;
                }

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
