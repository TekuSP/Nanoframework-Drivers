using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetBoilerFanSpeedRequest : ReadRequest
    {
        #region Public Constructors

        public GetBoilerFanSpeedRequest() : base()
        {
        }

        public GetBoilerFanSpeedRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public Ratio Actual { get; protected set; }
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.BoilerFanSpeedResponse);

        public override MessageID MessageID => MessageID.BoilerFanSpeedSetpointAndActual;
        public override MessageType MessageType => MessageType.READ_DATA;

        // Percent values (0..100) for setpoint (HB) and actual (LB)
        public Ratio Setpoint { get; protected set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // Encode percents into two bytes (HB=setpoint, LB=actual) using Utilities helpers
            ushort payload = 0;
            payload = Utilities.SetHighByte(payload, (byte)Utilities.Normalize((float)Setpoint.Percent));
            payload = Utilities.SetLowByte(payload, (byte)Utilities.Normalize((float)Actual.Percent));
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            Setpoint = Ratio.FromPercent(Utilities.GetHighByte(value));
            Actual = Ratio.FromPercent(Utilities.GetLowByte(value));
        }

        #endregion Protected Methods
    }
}

// Removed non-standard BoilerFanSpeed request. File intentionally left empty; not included in project.