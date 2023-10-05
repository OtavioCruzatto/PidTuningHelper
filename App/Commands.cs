namespace PidTuningHelper
{
    enum CommandsToMicrocontroller : byte
    {
        SetKpValue = 0x10,
        SetKiValue = 0x11,
        SetKdValue = 0x12,
        AskForPidKsParameters = 0x13,
        AskForSendProcessVariable = 0x22
    }

    enum CommandsFromMicrocontroller : byte
    {
        ProcessVariableValue = 0x21
    }
}
