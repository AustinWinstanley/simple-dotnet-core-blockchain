namespace Blockchain
{
    /// <summary>
    /// The data to be stored with each block in the chain.
    /// </summary>
    public class BlockData
    {
        /// <summary>
        /// The name of the transaction or person executing the transaction.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The amount of the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the transaction or person executing the transaction.</param>
        /// <param name="amount">The amount of the transaction.</param>
        public BlockData(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }

        /// <summary>
        /// Create a string based on the block data.
        /// </summary>
        /// <returns>The string representation of the data.</returns>
        public override string ToString() => $"Name: {Name}; Amount: {Amount};";
    }
}
