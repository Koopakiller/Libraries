namespace Koopakiller.Coding.Morse
{
    public enum MorseSpace
    {
        Signal = 0,
        Character = MorseSignal.Dit,
        Word = MorseSignal.Dit * 7,
    }
}