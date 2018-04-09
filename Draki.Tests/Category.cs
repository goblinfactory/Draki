namespace Draki.Tests
{
    public class Category
    {
        /// <summary>
        /// Slow - between 800ms and 2 seconds
        /// </summary>
        public const string SLOW = "SLOW";

        /// <summary>
        /// longer than 2 seconds
        /// </summary>
        public const string VERYSLOW = "VERYSLOW";

        /// <summary>
        /// Test known to be flakey and needs more work. 
        /// </summary>
        public const string FLAKEY = "FLAKEY";
    }
}
