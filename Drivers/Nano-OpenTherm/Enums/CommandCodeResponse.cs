namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    public static class CommandCodeResponse
    {
        public static bool GetCommandCodeResponse(byte commandCode)
        {
            if (commandCode >= 127)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
