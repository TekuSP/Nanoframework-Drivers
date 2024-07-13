namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    /// <summary>
    /// Internal Status of Data Sending/Receiving
    /// </summary>
    enum DataStatus
    {
        NOT_INITIALIZED,
        READY,
        DELAY,
        REQUEST_SENDING,
        RESPONSE_WAITING,
        RESPONSE_START_BIT,
        RESPONSE_RECEIVING,
    };
}
