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
        /// Test known to be flakey and needs more work. Might be impacted by side effects from other asserts or tests.
        /// </summary>
        public const string FLAKEY = "FLAKEY";
    }
}
