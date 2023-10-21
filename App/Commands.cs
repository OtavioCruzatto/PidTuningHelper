namespace PidTuningHelper
{
    enum CommandsToMicrocontroller : byte
    {
        SetKpValue = 0x10,
        SetKiValue = 0x11,
        SetKdValue = 0x12,
        AskForPidKsParameters = 0x13,
        AskForRunPidController = 0x15,
        AskForPidControllerParameters = 0x16,
        SetSamplingIntervalValue = 0x18,
        SetPidIntervalValue = 0x19,
        SetPidSetpointValue = 0x20,
        AskForSendProcessVariable = 0x22
    }

    enum CommandsFromMicrocontroller : byte
    {
        PidKsParameterValues = 0x14,
        PidControllerParameterValues = 0x17,
        ProcessVariableValue = 0x21
    }
}
