namespace PidTuningHelper
{
    enum DeviceSendData : byte
    {
        Disable = 0x00,
        Enable = 0x01
    }

    enum RunPidController : byte
    {
        False = 0x00,
        True = 0x01
    }
}
