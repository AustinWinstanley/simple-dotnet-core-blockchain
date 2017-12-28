using System.Collections.Generic;
using System.Linq;

namespace Blockchain
{
    /// <summary>
    /// The blockchain implementation.
    /// </summary>
    public class Blockchain
    {
        /// <summary>
        /// The chain of blocks to be kept.
        /// </summary>
        public IList<Block> Chain { get; set; } = new List<Block>();

        /// <summary>
        /// The mining difficulty set by the program and kept in the blockchain.
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="difficulty">The mining difficulty set by the program and kept in the blockchain.</param>
        public Blockchain(int difficulty)
        {
            // Set the difficulty
            Difficulty = difficulty;

            // Generate the initial block.
            var genesisBlock = CreateGenesisBlock();

            // Add the initial block to the chain.
            Chain.Add(genesisBlock);
        }

        /// <summary>
        /// Create the initial block in the chain.
        /// </summary>
        /// <returns>The initial genesis block.</returns>
        private Block CreateGenesisBlock() => new Block(new BlockData("Genesis Block", 0m), null);

        /// <summary>
        /// Get the most recent block in the chain.
        /// </summary>
        /// <returns>The most recent block in the chain.</returns>
        public Block GetLastBlock() => Chain.Last();

        /// <summary>
        /// Add a new block to the chain.
        /// </summary>
        /// <param name="data">The block data to be added to the chain.</param>
        public void AddBlock(BlockData data)
        {
            // Get the last block in the chain.
            var lastBlock = GetLastBlock();

            // Create a new block with the data and the parent hash.
            var block = new Block(data, lastBlock.Hash);

            // Generate a new block from the difficulty setting and data provided.
            block.MineBlock(Difficulty);

            // Add the new block to the chain.
            Chain.Add(block);
        }

        /// <summary>
        /// Validate the chain to ensure it has not been altered.
        /// </summary>
        /// <returns>True if the chain is valid. False otherwise.</returns>
        public bool Validate()
        {
            // Iterate the blocks in the chain.
            for (var i = 1; i < Chain.Count; i++) {
                // Get the current block in the chain.
                var currentBlock = Chain[i];
                
                // Get the previous block in the chain.
                var previousBlock = Chain[i - 1];

                // Get the current hash of the block.
                var currentHash = currentBlock.Hash;

                // // Recalculate the hash of the block.
                var newHash = currentBlock.CalculateHash();

                // Compare the current hash to the new hash to make sure they match.
                if (!currentHash.Equals(newHash)) {
                    return false;
                }

                // Compare the parent hash of this block to make sure it matches the hash of the previous block.
                if (!currentBlock.Parent.Equals(previousBlock.Hash)) {
                    return false;
                }
            }

            // Everything is valid.
            return true;
        }
    }
}
