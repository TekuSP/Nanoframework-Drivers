namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    namespace TekuSP.Drivers.Nano_OpenTherm.Enums
    {
        public enum CommandCode : byte
        {
            /// <summary>
            /// Reserved for future use.
            /// </summary>
            Reserved = 0,
            /// <summary>
            /// Boiler Lock-out Reset command.
            /// </summary>
            BLOR = 1,
            /// <summary>
            /// CH water filling.
            /// </summary>
            CHWF = 2,
            // Values 3 through 255 are implicitly reserved for future use.
        }
    }

}
