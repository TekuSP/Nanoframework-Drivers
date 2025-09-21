using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetBoilerFanSpeedRequest : ReadRequest
    {
        public GetBoilerFanSpeedRequest() : base() { }
        public GetBoilerFanSpeedRequest(Request baseReq) : base(baseReq) { }

    public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.BoilerFanSpeedResponse);

    public override MessageID MessageID => MessageID.BoilerFanSpeedSetpointAndActual;
    public override MessageType MessageType => MessageType.READ_DATA;

    // Percent values (0..100) for setpoint (HB) and actual (LB)
    public float SetpointPercent { get; protected set; }
    public float ActualPercent { get; protected set; }

        protected override uint GetRawDataCore()
        {
            // Encode percents into two bytes (HB=setpoint, LB=actual) using Utilities helpers
            ushort payload = 0;
            payload = Utilities.SetHighByte(payload, (byte)Utilities.Normalize(SetpointPercent));
            payload = Utilities.SetLowByte(payload, (byte)Utilities.Normalize(ActualPercent));
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            SetpointPercent = Utilities.GetHighByte(value);
            ActualPercent = Utilities.GetLowByte(value);
        }
    }
}
// Removed non-standard BoilerFanSpeed request. File intentionally left empty; not included in project.