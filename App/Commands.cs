namespace PidTuningHelper
{
    enum Commands : byte
    {
        SetKpValue = 0x10,
        SetKiValue = 0x11,
        SetKdValue = 0x12,
        SetDeviceSendDataStatus = 0x40,
        AdcReadValue = 0x51
    }
}
