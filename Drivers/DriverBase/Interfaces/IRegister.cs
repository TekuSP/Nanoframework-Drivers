namespace DriverBase.Interfaces
{
    public interface IRegister
    {
        #region Public Methods
        /// <summary>
        /// Gets one byte data based on 8 bit structure
        /// </summary>
        /// <returns>Formatted data</returns>
        byte GetData();
        /// <summary>
        /// Sets one byte data based on 8 bit structure
        /// </summary>
        /// <param name="input">Byte to set to formatted data</param>
        void SetData(byte input);

        #endregion Public Methods
    }
}