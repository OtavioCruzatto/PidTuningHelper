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
        AskForSendProcessVariable = 0x22,
        SetMovingAverageWindow = 0x23,
        AskForMinAndMaxSumOfErrors = 0x24,
        AskForMinAndMaxControlledVariable = 0x26,
        SetPidMinSumOfErrors = 0x28,
        SetPidMaxSumOfErrors = 0x29,
        SetPidMinControlledVariable = 0x30,
        SetPidMaxControlledVariable = 0x31,
        AskForPidOffsetAndBias = 0x32,
        SetPidOffset = 0x34,
        SetPidBias = 0x35
    }

    enum CommandsFromMicrocontroller : byte
    {
        PidKsParameterValues = 0x14,
        PidControllerParameterValues = 0x17,
        ProcessVariableValue = 0x21,
        PidMinAndMaxSumOfErrors = 0x25,
        PidMinAndMaxControlledVariable = 0x27,
        PidOffsetAndBias = 0x33
    }
}
