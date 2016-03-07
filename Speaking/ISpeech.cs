namespace Speaking {
    public abstract class TempSpeech {

        // Need to handle a string that represents actual text -> speech,
        // and another case where the passed string represents the location, relative to some passed-elsewhere path,
        // of say a wav file
        public abstract void Speak(string speech);
    }
}