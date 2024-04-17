namespace PidTuningHelper
{
    enum CommandsToMicrocontroller : byte
    {
        SetConfigDataValues = 0x01,
        AskForCurrentConfigDataValues = 0x02,
        SetPidSetpointValue = 0x03,
        AskForRunPidController = 0x04,
        AskForSendProcessVariable = 0x05,
        AskForSendPidSetpointValue = 0x06
    }

    enum CommandsFromMicrocontroller : byte
    {
        CurrentConfigDataValues = 0x80,
        CurrentPidSetpointValue = 0x81,
        CurrentProcessVariableValue = 0x82,
        KeepAliveMessageWithPidControllerStatus = 0x83
    }
}
