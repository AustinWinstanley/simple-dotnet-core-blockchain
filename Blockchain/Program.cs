using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blockchain
{
    class Program
    {
        /// <summary>
        /// The number of zeros to lead the hash. The higher the number the more difficult (and longer) to generate.
        /// </summary>
        public const int DIFFICULTY = 3;

        static void Main(string[] args)
        {
            // Create the new blockchain and pass it's difficulty setting.
            var blockchain = new Blockchain(DIFFICULTY);

            Console.WriteLine("Generating Blockchain...");

            Console.WriteLine();

            Console.WriteLine("Mining Block 1...");

            // Add a new block to be mined.
            blockchain.AddBlock(new BlockData("Foo", 10.25m));

            Console.WriteLine();

            Console.WriteLine("Mining Block 2...");

            // Add another new block to be mined.
            blockchain.AddBlock(new BlockData("Bar", 25.10m));

            Console.WriteLine();

            // Validate the blockchain.
            var valid = blockchain.Validate();

            if (valid) {
                Console.WriteLine("The blockchain is valid.");
            } else {
                Console.WriteLine("The blockchain is not valid.");
            }

            Console.WriteLine();

            // Print the full blockchain.
            Console.WriteLine("Blockchain:");

            var blockchainSring = JsonConvert.SerializeObject(blockchain);
            var prettyBlockchainString = JToken.Parse(blockchainSring).ToString(Formatting.Indented);

            Console.WriteLine(prettyBlockchainString);

            // Wait for input.
            Console.Read();
        }
    }
}
