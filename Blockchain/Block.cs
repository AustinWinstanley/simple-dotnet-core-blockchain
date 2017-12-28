using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain
{
    /// <summary>
    /// The block to be used in the blockchain.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// The index of the block in the chain.
        /// </summary>
        public Guid Index { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The date and time the block was created.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// The variable to be changed at random to mine a valid block.
        /// </summary>
        public int Nonce { get; set; } = 0;

        /// <summary>
        /// The hash of the previous block.
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// The hash of the block.
        /// </summary>
        [JsonIgnore]
        public string Hash { get; set; }

        /// <summary>
        /// The data to be stored with the block.
        /// </summary>
        public BlockData Data { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data to be stored with the block.</param>
        /// <param name="parent">The parent block to the block.</param>
        public Block(BlockData data, string parent)
        {
            // Set the data for this block.
            Data = data;

            // Set the parent for this block.
            Parent = parent;

            // Calculate the hash for this block.
            CalculateHash();
        }

        /// <summary>
        /// Get the string to be hashed composed of the properties of the block.
        /// </summary>
        /// <returns>The aggregated string to be hashed.</returns>
        private string GetHashString() => JsonConvert.SerializeObject(this);

        /// <summary>
        /// Calculate the hash of the block.
        /// </summary>
        /// <returns>The calculated hash of the block.</returns>
        public string CalculateHash()
        {
            // Get the hash string to be calculated.
            var hashString = GetHashString();

            // Initialize the hash string builder.
            var hash = new StringBuilder();

            // Create a new crypto object.
            using (var crypt = new SHA256Managed()) {
                // Get the bytes of the hash string.
                var hashStringBytes = Encoding.ASCII.GetBytes(hashString);

                // Get the size of the hash string.
                var hashStringByteCount = Encoding.ASCII.GetByteCount(hashString);

                // Compute the hash based on the bytes and byte count.
                var crypto = crypt.ComputeHash(hashStringBytes, 0, hashStringByteCount);

                // Iterate the bytes created.
                foreach (var theByte in crypto) {
                    // Convert the byte to a string and add it to the hash string builder.
                    hash.Append(theByte.ToString("x2"));
                }
            }

            // Generate the hash string from the hash string builder and set the block hash.
            Hash = hash.ToString();

            // Return the new hash string.
            return Hash;
        }

        /// <summary>
        /// Generate a new block based on the data.
        /// </summary>
        /// <param name="difficulty">The mining difficulty set by the program and kept in the blockchain.</param>
        public void MineBlock(int difficulty)
        {
            // Loop the hash until it begins with the same number of zeros as the difficulty level.
            while (Hash.Substring(0, difficulty) != String.Join("0", new string[difficulty + 1])) {
                // Change the nonce.
                Nonce++;

                // Regenerate the hash.
                CalculateHash();
            }

            // Output the new hash.
            Console.WriteLine($"Block mined: {Hash}");
        }
    }
}
