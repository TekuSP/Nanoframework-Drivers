namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    public static class CommandCodeResponse
    {
        /// <summary>
        /// Returns true when the command completed per v2.2: response 128..255 means completed; 0..127 means failed.
        /// </summary>
        public static bool GetCommandCodeResponse(byte responseCode)
        {
            return responseCode >= 128;
        }

        /// <summary>
        /// Alias for readability.
        /// </summary>
        public static bool IsCompleted(byte responseCode) => GetCommandCodeResponse(responseCode);
    }
}
